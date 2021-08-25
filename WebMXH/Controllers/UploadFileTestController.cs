using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using WebMXH.Models;

namespace WebMXH.Controllers
{
    public class UploadFileTestController : Controller
    {
        // GET: UploadFile
        public ActionResult upload()
        {
            return View();
        }

        // GET: UploadFileSuccess
        public ActionResult createupload()
        {
            return View();
        }
        // POST: createupload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createupload(HttpPostedFileBase filename,USERR user)
        {
            using (MXH_GREENZONEEntities db = new MXH_GREENZONEEntities())
            {
                try
                {
                    if (filename.ContentLength > 0)
                    {
                        var file = Path.GetFileName(filename.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/assets/img"), file);
                        filename.SaveAs(path);


                        user.HINHANH = file;
                        db.USERRs.Add(user);
                        db.SaveChanges();


                        ViewBag.success = "Lưu thành công";
                    }
                    else
                    {
                        ViewBag.error = "Lưu thất bại";
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return View();

        }

        // POST: UploadFile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult upload(HttpPostedFileBase filename)
        {
            try
            {
                if(filename.ContentLength > 0)
                {
                    var file = Path.GetFileName(filename.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/assets/img"), file);
                    filename.SaveAs(path);
                    ViewBag.success = "Lưu thành công";
                }
                else
                {
                    ViewBag.error = "Lưu thất bại";
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }
    }
}