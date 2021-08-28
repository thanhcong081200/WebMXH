using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMXH.Models;
using WebMXH.Hubs;


namespace WebMXH.Services
{
    public class AppService
    {
        MXH_GREENZONEEntities _Context;

        public AppService()
        {
            _Context = new MXH_GREENZONEEntities();
        }

        public bool Login(LoginData loginData, out int userId)
        {
            userId = 0;
            var currentUser = _Context.USERR.FirstOrDefault(x => x.SDT == loginData.Username || x.USERNAME == loginData.Username && x.PASSWORD == loginData.Password);
            if (currentUser != null)
            {
                userId = currentUser.USERID;
                return true;
            }
            return false;
        }

        public List<UserDTO> GetUsersToChat()
        {
            var userId = int.Parse(HttpContext.Current.User.Identity.Name);
            return _Context.USERR
                .Include("USER_CONNECTION")
                .Where(x => x.USERID != userId)
                .Select(x => new UserDTO
                {
                    UserId = x.USERID,
                    UserName = x.USERNAME,
                    FullName = x.FULLNAME,
                    HinhAnh = x.HINHANH,
                    IsOnline = x.USER_CONNECTION.Count > 0,
                }).ToList();
        }

        internal int AddUserConnection(Guid ConnectionId)
        {
            var userId = int.Parse(HttpContext.Current.User.Identity.Name);
            _Context.USER_CONNECTION.Add(new USER_CONNECTION
            {
                CONNECTIONID = ConnectionId,
                USERID = userId,
            });
            _Context.SaveChanges();
            return userId;
        }

        internal int RemoveUserConnection(Guid ConnectionId)
        {
            int userId = 0;
            var current = _Context.USER_CONNECTION.FirstOrDefault(x => x.CONNECTIONID == ConnectionId);
            if (current != null)
            {
                userId = current.USERID ?? 0;
                _Context.USER_CONNECTION.Remove(current);
                _Context.SaveChanges();
            }
            return userId;
        }

        internal IList<string> GetUSerConnections(int uSerId)
        {
            return _Context.USER_CONNECTION.Where(x => x.USERID == uSerId).Select(x => x.CONNECTIONID.ToString()).ToList();
        }

        internal void RemoveAllUserConnections(int userId)
        {
            var current = _Context.USER_CONNECTION.Where(x => x.USERID == userId);
            _Context.USER_CONNECTION.RemoveRange(current);
            _Context.SaveChanges();
        }

        internal ChatBoxModel GetChatbox(int toUserId)
        {
            var userId = int.Parse(HttpContext.Current.User.Identity.Name);
            var toUser = _Context.USERR.FirstOrDefault(x => x.USERID == toUserId);
            var messages = _Context.MESSAGE.Where(x => (x.FROM_USER == userId && x.TO_USER == toUserId) || (x.FROM_USER == toUserId && x.TO_USER == userId))
                .OrderByDescending(x => x.DATE)
                .Skip(0)
                .Take(50)
                .Select(x => new MessageDTO
                {
                    IDMes = x.IDMES,
                    Message = x.MESSAGE1,
                    Time = x.DATE.Value,
                    Class = x.FROM_USER == userId ? "from" : "to",
                })
                .OrderBy(x => x.IDMes)
                .ToList();

            return new ChatBoxModel
            {
                ToUser = ToUserDTO(toUser),
                Messages = messages,
            };
        }

        internal bool SendMessage(int toUserId, string message)
        {
            try
            {
                int USER_ID = int.Parse(HttpContext.Current.User.Identity.Name);
                _Context.MESSAGE.Add(new MESSAGE
                {
                    FROM_USER = USER_ID,
                    TO_USER = toUserId,
                    MESSAGE1 = message,
                    DATE = DateTime.Now
                });
                _Context.SaveChanges();
                ChatHub.RecieveMessage(USER_ID, toUserId, message);
                return true;
            }
            catch { return false; }
        }

        public UserDTO ToUserDTO(USERR user)
        {
            return new UserDTO
            {
                FullName = user.FULLNAME,
                UserId = user.USERID,
                UserName = user.USERNAME,
                HinhAnh = user.HINHANH,
            };
        }

        internal List<MessageDTO> LazyLoadMssages(int toUserId, int skip)
        {
            var userId = int.Parse(HttpContext.Current.User.Identity.Name);
            var messages = _Context.MESSAGE.Where(x => (x.FROM_USER == userId && x.TO_USER == toUserId) || (x.FROM_USER == toUserId && x.TO_USER == userId))
                .OrderByDescending(x => x.DATE)
                .Skip(skip)
                .Take(50)
                .Select(x => new MessageDTO
                {
                    IDMes = x.IDMES,
                    Message = x.MESSAGE1,
                    Class = x.FROM_USER == userId ? "from" : "to",
                })
                .OrderByDescending(x => x.IDMes)
                .ToList();
            return messages;
        }
    }
}