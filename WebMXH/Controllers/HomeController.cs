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
        MXH_GREENZONEEntities data = new MXH_GREENZONEEntities();

        [HttpGet]
        public ActionResult Index(USERR user)
        {
            using (MXH_GREENZONEEntities db = new MXH_GREENZONEEntities())
            {
                List<BAIVIET> lst = new List<BAIVIET>();
                USERR iduser = Session["user"] as USERR;
                if (iduser != null)
                {
                    int id = int.Parse(iduser.USERID.ToString());
                     lst = db.BAIVIET.Where(bv => bv.USERID == id ).OrderByDescending(p=>p.IDBAIVIET).ToList();
                    
                }
                return View(lst);

                //db.Entry(user).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult CreateNewPost()
        {
            return View();
        }

        [HttpPost]
        public JsonResult addbaiviet(string noidung,BAIVIET baiviet,string chedo)
        {
            USERR user = Session["user"] as USERR;
            baiviet.USERID = user.USERID;
            baiviet.NOIDUNG = noidung;
            baiviet.NGAYDANG = DateTime.Now;
            baiviet.CHEDO = chedo;
            try
            {
                data.BAIVIET.Add(baiviet);
                data.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
            [HttpGet]
        public ActionResult XemBaiViet(int id)
        {
            using (MXH_GREENZONEEntities db = new MXH_GREENZONEEntities())
            {
                return View(db.BAIVIET.Where(x=>x.USERID == id).FirstOrDefault());
            } 
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
                USERR user = new AppService().Login(loginData,out userId);
                if (user != null)
                {
                    Session["user"] = user;
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
                return View(db.USERR.ToList());
            }
        }

        // GET: User/Edit/5
        public ActionResult EditProfile(int id)
        {
            using (MXH_GREENZONEEntities db = new MXH_GREENZONEEntities())
            {
                return View(db.USERR.Where(x => x.USERID == id).FirstOrDefault());
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

                    //DropdownList

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
                var check = db.USERR.FirstOrDefault(s => s.EMAIL == _user.EMAIL);
                if (check == null)
                {
                    _user.HINHANH = "user.png";
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.USERR.Add(_user);
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