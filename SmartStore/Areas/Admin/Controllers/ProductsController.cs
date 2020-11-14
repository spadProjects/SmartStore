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
            prolist = db.Products.Include(p => p.Brand).Include(p => p.ProductGroup).OrderByDescending(p => p.ProductOrder).ToList();
            return View(prolist.ToPagedList(PageNumber, pagesize));
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands.OrderBy(b => b.BrandOrder), "Id", "BrandName");
            var ProductGroups = db.ProductGroups.Where(pg => pg.ParentId == null).ToList();
            //ViewBag.ProductSubGroupId = new SelectList(db.ProductGroups.Where(g => g.ParentId != null), "GroupId", "GroupName");

            var model = new ProductViewModel
            {
                ProductGroupList = ProductGroups

            };

            return View(model);
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product entity)
        {
            if (ModelState.IsValid)
            {

                //product.ProductGroupId = 2;
                entity.InsertUser = "admin";
                entity.InsertDate = DateTime.Now;
                //entity.BrandId = 1;
                db.Products.Add(entity);
                db.SaveChanges();

                return Json(entity, JsonRequestBehavior.AllowGet); //message
            }
            //ViewBag.BrandId = new SelectList(db.Brands, "Id", "BrandName", product.BrandId);
            //ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "GroupId", "GroupName", product.ProductGroup);
            return Json("false", JsonRequestBehavior.AllowGet); //message
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
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "BrandName", product.BrandId);
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "Id", "GroupName", product.ProductGroupId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductGroupId,BrandId,ProductName,ProductImg,ProductPrice,ProductDiscountPercent,ProductDescription,ProductOrder,ProductNotShow")] Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999999).ToString();

                    file.SaveAs(HttpContext.Server.MapPath("~/Images/Product/") + imgcode.ToString() + "-" + file.FileName);
                    product.ProductImg = imgcode.ToString() + "-" + file.FileName;
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "BrandName", product.BrandId);
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "Id", "GroupName", product.ProductGroupId);
            return View(product);
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
            db.Products.Remove(product);
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
