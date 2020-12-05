using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartStore.DataAccess.Context;
using SmartStore.Model.Entities;
using PagedList;

namespace SmartStore.Areas.Admin.Controllers
{
    public class FactorsController : Controller
    {
        // GET: Admin/Factors
        SmartStoreContext db = new SmartStoreContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FactorList(int? Page, int? pageSize)
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

            List<SmartStore.Model.Entities.Factor> factorlist = new List<Factor>();
            factorlist = db.Factors.ToList();
            return View(factorlist.ToPagedList(PageNumber, pagesize));
        }

        public ActionResult FactorListDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factor factor = db.Factors.Find(id);
            if (factor == null)
            {
                return HttpNotFound();
            }
            return View(factor);
        }
    }
}