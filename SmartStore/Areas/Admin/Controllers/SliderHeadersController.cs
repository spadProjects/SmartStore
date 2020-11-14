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
    public class SliderHeadersController : Controller
    {
        private SmartStoreContext db = new SmartStoreContext();

        // GET: Admin/SliderHeaders
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

            List<SmartStore.Model.Entities.SliderHeader> sliderheader = new List<SliderHeader>();
            sliderheader = db.SliderHeaders.ToList();
            return View(sliderheader.ToPagedList(PageNumber, pagesize));
        }

        // GET: Admin/SliderHeaders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderHeader sliderHeader = db.SliderHeaders.Find(id);
            if (sliderHeader == null)
            {
                return HttpNotFound();
            }
            return View(sliderHeader);
        }

        // GET: Admin/SliderHeaders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SliderHeaders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SliderTitle,SliderSubTitle,SliderDescription,SliderImg,SliderUrl,SliderOrder,SliderNotShow")] SliderHeader sliderHeader, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999999).ToString();

                    file.SaveAs(HttpContext.Server.MapPath("~/Images/SliderHeader/") + imgcode.ToString() + "-" + file.FileName);
                    sliderHeader.SliderImg = imgcode.ToString() + "-" + file.FileName;
                }
                sliderHeader.InsertUser = "Admin";
                sliderHeader.InsertDate = DateTime.Now;
                sliderHeader.UpdateUser = "Admin";
                sliderHeader.UpdateDate = DateTime.Now;
                sliderHeader.IsArchived = false;
                sliderHeader.IsDeleted = false;
                db.SliderHeaders.Add(sliderHeader);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sliderHeader);
        }

        // GET: Admin/SliderHeaders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderHeader sliderHeader = db.SliderHeaders.Find(id);
            if (sliderHeader == null)
            {
                return HttpNotFound();
            }
            return View(sliderHeader);
        }

        // POST: Admin/SliderHeaders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SliderTitle,SliderSubTitle,SliderDescription,SliderImg,SliderUrl,SliderOrder,SliderNotShow")] SliderHeader sliderHeader, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    Random random = new Random();
                    string imgcode = random.Next(100000, 999999).ToString();

                    file.SaveAs(HttpContext.Server.MapPath("~/Images/SliderHeader/") + imgcode.ToString() + "-" + file.FileName);
                    sliderHeader.SliderImg = imgcode.ToString() + "-" + file.FileName;
                }
                sliderHeader.InsertUser = "Admin";
                sliderHeader.InsertDate = DateTime.Now;
                sliderHeader.UpdateUser = "Admin";
                sliderHeader.UpdateDate = DateTime.Now;
                sliderHeader.IsArchived = false;
                sliderHeader.IsDeleted = false;
                db.Entry(sliderHeader).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sliderHeader);
        }

        // GET: Admin/SliderHeaders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderHeader sliderHeader = db.SliderHeaders.Find(id);
            if (sliderHeader == null)
            {
                return HttpNotFound();
            }
            return View(sliderHeader);
        }

        // POST: Admin/SliderHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SliderHeader sliderHeader = db.SliderHeaders.Find(id);
            db.SliderHeaders.Remove(sliderHeader);
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
