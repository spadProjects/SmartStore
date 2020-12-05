using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SmartStore.DataAccess.Context;
using SmartStore.Model.Entities;

namespace SmartStore.Areas.Admin.Controllers
{
    public class FeaturesController : Controller
    {
        private SmartStoreContext db = new SmartStoreContext();
        // GET: Features
        public ActionResult Index(int? Page, int? pageSize)
        {
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
                new SelectListItem() { Value="75", Text= "75" },
                new SelectListItem() { Value="100", Text= "100" },
                new SelectListItem() { Value="125", Text= "125" },
                new SelectListItem() { Value="150", Text= "150" },
            };

            int pagesize = (pageSize ?? 25);
            int PageNumber = (Page ?? 1);

            List<SmartStore.Model.Entities.Feature> prolist = new List<Feature>();
            prolist = db.Features.OrderByDescending(p => p.Id).ToList();
            return View(prolist.ToPagedList(PageNumber, pagesize));
        }
        // GET: Admin/Features/Create
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Feature feature)
        {
            if (ModelState.IsValid)
            {
                db.Features.Add(feature);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(feature);
        }

        // GET: Admin/Features/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feature feature = db.Features.Find(id.Value);
            if (feature == null)
            {
                return HttpNotFound();
            }
            return PartialView(feature);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Feature feature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feature).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feature);
        }

        // GET: Admin/Features/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feature feature = db.Features.Find(id.Value);
            if (feature == null)
            {
                return HttpNotFound();
            }
            return PartialView(feature);
        }

        // POST: Admin/Features/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feature feature = db.Features.Find(id);
            db.Features.Remove(feature);
            db.SaveChanges(); 
            return RedirectToAction("Index");
        }
    }
}