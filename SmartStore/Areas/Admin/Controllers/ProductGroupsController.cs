using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartStore.DataAccess.Context;
using SmartStore.Model.Entities;
using SmartStore.Models;

namespace SmartStore.Areas.Admin.Controllers
{
    public class ProductGroupsController : Controller
    {
        private SmartStoreContext db = new SmartStoreContext();

        // GET: Admin/ProductGroups
        public ActionResult Index(int? id = null)
        {
            Session.Add("GroupId", id);
            var productgroups = db.ProductGroups.ToList();
            return View(productgroups.ToList());
        }

        // GET: Admin/ProductGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productGroup = db.ProductGroups.Find(id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            return View(productGroup);
        }

        // GET: Admin/ProductGroups/Create
        public ActionResult Create()
        {
            ViewBag.Features = db.Features.ToList();
            ViewBag.Brands = db.Brands.ToList();
            ViewBag.ProductGroups = db.ProductGroups.Include(p=>p.Children).ToList();
            //ViewBag.GroupId = new SelectList(db.ProductGroups.OrderBy(pg => pg.GroupOrder), "GroupId", "GroupName");
            return View();
        }

        // POST: Admin/ProductGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public int? Create(NewProductGroupViewModel newProductGroup)
        {
            if (ModelState.IsValid)
            {
                var productGroup = new ProductGroup();

                #region Adding Product Group
                productGroup.GroupName = newProductGroup.GroupName;
                if (newProductGroup.ParentGroupId != 0)
                    productGroup.ParentId = newProductGroup.ParentGroupId;
                db.ProductGroups.Add(productGroup);
                db.SaveChanges();
                #endregion

                #region Adding Product Group Brands

                foreach (var brandId in newProductGroup.BrandIds)
                {
                    var productGroupBrand = new ProductGroupBrand();
                    productGroupBrand.ProductGroupId = productGroup.Id;
                    productGroupBrand.BrandId = brandId;
                    db.ProductGroupBrands.Add(productGroupBrand);
                }
                db.SaveChanges();

                #endregion
                #region Adding product Group Features
                foreach (var featureId in newProductGroup.ProductGroupFeatureIds)
                {
                    var productGroupFeature = new ProductGroupFeature();
                    productGroupFeature.ProductGroupId = productGroup.Id;
                    productGroupFeature.FeatureId = featureId;
                    db.ProductGroupFeatures.Add(productGroupFeature);
                }

                db.SaveChanges();
                #endregion
                return productGroup.Id;
            }

            return null;
        }
        // GET: Admin/ProductGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productGroup = db.ProductGroups.Include(p=>p.ProductGroupBrands).Include(p=>p.ProductGroupFeatures).FirstOrDefault(p=>p.Id == id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.Features = db.Features.ToList();
            ViewBag.Brands = db.Brands.ToList();
            ViewBag.ProductGroups = db.ProductGroups.Include(p => p.Children).ToList();
            return PartialView(productGroup);
        }

        // POST: Admin/ProductGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public int? Edit(UpdateProductGroupViewModel newProductGroup)
        {
            if (ModelState.IsValid)
            {
                var productGroup = db.ProductGroups.Find(newProductGroup.Id);
                #region Adding Product Group
                productGroup.GroupName = newProductGroup.GroupName;
                if (newProductGroup.ParentGroupId != 0)
                    productGroup.ParentId = newProductGroup.ParentGroupId;
                else
                    productGroup.ParentId = null;

                db.Entry(productGroup).State = EntityState.Modified;
                db.SaveChanges();
                #endregion

                #region Product Group Brands

                // Removing Previous Group Brands
                var productGroupBrands = db.ProductGroupBrands
                    .Where(b =>b.ProductGroupId == productGroup.Id).ToList();
                foreach (var item in productGroupBrands)
                    db.ProductGroupBrands.Remove(item);

                db.SaveChanges();
                // Adding new Group Brands
                foreach (var brandId in newProductGroup.BrandIds)
                {
                    var productGroupBrand = new ProductGroupBrand();
                    productGroupBrand.ProductGroupId = productGroup.Id;
                    productGroupBrand.BrandId = brandId;
                    db.ProductGroupBrands.Add(productGroupBrand);
                }
                db.SaveChanges();

                #endregion
                #region product Group Features
                var productGroupFeatures = db.ProductGroupFeatures
                    .Where(b => b.ProductGroupId == productGroup.Id).ToList();

                // Removing Previous Group Features
                foreach (var item in productGroupFeatures)
                    db.ProductGroupFeatures.Remove(item);

                db.SaveChanges();

                // Adding New Group Features
                foreach (var featureId in newProductGroup.ProductGroupFeatureIds)
                {
                    var productGroupFeature = new ProductGroupFeature();
                    productGroupFeature.ProductGroupId = productGroup.Id;
                    productGroupFeature.FeatureId = featureId;
                    db.ProductGroupFeatures.Add(productGroupFeature);
                }
                db.SaveChanges();
                #endregion
                return productGroup.Id;
            }

            return null;
        }

        [HttpPost]
        public bool UploadImage(int id, HttpPostedFileBase File)
        {
            #region Upload Image
            if (File != null)
            {
                var productGroup = db.ProductGroups.Find(id);
                if (productGroup.GroupImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Images/ProductGroup/" + productGroup.GroupImage)))
                        System.IO.File.Delete(Server.MapPath("/Images/ProductGroup/" + productGroup.GroupImage));
                }
                // Saving Image
                var newFileName = Guid.NewGuid() + Path.GetExtension(File.FileName);
                File.SaveAs(Server.MapPath("/Images/ProductGroup/" + newFileName));
                productGroup.GroupImage = newFileName;
                db.Entry(productGroup).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            #endregion
            return false;
        }
        // GET: Admin/ProductGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productGroup = db.ProductGroups.Find(id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            return View(productGroup);
        }

        // POST: Admin/ProductGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var productGroup = db.ProductGroups.Find(id);

            // Removing Group Brands
            var productGroupBrands = db.ProductGroupBrands
                .Where(b => b.ProductGroupId == productGroup.Id).ToList();
            foreach (var item in productGroupBrands)
                db.ProductGroupBrands.Remove(item);

            db.SaveChanges();

            // Removing Previous Group Features
            var productGroupFeatures = db.ProductGroupFeatures
                .Where(b => b.ProductGroupId == productGroup.Id).ToList();

            foreach (var item in productGroupFeatures)
                db.ProductGroupFeatures.Remove(item);

            db.SaveChanges();

            if (productGroup.Children.Any())
            {
                foreach (var subgroup in db.ProductGroups.Where(g => g.ParentId == id))
                {
                    db.ProductGroups.Remove(subgroup);
                }
            }
            db.ProductGroups.Remove(productGroup);
            db.SaveChanges();
            return RedirectToAction("Index", new { Id = productGroup.ParentId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        [HttpGet]
        public ActionResult GetSubGroups(int id)
        {
            var subgroupList = db.ProductGroups.Where(c => c.ParentId == id)
                .Where(w => w.ParentId == id).ToList();
            return Json(subgroupList, JsonRequestBehavior.AllowGet);
        }
    }
}
