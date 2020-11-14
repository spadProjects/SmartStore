using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartStore.DataAccess.Context;
using SmartStore.Model.Entities;

namespace SmartStore.Areas.Admin.Controllers
{
    public class SocialNetWorksController : Controller
    {
        private SmartStoreContext db = new SmartStoreContext();

        // GET: Admin/SocialNetWorks
        public ActionResult Index()
        {
            return View(db.SocialNetWorks.ToList());
        }

        // GET: Admin/SocialNetWorks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SocialNetWorks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Icon,Link,Order,NotShow")] SocialNetWork socialNetWork)
        {
            if (ModelState.IsValid)
            {
                socialNetWork.InsertUser = "Admin";
                socialNetWork.InsertDate = DateTime.Now;
                socialNetWork.UpdateUser = "Admin";
                socialNetWork.UpdateDate = DateTime.Now;
                socialNetWork.IsArchived = false;
                socialNetWork.IsDeleted = false;
                db.SocialNetWorks.Add(socialNetWork);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(socialNetWork);
        }

        // GET: Admin/SocialNetWorks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialNetWork socialNetWork = db.SocialNetWorks.Find(id);
            if (socialNetWork == null)
            {
                return HttpNotFound();
            }
            return View(socialNetWork);
        }

        // POST: Admin/SocialNetWorks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Icon,Link,Order,NotShow")] SocialNetWork socialNetWork)
        {
            if (ModelState.IsValid)
            {
                socialNetWork.InsertUser = "Admin";
                socialNetWork.InsertDate = DateTime.Now;
                socialNetWork.UpdateUser = "Admin";
                socialNetWork.UpdateDate = DateTime.Now;
                socialNetWork.IsArchived = false;
                socialNetWork.IsDeleted = false;
                db.Entry(socialNetWork).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(socialNetWork);
        }

        // GET: Admin/SocialNetWorks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialNetWork socialNetWork = db.SocialNetWorks.Find(id);
            if (socialNetWork == null)
            {
                return HttpNotFound();
            }
            return View(socialNetWork);
        }

        // POST: Admin/SocialNetWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SocialNetWork socialNetWork = db.SocialNetWorks.Find(id);
            db.SocialNetWorks.Remove(socialNetWork);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
