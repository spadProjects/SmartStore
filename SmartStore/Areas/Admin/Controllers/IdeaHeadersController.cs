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
    public class IdeaHeadersController : Controller
    {
        private SmartStoreContext db = new SmartStoreContext();

        // GET: Admin/IdeaHeaders
        public ActionResult Index()
        {
            return View(db.IdeaHeaders.ToList());
        }

        // GET: Admin/IdeaHeaders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/IdeaHeaders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Header,NotShow")] IdeaHeader ideaHeader)
        {
            if (ModelState.IsValid)
            {
                ideaHeader.InsertUser = "Admin";
                ideaHeader.InsertDate = DateTime.Now;
                ideaHeader.UpdateUser = "Admin";
                ideaHeader.UpdateDate = DateTime.Now;
                ideaHeader.IsArchived = false;
                ideaHeader.IsDeleted = false;
                db.IdeaHeaders.Add(ideaHeader);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ideaHeader);
        }

        // GET: Admin/IdeaHeaders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdeaHeader ideaHeader = db.IdeaHeaders.Find(id);
            if (ideaHeader == null)
            {
                return HttpNotFound();
            }
            return View(ideaHeader);
        }

        // POST: Admin/IdeaHeaders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Header,NotShow")] IdeaHeader ideaHeader)
        {
            if (ModelState.IsValid)
            {
                ideaHeader.InsertUser = "Admin";
                ideaHeader.InsertDate = DateTime.Now;
                ideaHeader.UpdateUser = "Admin";
                ideaHeader.UpdateDate = DateTime.Now;
                ideaHeader.IsArchived = false;
                ideaHeader.IsDeleted = false;
                db.Entry(ideaHeader).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ideaHeader);
        }

        // GET: Admin/IdeaHeaders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdeaHeader ideaHeader = db.IdeaHeaders.Find(id);
            if (ideaHeader == null)
            {
                return HttpNotFound();
            }
            return View(ideaHeader);
        }

        // POST: Admin/IdeaHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IdeaHeader ideaHeader = db.IdeaHeaders.Find(id);
            db.IdeaHeaders.Remove(ideaHeader);
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
