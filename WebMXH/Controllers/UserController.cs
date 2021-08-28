using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMXH.Models;

namespace WebMXH.Controllers
{
    public class UserController : Controller
    {
        MXH_GREENZONEEntities db = new MXH_GREENZONEEntities();
        // GET: User
        public ActionResult Index()
        {
            return View(db.BAIVIET.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(BAIVIET baiviet)
        {
            try
            {
                // TODO: Add insert logic here
                db.BAIVIET.Add(baiviet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.USERR.Where(x => x.USERID == id).FirstOrDefault());
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, USERR user)
        {
            try
            {
                // TODO: Add update logic here
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
