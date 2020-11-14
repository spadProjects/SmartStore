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

namespace SmartStore.Areas.Admin.Controllers
{
    public class BrandsController : Controller
    {
        private SmartStoreContext db = new SmartStoreContext();

        // GET: Admin/Brands
        public ActionResult Index(int? Page, int? pageSize)
        {
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="20", Text= "20" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
            };

            int pagesize = (pageSize ?? 5);
            int PageNumber = (Page ?? 1);

            List<SmartStore.Model.Entities.Brand> brandlist = new List<Brand>();
            brandlist = db.Brands.OrderBy(b=>b.BrandOrder).ToList();
            return View(brandlist.ToPagedList(PageNumber, pagesize));
        }

        // GET: Admin/Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BrandName,BrandImg,BrandOrder,BrandNotShow,ParentHistoryId,InsertUser,InsertDate,UpdateUser,UpdateDate,IsArchived,IsDeleted")] Brand brand, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999999).ToString();

                    file.SaveAs(HttpContext.Server.MapPath("~/Images/Brand/") + imgcode.ToString() + "-" + file.FileName);
                    brand.BrandImg = imgcode.ToString() + "-" + file.FileName;
                }
                brand.InsertUser = "Admin";
                brand.InsertDate = DateTime.Now;
                brand.UpdateUser = "Admin";
                brand.UpdateDate = DateTime.Now;
                brand.IsArchived = false;
                brand.IsDeleted = false;
                db.Brands.Add(brand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brand);
        }

        // GET: Admin/Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Admin/Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BrandName,BrandImg,BrandOrder,BrandNotShow")] Brand brand, HttpPostedFileBase file)
        {
            if (file != null)
            {
                Random random = new Random();
                string imgcode = random.Next(100000, 999999).ToString();

                file.SaveAs(HttpContext.Server.MapPath("~/Images/Brand/") + imgcode.ToString() + "-" + file.FileName);
                brand.BrandImg = imgcode.ToString() + "-" + file.FileName;
            }
            brand.InsertUser = "Admin";
            brand.InsertDate = DateTime.Now;
            brand.UpdateUser = "Admin";
            brand.UpdateDate = DateTime.Now;
            brand.IsArchived = false;
            brand.IsDeleted = false;
            db.Entry(brand).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/Brands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Admin/Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brand brand = db.Brands.Find(id);
            db.Brands.Remove(brand);
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
