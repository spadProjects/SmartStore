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
    public class SubFeaturesController : Controller
    {
        private SmartStoreContext db = new SmartStoreContext();
        // GET: Admin/SubFeatures
        public ActionResult Index(int featureId,int? Page, int? pageSize)
        {
            ViewBag.FeatureId = featureId;
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

            var subFeatureList = db.SubFeatures.Where(sf=>sf.FeatureId == featureId).OrderByDescending(p => p.Id).ToList();
            return View(subFeatureList.ToPagedList(PageNumber, pagesize));
        }
        // GET: Admin/SubFeatures/Create
        public ActionResult Create(int featureId)
        {
            ViewBag.FeatureId = featureId;
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubFeature subFeature)
        {
            if (ModelState.IsValid)
            {
                db.SubFeatures.Add(subFeature);
                db.SaveChanges();
                return RedirectToAction("Index", new { featureId = subFeature.FeatureId });
            }

            return View(subFeature);
        }

        // GET: Admin/SubFeatures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubFeature subFeature = db.SubFeatures.Find(id.Value);
            if (subFeature == null)
            {
                return HttpNotFound();
            }
            return PartialView(subFeature);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubFeature subFeature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subFeature).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { featureId = subFeature.FeatureId });
            }
            return View(subFeature);
        }

        // GET: Admin/SubFeatures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubFeature subFeature = db.SubFeatures.Find(id.Value);
            if (subFeature == null)
            {
                return HttpNotFound();
            }
            return PartialView(subFeature);
        }

        // POST: Admin/SubFeatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubFeature subFeature = db.SubFeatures.Find(id);
            var featureId = subFeature.FeatureId;
            db.SubFeatures.Remove(subFeature);
            db.SaveChanges();
            return RedirectToAction("Index", new { featureId });
        }
    }
}