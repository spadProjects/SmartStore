using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SmartStore.DataAccess.Context;
using SmartStore.Model.Entities;
using PagedList;
using SmartStore.Models;
using SmartStore.Classes;

namespace SmartStore.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private SmartStoreContext db = new SmartStoreContext();

        // GET: Admin/Users
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

            List<SmartStore.Model.Entities.User> users = new List<User>();
            users = db.Users.Include(u => u.Role).ToList();
            var vm = new List<UserTableViewModel>();
            foreach (var user in users)
                vm.Add(new UserTableViewModel() { 
                    User = user,
                    HasTwoSubsets = db.Users.Count(u=>u.UserIdentifierCode == user.UserCode) < 2? false: true
                });;

            return View(vm.ToPagedList(PageNumber, pagesize));
        }
        public ActionResult ProfitPayment(int id)
        {
            var userProfitManager = new UserProfitManager();
            ViewBag.UserId = id;
            ViewBag.PreviousPayments = db.UserProfitPayments.Where(u => u.UserId == id).ToList();
            ViewBag.UnpayedProfit = userProfitManager.CalculateUserProfit(id);
            return View();
        }
        [HttpPost]
        public ActionResult ProfitPayment(UserProfitPayment payment)
        {
            try
            {
                payment.Date = DateTime.Now;
                db.UserProfitPayments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                var userProfitManager = new UserProfitManager();
                ViewBag.UserId = payment.UserId;
                ViewBag.PreviousPayments = db.UserProfitPayments.Where(u => u.UserId == payment.UserId).ToList();
                ViewBag.UnpayedProfit = userProfitManager.CalculateUserProfit(payment.UserId);
                return View();
            }
        }
        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName");
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoleId,UserCode,UserFirstName,UserLastName,UserFatherName,UserImage,UserDateofBirth,UserNationalCode,UserPostalCode,UserPhoneNumber,UserEmail,UserPassword,UserVerificationCode,UserIdentifierCode,IsActive,UserAddress,UserLandlinePhone")] User user, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (!db.Users.Any(u => u.UserPhoneNumber == user.UserPhoneNumber && u.UserEmail == user.UserEmail))
                {
                    if (file != null)
                    {
                        Random randomm = new Random();
                        string imgcode = randomm.Next(100000, 999999).ToString();

                        file.SaveAs(HttpContext.Server.MapPath("~/Images/User/") + imgcode.ToString() + "-" + file.FileName);
                        user.UserImage = imgcode.ToString() + "-" + file.FileName;
                    }

                    Random random = new Random();
                    int MyUserCode = random.Next(10000000, 99000000);
                    int MyVerificationCode = 0;
                    bool isValidCod = false;
                    while (!isValidCod)
                    {
                        Random random1 = new Random();

                        MyVerificationCode = random1.Next(100000, 990000);

                        var userverificationcode = db.Users.FirstOrDefault(j => j.UserVerificationCode == MyVerificationCode.ToString());
                        if (userverificationcode == null)
                        {
                            isValidCod = true;
                        }
                    }

                    User usr = new User()
                    {
                        IsActive = user.IsActive,
                        RoleId = user.RoleId,
                        UserCode = MyUserCode.ToString(),
                        UserPhoneNumber = user.UserPhoneNumber,
                        UserPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(user.UserPassword, "MD5"),
                        UserVerificationCode = MyVerificationCode.ToString(),
                        UserFirstName = user.UserFirstName,
                        UserLastName = user.UserLastName,
                        UserEmail = user.UserEmail,
                        UserAddress = user.UserAddress,
                        UserIdentifierCode = user.UserIdentifierCode,
                        UserDateofBirth = user.UserDateofBirth,
                        UserFatherName = user.UserFatherName,
                        UserImage = user.UserImage,
                        UserLandlinePhone = user.UserLandlinePhone,
                        UserNationalCode = user.UserNationalCode,
                        UserPostalCode = user.UserPostalCode,
                    };

                    db.Users.Add(usr);
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("UserPhoneNumber", "You cannot register with this mobile number");
                    ModelState.AddModelError("UserEmail", "You cannot register with this Email");
                }

                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleTitle", user.RoleId);
            return PartialView(user);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", user.RoleId);
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoleId,UserCode,UserFirstName,UserLastName,UserFatherName,UserImage,UserDateofBirth,UserNationalCode,UserPostalCode,UserPhoneNumber,UserEmail,UserPassword,UserVerificationCode,UserIdentifierCode,IsActive,UserAddress,UserLandlinePhone")] User user, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string MyHash = FormsAuthentication.HashPasswordForStoringInConfigFile(user.UserPassword, "MD5");
                Random random = new Random();
                int MyUserCode = random.Next(10000000, 99000000);
                int MyVerificationCode = 0;
                bool isValidCod = false;
                while (!isValidCod)
                {
                    Random random1 = new Random();

                    MyVerificationCode = random1.Next(100000, 990000);

                    var userverificationcode = db.Users.FirstOrDefault(j => j.UserVerificationCode == MyVerificationCode.ToString());
                    if (userverificationcode == null)
                    {
                        isValidCod = true;
                    }
                }
                if (file != null)
                {
                    Random randomm = new Random();
                    string imgcode = randomm.Next(100000, 999999).ToString();

                    file.SaveAs(HttpContext.Server.MapPath("~/Images/User/") + imgcode.ToString() + "-" + file.FileName);
                    user.UserImage = imgcode.ToString() + "-" + file.FileName;
                }
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", user.RoleId);
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
