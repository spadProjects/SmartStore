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
    public class ProductGroupsController : Controller
    {
        private SmartStoreContext db = new SmartStoreContext();

        // GET: Admin/ProductGroups
        public ActionResult Index(int? id = null)
        {
            Session.Add("GroupId", id);
            var productgroups = db.ProductGroups.Where(g => g.ParentId == id);
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
        public ActionResult Create(int? id)
        {
            //ViewBag.GroupId = new SelectList(db.ProductGroups.OrderBy(pg => pg.GroupOrder), "GroupId", "GroupName");
            return PartialView(new ProductGroup()
            {
                ParentId = id
            });
        }

        // POST: Admin/ProductGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupId,GroupImage,GroupName,GroupOrder,GroupNotShow,ParentId")] ProductGroup productGroup, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    Random randomm = new Random();
                    string imgcode = randomm.Next(100000, 999999).ToString();

                    file.SaveAs(HttpContext.Server.MapPath("~/Images/ProductGroup/") + imgcode.ToString() + "-" + file.FileName);
                    productGroup.GroupImage = imgcode.ToString() + "-" + file.FileName;
                }
                var sessionParentId = HttpContext.Session["GroupId"]?.ToString();


                if (!string.IsNullOrEmpty(sessionParentId))
                    productGroup.ParentId = Convert.ToInt32(sessionParentId);
                productGroup.InsertUser = "Admin";
                productGroup.InsertDate = DateTime.Now;
                productGroup.UpdateUser = "Admin";
                productGroup.UpdateDate = DateTime.Now;
                productGroup.IsArchived = false;
                productGroup.IsDeleted = false;
                db.ProductGroups.Add(productGroup);
                db.SaveChanges();
                return RedirectToAction("Index", new { Id = productGroup.ParentId });
            }

            return View(productGroup);
        }

        // GET: Admin/ProductGroups/Edit/5
        public ActionResult Edit(int? id)
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
            return PartialView(productGroup);
        }

        // POST: Admin/ProductGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GroupImage,GroupName,GroupOrder,GroupNotShow,ParentId")] ProductGroup productGroup, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    Random randomm = new Random();
                    string imgcode = randomm.Next(100000, 999999).ToString();

                    file.SaveAs(HttpContext.Server.MapPath("~/Images/ProductGroup/") + imgcode.ToString() + "-" + file.FileName);
                    productGroup.GroupImage = imgcode.ToString() + "-" + file.FileName;
                }
                var sessionParentId = HttpContext.Session["GroupId"]?.ToString();


                if (!string.IsNullOrEmpty(sessionParentId))
                    productGroup.ParentId = Convert.ToInt32(sessionParentId);
                productGroup.InsertUser = "Admin";
                productGroup.InsertDate = DateTime.Now;
                productGroup.UpdateUser = "Admin";
                productGroup.UpdateDate = DateTime.Now;
                productGroup.IsArchived = false;
                productGroup.IsDeleted = false;
                db.Entry(productGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { Id = productGroup.ParentId });
            }
            return View(productGroup);
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
            if (productGroup.ProductGroup1.Any())
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
