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
using PagedList;
using SmartStore.Models;

namespace SmartStore.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private SmartStoreContext db = new SmartStoreContext();

        // GET: Admin/Products
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

            List<SmartStore.Model.Entities.Product> prolist = new List<Product>();
            prolist = db.Products.Where
                (p=> p.IsDeleted != true).Include(p => p.Brand).Include(p => p.ProductGroup).OrderByDescending(p => p.ProductOrder).ToList();
            return View(prolist.ToPagedList(PageNumber, pagesize));
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.ProductGroups = db.ProductGroups.ToList();
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public int? Create(NewProductViewModel entity)
        {
            if (!ModelState.IsValid) return null;
            var prod = new Product();
            prod.ProductName = entity.ProductName;
            prod.ProductShortDescription = entity.ProductShortDescription;
            prod.ProductDescription = HttpUtility.UrlDecode(entity.ProductDescription, System.Text.Encoding.Default);
            prod.BrandId = entity.Brand;
            prod.ProductGroupId = entity.ProductGroup;
            prod.ProductPrice = entity.ProductPrice;
            prod.ProductDiscountPercent = entity.ProductDiscountPercent;
            prod.Point = entity.Point;
            var addProduct = db.Products.Add(prod);
            db.SaveChanges();
            #region Adding Product Features

            foreach (var feature in entity.ProductFeatures)
            {
                if (feature.IsMain)
                {
                    var model = new ProductMainFeature();
                    model.ProductId = addProduct.Id;
                    model.FeatureId = feature.FeatureId;
                    model.SubFeatureId = feature.SubFeatureId;
                    model.Value = feature.Value;
                    model.Quantity = feature.Quantity ?? 0;
                    model.Price = feature.Price ?? 0;
                    db.ProductMainFeatures.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    var model = new ProductFeatureValue();
                    model.ProductId = addProduct.Id;
                    model.FeatureId = feature.FeatureId;
                    model.SubFeatureId = feature.SubFeatureId;
                    model.Value = feature.Value;
                    db.ProductFeatureValues.Add(model);
                    db.SaveChanges();
                }
            }
            #endregion
            return addProduct.Id;
        }


        [HttpPost]
        public ActionResult SaveMessage()
        {
            //if (file != null)
            //{
            //    Random random = new Random();
            //    string imgcode = random.Next(100000, 999999).ToString();

            //    file.SaveAs(HttpContext.Server.MapPath("~/Images/Product/") + imgcode.ToString() + "-" + file.FileName);
            //    //product.ProductImg = imgcode.ToString() + "-" + file.FileName;                
            //}

            try
            {
                //  Get all files from Request object  
                HttpFileCollectionBase files = Request.Files;
                var title = Request.Form["Title"].ToString();

                for (int i = 0; i < files.Count; i++)
                {
                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[i];
                    string fname;

                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    // Get the complete folder path and store the file inside it.  
                    //fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);

                    file.SaveAs((HttpContext.Server.MapPath("~/Images/Product/") + title));
                }
                // Returns message that successfully uploaded  
                return Json("File Uploaded Successfully!");
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }

            //return Json("OK", JsonRequestBehavior.AllowGet); //message
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductGroups = db.ProductGroups.ToList();
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public int? Edit(NewProductViewModel product)
        {
            if (!ModelState.IsValid) return null;
            var prod = db.Products.Find(product.Id.Value);
            prod.ProductName = product.ProductName;
            prod.ProductShortDescription = product.ProductShortDescription;
            prod.ProductDescription = prod.ProductDescription;
            prod.BrandId = product.Brand;
            prod.ProductGroupId = product.ProductGroup;
            prod.ProductPrice = product.ProductPrice;
            prod.ProductDiscountPercent = product.ProductDiscountPercent;
            prod.Point = product.Point;
            db.Entry(prod).State = EntityState.Modified;
            db.SaveChanges();
            #region Removing Previous Product Features
            var productMainFeatures = db.ProductMainFeatures.Where(f=>f.ProductId == prod.Id).ToList();
            foreach (var mainFeature in productMainFeatures)
                db.ProductMainFeatures.Remove(mainFeature);
            db.SaveChanges();
            var productFeatures = db.ProductFeatureValues.Where(f => f.ProductId == prod.Id).ToList();
            foreach (var feature in productFeatures)
                db.ProductFeatureValues.Remove(feature);
            db.SaveChanges();
            #endregion

            #region Adding Product Features

            foreach (var feature in product.ProductFeatures)
            {
                if (feature.IsMain)
                {
                    var model = new ProductMainFeature();
                    model.ProductId = prod.Id;
                    model.FeatureId = feature.FeatureId;
                    model.SubFeatureId = feature.SubFeatureId;
                    model.Value = feature.Value;
                    model.Quantity = feature.Quantity ?? 0;
                    model.Price = feature.Price ?? 0;
                    db.ProductMainFeatures.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    var model = new ProductFeatureValue();
                    model.ProductId = prod.Id;
                    model.FeatureId = feature.FeatureId;
                    model.SubFeatureId = feature.SubFeatureId;
                    model.Value = feature.Value;
                    db.ProductFeatureValues.Add(model);
                    db.SaveChanges();
                }
            }
            #endregion
            return prod.Id;

        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return PartialView(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            //var productMainFeatures = db.ProductMainFeatures.Where(f => f.ProductId == id).ToList();
            //var productFeatures = db.ProductFeatureValues.Where(f => f.ProductId == id).ToList();
            //foreach (var mainFeature in productMainFeatures)
            //    db.ProductMainFeatures.Remove(mainFeature);
            //db.SaveChanges();

            //foreach (var feature in productFeatures)
            //    db.ProductFeatureValues.Remove(feature);
            //db.SaveChanges();

            //#region Delete Product Image
            //if (product.ProductImg != null)
            //{
            //    if (System.IO.File.Exists(Server.MapPath("/Images/Product/" + product.ProductImg)))
            //        System.IO.File.Delete(Server.MapPath("/Images/Product/" + product.ProductImg));
            //}
            //#endregion
            product.IsDeleted = true;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult ProductDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpPost]
        public bool UploadImage(int id, HttpPostedFileBase File)
        {
            #region Upload Image
            if (File != null)
            {
                var product = db.Products.Find(id);
                if (product.ProductImg != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Images/Product/" + product.ProductImg)))
                        System.IO.File.Delete(Server.MapPath("/Images/Product/" + product.ProductImg));
                }
                // Saving Image
                var newFileName = Guid.NewGuid() + Path.GetExtension(File.FileName);
                File.SaveAs(Server.MapPath("/Images/Product/" + newFileName));

                product.ProductImg = newFileName;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return true;

            }
            #endregion

            return false;
        }
        public JsonResult GetProductGroupFeatures(int id)
        {
            var pgFeatures = db.ProductGroupFeatures.Where(f=>f.ProductGroupId == id).ToList();
            var features = pgFeatures.Select(item => db.Features.FirstOrDefault(f => f.Id == item.FeatureId)).ToList();
            var obj = features.Select(item => new FeaturesObjViewModel() { Id = item.Id, Title = item.FeatureTitle }).ToList();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductFeatures(int id)
        {
            var mainFeatures = db.ProductMainFeatures.Where(f=>f.ProductId == id).ToList();
            var features = db.ProductFeatureValues.Where(f => f.ProductId == id).ToList();
            var obj = mainFeatures.Select(mainFeature => new ProductFeaturesViewModel()
            {
                ProductId = mainFeature.ProductId,
                FeatureId = mainFeature.FeatureId.Value,
                SubFeatureId = mainFeature.SubFeatureId,
                IsMain = true,
                Value = mainFeature.Value,
                Quantity = mainFeature.Quantity,
                Price = mainFeature.Price
            })
                .ToList();
            obj.AddRange(features.Select(feature => new ProductFeaturesViewModel()
            {
                ProductId = feature.ProductId,
                FeatureId = feature.FeatureId.Value,
                Value = feature.Value,
                IsMain = false,
                SubFeatureId = feature.SubFeatureId
            }));

            return Json(obj.GroupBy(a => a.FeatureId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductGroupBrands(int id)
        {
            var pgBrands = db.ProductGroupBrands.Where(f => f.ProductGroupId == id).ToList();
            var brands = pgBrands.Select(item => db.Brands.FirstOrDefault(f => f.Id == item.BrandId)).ToList();
            var obj = brands.Select(item => new BrandsObjViewModel() { Id = item.Id, Name = item.BrandName }).ToList();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFeatureSubFeatures(int id)
        {
            var subFeatures = db.SubFeatures.Where(s=>s.FeatureId == id).ToList();
            var obj = subFeatures.Select(item => new SubFeaturesObjViewModel() { Id = item.Id, Value = item.Value }).ToList();
            return Json(obj, JsonRequestBehavior.AllowGet);
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
