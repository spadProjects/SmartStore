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
    public class GalleriesController : Controller
    {
        SmartStoreContext db = new SmartStoreContext();

        // GET: Admin/Galleries
        public ActionResult Index(int? id, int? Page, int? pageSize)
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

            List<SmartStore.Model.Entities.Gallery> gallery = new List<Gallery>();
            gallery = db.Galleries.Where(g => g.ProductId == id).Include(u => u.Product).ToList();
            ViewBag.MyId = id;
            return View(gallery.ToPagedList(PageNumber, pagesize));
        }

        // GET: Admin/Galleries/Create
        public ActionResult Create(int? id)
        {
            ViewBag.ProductId = new SelectList(db.Products.Where(p => p.Id == id).ToList(), "Id", "ProductName");
            return PartialView();
        }

        // POST: Admin/Galleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,Image,Order,NotShow")] Gallery gallery, int? id, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999999).ToString();

                    file.SaveAs(HttpContext.Server.MapPath("~/Images/Gallery/") + imgcode.ToString() + "-" + file.FileName);
                    gallery.Image = imgcode.ToString() + "-" + file.FileName;
                }
                gallery.InsertUser = "Admin";
                gallery.InsertDate = DateTime.Now;
                gallery.UpdateUser = "Admin";
                gallery.UpdateDate = DateTime.Now;
                gallery.IsArchived = false;
                gallery.IsDeleted = false;
                db.Galleries.Add(gallery);
                db.SaveChanges();
                return Redirect("/Admin/Galleries/Index/" + id);
            }

            ViewBag.ProductId = new SelectList(db.Products.Where(p => p.Id == id).ToList(), "Id", "ProductName", gallery.ProductId);
            return PartialView(gallery);
        }


        // GET: Admin/Galleries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Admin/Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gallery gallery = db.Galleries.Find(id);
            db.Galleries.Remove(gallery);
            db.SaveChanges();
            return Redirect("/Admin/Galleries/Index/" + gallery.ProductId);
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
