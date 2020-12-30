using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartStore.Classes;
using SmartStore.DataAccess.Context;
using SmartStore.Model.Entities;
using SmartStore.Models;

namespace SmartStore.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        SmartStoreContext db = new SmartStoreContext();
        // GET: Admin/Dashboard
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AdminIntroducersChart()
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

            //var parent = db.Users.FirstOrDefault(u => u.UserCode == user.UserIdentifierCode);
            //var parentCard = new IntroducersCardViewModel()
            //{
            //    Id = parent.Id,
            //    UserImage = parent.UserImage,
            //    Name = $"{parent.UserFirstName} {parent.UserLastName}",
            //    Role = parent.Role.RoleTitle,
            //};

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
            //introducersChartVm.Parent = parentCard;
            introducersChartVm.Children = childrenCard;
            return View(introducersChartVm);
        }
    }
}