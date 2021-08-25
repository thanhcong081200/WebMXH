using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMXH.Hubs;
using WebMXH.Models;
using WebMXH.Services;

namespace WebMXH.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View(new LoginData());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginData loginData)
        {
            if (ModelState.IsValid)
            {
                int userId;
                if (new AppService().Login(loginData, out userId))
                {
                    FormsAuthentication.RedirectFromLoginPage(userId.ToString(), loginData.RememberMe);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.LoginError = "Tên người dùng không tồn tại";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            int userId = int.Parse(User.Identity.Name);
            new AppService().RemoveAllUserConnections(userId);
            ChatHub.OfflineUser(userId);
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult Profiles()
        {
            using (MXH_GREENZONEEntities db = new MXH_GREENZONEEntities())
            {

                return View(db.USERRs.ToList());
            }
        }

        // GET: User/Edit/5
        public ActionResult EditProfile(int id)
        {
            using (MXH_GREENZONEEntities db = new MXH_GREENZONEEntities())
            {
                return View(db.USERRs.Where(x => x.USERID == id).FirstOrDefault());
            }
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult EditProfile(HttpPostedFileBase filename,int id, USERR user)
        {
            using (MXH_GREENZONEEntities db = new MXH_GREENZONEEntities())
            {
                try
                {
                    //Upload File
                        var file = Path.GetFileName(filename.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/assets/img"), file);
                        filename.SaveAs(path);
                        user.HINHANH = file;

                    //Sửa thông tin
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("EditProfile");
                }
                catch
                {
                    return View();

                }
            }
        }

        //GET: Register
        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(USERR _user)
        {
            MXH_GREENZONEEntities db = new MXH_GREENZONEEntities();
            if (ModelState.IsValid)
            {
                var check = db.USERRs.FirstOrDefault(s => s.EMAIL == _user.EMAIL);
                if (check == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.USERRs.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Email đã tồn tại";
                    return View();
                }
            }
            return View();
        }
     
        [HttpPost]
        public ActionResult GetChatbox(int toUserId)
        {
            ChatBoxModel chatBoxModel = new AppService().GetChatbox(toUserId);
            return PartialView("~/Views/Partials/_ChatBox.cshtml", chatBoxModel);
        }

        [HttpPost]
        public ActionResult SendMessage(int toUserId, string message)
        {
            return Json(new AppService().SendMessage(toUserId, message));
        }

        [HttpPost]
        public ActionResult LazyLoadMssages(int toUserId, int skip)
        {
            return Json(new AppService().LazyLoadMssages(toUserId, skip));
        }
    }
}