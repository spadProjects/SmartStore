using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartStore.DataAccess.Context;
using SmartStore.Model.Entities;
using SmartStore.Models;
using System.Web.Security;
using SmartStore.Classes;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using PagedList;
using System.Net;

namespace SmartStore.Controllers
{
    public class HomeController : Controller
    {
        SmartStoreContext db = new SmartStoreContext();

        // GET: Home
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ActionResult HeaderNavbar()
        {
            return PartialView();
        }

        public ActionResult SliderHeader()
        {
            var headerslider = db.SliderHeaders.Where(m => m.SliderNotShow == false).OrderByDescending(m => m.SliderOrder).Take(4).ToList();
            return PartialView(headerslider);
        }

        //public ActionResult Group()
        //{
        //    List<SmartStore.Model.Entities.Group> subs = new List<Group>();
        //    subs = db.Groups.Where(w => w.GroupNotShow == false).OrderBy(s => s.GroupOrder).Take(10).ToList();
        //    return PartialView(subs);
        //}

        //public ActionResult SubGroup(int? id)
        //{
        //    List<SmartStore.Model.Entities.SubGroup> subs = new List<SubGroup>();
        //    subs = db.SubGroups.Where(w => w.SubGroupNotShow == false && w.GroupId == id).OrderBy(s => s.SubGroupOrder).Take(10).ToList();
        //    return PartialView(subs);
        //}

        public ActionResult ProductSlider()
        {
            //List<SmartStore.Models.ShapCartViewModel> list = new List<SmartStore.Models.ShapCartViewModel>();

            //if (Session["ShopCart"] != null)
            //{
            //    List<ShopCartItem> cart = Session["ShopCart"] as List<ShopCartItem>;
            //    foreach (var shopCartItem in cart)
            //    {
            //        var product = db.Products.Find(shopCartItem.Id);
            //        list.Add(new SmartStore.Models.ShapCartViewModel()
            //        {
            //            ProductId = shopCartItem.ProductId,
            //            Count = shopCartItem.Count,
            //            ProductName = product.ProductName,
            //            ProductPrice = product.ProductPrice,
            //            Sum = shopCartItem.Count * product.ProductPrice,
            //            ProductImg = product.ProductImg

            //        });
            //    }
            //}

            //return PartialView(list);
            var productslider = db.Products.Where(ps => ps.ProductNotShow == false).OrderByDescending(ps => ps.ProductOrder).ToList();
            return PartialView(productslider);
            //List<SmartStore.Model.Entities.Product> prolist = new List<Product>();
            //prolist = db.Products.Where(m => m.ProductNotShow == false).OrderBy(m => m.ProductOrder).Take(8).ToList();
            //return View(prolist);
        }

        public ActionResult MainSlider()
        {
            var mainslider = db.Products.Where(ms => ms.ProductNotShow == false).OrderByDescending(ms => ms.ProductOrder).ToList();
            return PartialView(mainslider);
            //var mainslider = db.Products.Where(m => m.ProductNotShow == false).OrderBy(m => m.ProductOrder).Take(4).ToList();
            //return PartialView(mainslider);
        }

        public ActionResult Gallery()
        {
            List<SmartStore.Model.Entities.Product> gallerylist = new List<Product>();
            gallerylist = db.Products.Where(m => m.ProductNotShow == false).OrderBy(m => m.ProductOrder).Take(8).ToList();
            return PartialView(gallerylist);
        }

        //public ActionResult Mega()
        //{
        //    List<SmartStore.Model.Entities.Group> subs = new List<Group>();
        //    subs = db.Groups.Where(w => w.GroupNotShow == false).OrderBy(s => s.GroupOrder).Take(10).ToList();
        //    return PartialView(subs);
        //}

        //public ActionResult MegaMenu(int? id)
        //{
        //    List<SmartStore.Model.Entities.SubGroup> subs = new List<SubGroup>();
        //    subs = db.SubGroups.Where(w => w.SubGroupNotShow == false && w.GroupId == id).OrderBy(s => s.SubGroupOrder).Take(10).ToList();
        //    return PartialView(subs);
        //}

        public ActionResult SocialNetWork()
        {
            var socialnetwork = db.SocialNetWorks.Where(s => s.NotShow == false).OrderBy(s => s.Order).Take(4).ToList();
            return PartialView(socialnetwork);
        }

        public ActionResult ContactUs()
        {
            var contactus = db.ContactUss.Where(s => s.NotShow == false).ToList();
            return PartialView(contactus);
        }

