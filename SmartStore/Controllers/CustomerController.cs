﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SmartStore.DataAccess.Context;
using SmartStore.Model.Entities;
using SmartStore.Models;
using PagedList;
using SmartStore.Classes;

namespace SmartStore.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        SmartStoreContext db = new SmartStoreContext();

        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SideNav()
        {
            var user = db.Users.FirstOrDefault(u => u.UserEmail == User.Identity.Name);
            var userProfitManager = new UserProfitManager(db);
            var userPoint = userProfitManager.GetUserPointsAmount(user);
            //var userProfit = userProfitManager.CalculateUserProfit(user);
            ViewBag.User = user;
            ViewBag.UserPoint = userPoint;
            //ViewBag.UserProfit = userProfit;
            return View();
        }

        public ActionResult TopNavbar()
        {
            return View();
        }

        public ActionResult FactorList(int? id, int? Page, int? pageSize)
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
            factorlist = db.Factors.Where(m => m.UserId == id).ToList();
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

        public ActionResult ProfileEdit(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileEdit([Bind(Include = "Id,RoleId,UserCode,UserFirstName,UserLastName,UserFatherName,UserImage,UserDateofBirth,UserNationalCode,UserPostalCode,UserPhoneNumber,UserEmail,UserPassword,UserVerificationCode,UserIdentifierCode,IsActive,UserAddress,UserLandlinePhone")] User user, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
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
                user.UserPassword = user.UserPassword;
                user.UserVerificationCode = MyVerificationCode.ToString();
                user.UserCode = MyUserCode.ToString();
                user.RoleId = user.RoleId;
                user.IsActive = user.IsActive;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", user.RoleId);
            return View(user);
        }

        public ActionResult IdentifierCodeSearch(string strsearch)
        {
            ViewBag.strsearch = strsearch;
            var us = db.Users.Where(u => u.UserCode.Equals(strsearch)).ToList();
            ViewBag.IsFull = false;
            if (db.Users.Where(u => u.UserIdentifierCode.Equals(strsearch)).Count() >= 2)
            {
                ViewBag.IsFull = true;
            }
            return View(us);
        }

        public ActionResult IdentifierCodeEdit(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IdentifierCodeEdit([Bind(Include = "Id,RoleId,UserCode,UserFirstName,UserLastName,UserFatherName,UserImage,UserDateofBirth,UserNationalCode,UserPostalCode,UserPhoneNumber,UserEmail,UserPassword,UserVerificationCode,UserIdentifierCode,IsActive,UserAddress,UserLandlinePhone")] User user, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
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
        public ActionResult IntroducersChart()
        {
            var userProfitManager = new UserProfitManager(db);
            var user = db.Users.FirstOrDefault(u => u.UserEmail == User.Identity.Name);
            var userCard = new IntroducersCardViewModel()
            {
                Id = user.Id,
                UserImage = user.UserImage,
                Name = $"{user.UserFirstName} {user.UserLastName}",
                Role = user.Role.RoleTitle,
                Points = userProfitManager.GetUserPointsAmount(user)
            };

            var parent = db.Users.FirstOrDefault(u => u.UserCode == user.UserIdentifierCode);
            var parentCard = new IntroducersCardViewModel()
            {
                Id = parent.Id,
                UserImage = parent.UserImage,
                Name = $"{parent.UserFirstName} {parent.UserLastName}",
                Role = parent.Role.RoleTitle,
            };

            var children = db.Users.Where(u => u.UserIdentifierCode == user.UserCode).ToList();
            var childrenCard = new List<IntroducersCardViewModel>();
            foreach (var child in children)
            {
                var childCard = new IntroducersCardViewModel()
                {
                    Id = child.Id,
                    UserImage = child.UserImage,
                    Name = $"{child.UserFirstName} {child.UserLastName}",
                    Role = child.Role.RoleTitle,
                    Points = userProfitManager.GetUserPointsAmount(child)
                };
                childrenCard.Add(childCard);
            }

            var introducersChartVm = new IntroducersChartViewModel();
            introducersChartVm.User = userCard;
            introducersChartVm.Parent = parentCard;
            introducersChartVm.Children = childrenCard;
            return View(introducersChartVm);
        }
        public string RemoveSubset(int id)
        {
            try
            {
                var child = db.Users.Find(id);
                child.UserIdentifierCode = null;
                db.Entry(child).State = EntityState.Modified;
                db.SaveChanges();
                return "True";
            }
            catch (Exception)
            {
                return "Failed to Remove the Subset";
            }

        }
        public ActionResult IdentifierCodeConfirm(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IdentifierCodeConfirm(int id)
        {
            var user = db.Users.Find(id);
            var us = db.Users.FirstOrDefault(u => u.UserEmail == User.Identity.Name);
            us.UserIdentifierCode = user.UserCode;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel change)
        {
            string HashPass = FormsAuthentication.HashPasswordForStoringInConfigFile(change.UserOldPassword, "MD5");

            var user = db.Users.FirstOrDefault(u => u.UserEmail == User.Identity.Name && u.UserPassword == HashPass);

            if (user != null)
            {
                user.UserPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(change.UserPassword, "Md5");

                db.SaveChanges();

                return Redirect("/Account/RegisterWithEmail");
            }
            else
            {
                ModelState.AddModelError("UserOldPassword", "Incorrect password entered");
            }
            return View(change);
        }
    }
}