        public ActionResult FooterAboutUs()
        {
            var footeraboutus = db.ContactUss.Where(s => s.NotShow == false).ToList();
            return PartialView(footeraboutus);
        }

        public ActionResult Blog(int? Page, int? pageSize)
        {
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="20", Text= "20" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="30", Text= "30" },
            };


            int pagesize = (pageSize ?? 5);
            int PageNumber = (Page ?? 1);

            List<SmartStore.Model.Entities.Blog> bloglist = new List<Blog>();
            bloglist = db.Blogs.Where(m => m.NotShow == false).OrderBy(m => m.Order).ToList();
            return View(bloglist.ToPagedList(PageNumber, pagesize));
        }

        public ActionResult PopularBlog()
        {
            var popularblog = db.Blogs.Where(b => b.NotShow == false).OrderByDescending(b => b.Order).Take(3).ToList();
            return PartialView(popularblog);
        }

        public ActionResult Categories()
        {
            var categories = db.ProductGroups.Where(b => b.GroupNotShow == false).OrderByDescending(b => b.GroupOrder).Take(3).ToList();
            return PartialView(categories);
        }

        public ActionResult LatestProducts()
        {
            var latestproducts = db.Products.Where(b => b.ProductNotShow == false).OrderByDescending(b => b.ProductOrder).Take(3).ToList();
            return PartialView(latestproducts);
        }

        public ActionResult BlogDetails(int? id, string title)
        {
            var blog = db.Blogs.Find(id);

            return View(blog);
        }

        public ActionResult RelatedBlogPosts()
        {
            var relatedblogposts = db.Blogs.Where(b => b.NotShow == false).OrderByDescending(b => b.Order).Take(2).ToList();
            return PartialView(relatedblogposts);
        }

        public ActionResult LatestBlog()
        {
            var latestblog = db.Blogs.Where(b => b.NotShow == false).OrderByDescending(b => b.Order).Take(3).ToList();
            return PartialView(latestblog);
        }

        public ActionResult FooterList()
        {
            return PartialView();
        }

        public ActionResult FeaturedCollection()
        {
            List<SmartStore.Model.Entities.ProductGroup> sg = new List<ProductGroup>();
            sg = db.ProductGroups.Where(w => w.GroupNotShow == false).OrderBy(s => s.GroupOrder).Take(6).ToList();

            sg.ForEach(item =>
            {
                var subGroup = db.ProductGroups.Where(w => w.ParentId == item.Id).ToList();
                subGroup.ForEach(sub =>
                {
                    item.Children.Add(sub);
                });
            });

            return PartialView(sg);
        }

        public ActionResult FeaturedCollectionGallery()
        {
            List<SmartStore.Model.Entities.ProductGroup> sg = new List<ProductGroup>();
            sg = db.ProductGroups.Where(w => w.GroupNotShow == false).OrderBy(s => s.GroupNotShow).Take(6).ToList();
            return PartialView(sg);
        }

        public ActionResult PageAboutUs()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return PartialView(db.ContactUss.Where(c => c.NotShow == false));
        }

        public ActionResult PopularCategories()
        {
            List<SmartStore.Model.Entities.ProductGroup> pc = new List<ProductGroup>();
            pc = db.ProductGroups.Where(w => w.GroupNotShow == false).OrderByDescending(s => s.GroupOrder).Take(4).ToList();
            return PartialView(pc);
        }

        public ActionResult ShowCategory()
        {
            var model = new List<ProductGroupViewModel>();

            var groupList = db.ProductGroups.Where(w => w.ParentId == null).ToList();

            groupList.ForEach(item =>
            {
                var GroupHeader = new ProductGroupViewModel();
                GroupHeader.Title = item.GroupName;
                GroupHeader.GroupId = item.Id;

                var subGroupList = db.ProductGroups.Where(w => w.ParentId == item.Id).ToList();

                subGroupList.ForEach(subItem =>
                {
                    var Groups = new ProductGroupViewModel();
                    Groups.Title = subItem.GroupName;
                    Groups.GroupId = subItem.Id;

                    var subItemGroupList = db.ProductGroups.Where(w => w.ParentId == subItem.Id).ToList();

                    subItemGroupList.ForEach(sub =>
                    {
                        var subgroup = new ProductGroupViewModel();
                        subgroup.GroupId = sub.Id;
                        subgroup.Title = sub.GroupName;

                        Groups.SubGroups.Add(subgroup);
                    });

                    GroupHeader.SubGroups.Add(Groups);
                });

                model.Add(GroupHeader);
            });
            return PartialView(model);
        }

    }
}