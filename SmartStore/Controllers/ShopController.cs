using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Linq;
using SmartStore.Service.Extensions;

namespace SmartStore.Controllers
{
    public class ShopController : Controller
    {
        SmartStoreContext db = new SmartStoreContext();

        // GET: Shop
        //[Route("ProductDetails/{id}/{title}")]
        public ActionResult ProductDetails(int? id, string title)
        {
            var product = db.Products.Find(id);

            return View(product);
        }

        public ActionResult ImageGallery(int? id)
        {
            List<SmartStore.Model.Entities.Gallery> gallery = new List<Gallery>();
            gallery = db.Galleries.Where(g => g.NotShow == false && g.ProductId == id).OrderBy(g => g.Order).Take(10).ToList();
            return PartialView(gallery);
        }

        public ActionResult ProductMore(int? id)
        {
            var product = db.Products.Find(id);

            return PartialView(product);
        }

        //public ActionResult ProductFeatures(int? id)
        //{
        //    List<SmartStore.Model.Entities.Feature> productfeatures = new List<Feature>();
        //    productfeatures = db.Features.Where(f => f.FeatueNotShow == false && f.ProductId == id).Take(10).ToList();
        //    return PartialView(productfeatures);
        //}

        public ActionResult GroupProductList(int? groupid, int? Page, int? pageSize)
        {
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="12", Text= "12" },
                new SelectListItem() { Value="24", Text= "24" },
                new SelectListItem() { Value="36", Text= "36" },
                new SelectListItem() { Value="48", Text= "48" },
                new SelectListItem() { Value="60", Text= "60" },
                new SelectListItem() { Value="72", Text= "72" },
            };


            int pagesize = (pageSize ?? 12);
            int PageNumber = (Page ?? 1);

            List<SmartStore.Model.Entities.Product> prdlist = new List<Product>();

            prdlist = db.Products.Where(m => m.ProductNotShow == false && m.ProductGroupId == groupid).ToList();
            return View(prdlist.ToPagedList(PageNumber, pagesize));
        }

        public ActionResult SubGroupProductList(int? subgroupid, int? Page, int? pageSize)
        {
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="12", Text= "12" },
                new SelectListItem() { Value="24", Text= "24" },
                new SelectListItem() { Value="36", Text= "36" },
                new SelectListItem() { Value="48", Text= "48" },
                new SelectListItem() { Value="60", Text= "60" },
                new SelectListItem() { Value="72", Text= "72" },
            };


            int pagesize = (pageSize ?? 12);
            int PageNumber = (Page ?? 1);

            List<SmartStore.Model.Entities.Product> prdlist = new List<Product>();
            prdlist = db.Products.Where(m => m.ProductNotShow == false && m.ProductGroupId == subgroupid).OrderBy(m => m.ProductOrder).ToList();
            return View(prdlist.ToPagedList(PageNumber, pagesize));
        }

        public ActionResult Category()
        {
            List<SmartStore.Model.Entities.ProductGroup> grouplist = new List<ProductGroup>();
            grouplist = db.ProductGroups.Where(m => m.GroupNotShow == false).OrderBy(m => m.GroupOrder).Take(8).ToList();
            return PartialView(grouplist);
        }

        public ActionResult Brands()
        {
            List<SmartStore.Model.Entities.Brand> brandlist = new List<Brand>();
            brandlist = db.Brands.Where(m => m.BrandNotShow == false).OrderBy(m => m.BrandOrder).Take(8).ToList();
            return PartialView(brandlist);
        }

        public ActionResult HeaderSlider()
        {
            var headerslider = db.SliderHeaders.Where(m => m.SliderNotShow == false).OrderBy(m => m.SliderOrder).Take(4).ToList();
            return PartialView(headerslider);
        }

        public ActionResult ShoppingCart()
        {
            return View(getListOrder());
        }

        public ActionResult ShoppingCount()
        {
            return PartialView();
        }

        public int AddToCart(int id)
        {
            List<ShopCartItem> cart = new List<ShopCartItem>();

            if (Session["ShopCart"] != null)
            {
                cart = Session["ShopCart"] as List<ShopCartItem>;
            }

            if (cart.Any(p => p.ProductId == id))
            {
                int index = cart.FindIndex(p => p.ProductId == id);
                cart[index].Count += 1;
            }
            else
            {
                cart.Add(new ShopCartItem()
                {
                    ProductId = id,
                    Count = 1
                });
            }

            Session["ShopCart"] = cart;

            return cart.Sum(p => p.Count);
        }
        public int RemoveItemFromCart(int id)
        {
            List<ShopCartItem> cart = new List<ShopCartItem>();

            if (Session["ShopCart"] != null)
            {
                cart = Session["ShopCart"] as List<ShopCartItem>;
            }

            if (cart.Any(p => p.ProductId == id))
            {
                if (cart.FirstOrDefault(p => p.ProductId == id).Count > 1)
                {
                    int index = cart.FindIndex(p => p.ProductId == id);
                    cart[index].Count -= 1;
                }
                else
                {
                    int index = cart.FindIndex(p => p.ProductId == id);
                    cart.RemoveAt(index);
                }
            }

            Session["ShopCart"] = cart;

            return cart.Sum(p => p.Count);
        }
        public int ShopCartCount()
        {
            int count = 0;

            if (Session["ShopCart"] != null)
            {
                List<ShopCartItem> cart = Session["ShopCart"] as List<ShopCartItem>;
                count = cart.Sum(p => p.Count);
            }

            return count;
        }

        public ActionResult RemoveFromCart(int id, string returnUrl = null)
        {
            List<ShopCartItem> cart = Session["ShopCart"] as List<ShopCartItem>;
            int remove = cart.FindIndex(p => p.ProductId == id);
            cart.RemoveAt(remove);
            Session["ShopCart"] = cart;
            if (returnUrl != null)
                return Redirect(returnUrl);

            return Redirect("/Home/Index");
        }

        public ActionResult RemoveFromShoppingCart(int id)
        {
            List<ShopCartItem> cart = Session["ShopCart"] as List<ShopCartItem>;
            int remove = cart.FindIndex(p => p.ProductId == id);
            cart.RemoveAt(remove);
            Session["ShopCart"] = cart;
            return Redirect("/Shop/ShoppingCart");
        }

        public ActionResult ShowCart()
        {
            List<SmartStore.Models.ShapCartViewModel> list = new List<SmartStore.Models.ShapCartViewModel>();

            if (Session["ShopCart"] != null)
            {
                List<ShopCartItem> listShop = (List<ShopCartItem>)Session["ShopCart"];

                foreach (var item in listShop)
                {
                    var product = db.Products.Where(p => p.Id == item.ProductId).Select(p => new
                    {
                        p.ProductName,
                        p.ProductImg,
                        p.ProductPrice,
                        p.ProductDiscountPercent
                    }).Single();

                    var shopCart = new ShapCartViewModel()
                    {
                        Count = item.Count,
                        ProductId = item.ProductId,
                        ProductImg = product.ProductImg,
                        ProductName = product.ProductName,
                    };

                    var price = product.ProductPrice;
                    if (product.ProductDiscountPercent != null && product.ProductDiscountPercent > 0)
                        price = product.ProductPrice - product.ProductPrice * product.ProductDiscountPercent / 100;

                    shopCart.ProductPrice = price;
                    shopCart.Sum = item.Count * price;
                    list.Add(shopCart);
                }
            }

            return PartialView(list);
        }

        List<SmartStore.Models.ShapCartViewModel> getListOrder()
        {
            List<SmartStore.Models.ShapCartViewModel> list = new List<SmartStore.Models.ShapCartViewModel>();

            if (Session["ShopCart"] != null)
            {
                List<ShopCartItem> listShop = Session["ShopCart"] as List<ShopCartItem>;

                foreach (var item in listShop)
                {
                    var product = db.Products.Where(p => p.Id == item.ProductId).Select(p => new
                    {
                        p.ProductName,
                        p.ProductImg,
                        p.ProductPrice,
                        p.ProductDiscountPercent
                    }).Single();

                    var shopCart = new ShapCartViewModel()
                    {
                        Count = item.Count,
                        ProductId = item.ProductId,
                        ProductImg = product.ProductImg,
                        ProductName = product.ProductName,
                    };

                    var price = product.ProductPrice;
                    if (product.ProductDiscountPercent != null && product.ProductDiscountPercent > 0)
                        price = product.ProductPrice - product.ProductPrice * product.ProductDiscountPercent / 100;

                    shopCart.ProductPrice = price;
                    shopCart.Sum = item.Count * price;
                    list.Add(shopCart);
                }
            }
            return list;
        }

        public ActionResult Order()
        {
            return PartialView(getListOrder());
        }

        public ActionResult CommandOrder(int id, int count)
        {
            List<ShopCartItem> listShop = Session["ShopCart"] as List<ShopCartItem>;
            int index = listShop.FindIndex(p => p.ProductId == id);
            if (count == 0)
            {
                listShop.RemoveAt(index);
            }
            else
            {
                listShop[index].Count = count;
            }
            Session["ShopCart"] = listShop;

            return PartialView("Order", getListOrder());
        }

        [Authorize]
        public ActionResult Payment()
        {
            ViewBag.MyUserId = db.Users.Single(u => u.UserEmail == User.Identity.Name).Id;
            int userId = db.Users.Single(u => u.UserEmail == User.Identity.Name).Id;
            var user = db.Users.FirstOrDefault(u => u.UserEmail == User.Identity.Name);
            //var product = db.Products.FirstOrDefault();
            string fn = "SFN";
            string PN = "SPN";
            Random random = new Random();
            string MyFactorNumber = fn.ToString() + "_" + random.Next(200000000, 990000000);
            string MyPayNumber = "0";
            bool isValidCod = false;
            while (!isValidCod)
            {
                Random random1 = new Random();

                MyPayNumber = PN.ToString() + "_" + random.Next(10000000, 90000000);

                var userpaynumber = db.Users.FirstOrDefault(j => j.UserVerificationCode == MyPayNumber.ToString());
                if (userpaynumber == null)
                {
                    isValidCod = true;
                }
            }
            var listDetails = getListOrder();

            Factor factor = new Factor()
            {
                UserId = userId,
                Price = listDetails.Select(l => l.ProductPrice * l.Count).Sum(),
                CreationDate = DateTime.Now,
                IsPay = true,
                FactorNumber = MyFactorNumber.ToString(),
                PayNumber = MyPayNumber.ToString(),
                PayDate = DateTime.Now.ToString("dd/MM/yyyy"),
                PayTime = DateTime.Now.ToString("hh:mm tt"),
            };
            db.Factors.Add(factor);


            foreach (var item in listDetails)
            {
                db.FactorDetails.Add(new FactorDetail()
                {
                    DetailCount = item.Count,
                    FactorId = factor.Id,
                    UnitPrice = item.ProductPrice,
                    ProductId = item.ProductId,
                });
            }
            db.SaveChanges();

            #region Adding User Points

            foreach (var item in listDetails)
            {
                var product = db.Products.Find(item.ProductId);
                var userPoint = new UserPoint()
                {
                    UserId = userId,
                    Point = product.Point * item.Count,
                    RegisterDate = DateTime.Now
                };
                db.UserPoints.Add(userPoint);

            }
            db.SaveChanges();

            #endregion

            return View(factor);
        }
        public ActionResult ProductList(int? id)
        {
            var vm = new ProductListViewModel();
            vm.ProductCount = db.Products.Count();
            vm.SelectedGroupId = id ?? 0;
            var productGroups = new List<ProductGroupWithCountViewModel>();
            if (id == null)
            {
                vm.Features = db.Features.Include(f => f.SubFeatures).ToList();
                vm.Brands = db.Brands.ToList();
                productGroups.Add(
                    new ProductGroupWithCountViewModel(
                        0,
                        "All",
                        db.Products.Where(p => p.IsDeleted != true && p.ProductNotShow == false).Count())
                    );
                var allParentGroups = db.ProductGroups.Where(p => p.ParentId == null);
                foreach (var item in allParentGroups)
                {
                    productGroups.Add(
                        new ProductGroupWithCountViewModel(
                        item.Id,
                        item.GroupName,
                        GetGroupProductCount(item.Id)
                        ));
                }
            }
            else
            {
                var pgFeatures = db.ProductGroupFeatures.Where(f => f.ProductGroupId == id).ToList();
                var features = pgFeatures.Select(item => db.Features.Include(f => f.SubFeatures).FirstOrDefault(f => f.Id == item.FeatureId)).ToList();
                vm.Features = features;

                var pgBrands = db.ProductGroupBrands.Where(f => f.ProductGroupId == id).ToList();
                var brands = pgBrands.Select(item => db.Brands.Find(item.BrandId)).ToList();
                vm.Brands = brands;
                var selectedProductGroup = db.ProductGroups.Find(id);
                productGroups.Add(
                    new ProductGroupWithCountViewModel(
                        id,
                        "All",
                        GetGroupProductCount(id.Value))
                    );
                var allChildGroups = db.ProductGroups.Where(p => p.ParentId == id).ToList();
                foreach (var item in allChildGroups)
                {
                    productGroups.Add(
                        new ProductGroupWithCountViewModel(
                        item.Id,
                        item.GroupName,
                        GetGroupProductCount(item.Id)
                        ));
                }
                ViewBag.ProductGroupName = selectedProductGroup.GroupName;
            }
            vm.ProductGroups = productGroups;
            return View(vm);
        }
        public JsonResult GetProductGroupFeatures(List<int> productGroups)
        {
            var features = new List<Feature>();
            if (productGroups.Count == 0)
            {
                features = db.Features.ToList();
            }
            else
            {
                foreach (var groupId in productGroups)
                {
                    var pgFeatures = db.ProductGroupFeatures.Where(f => f.ProductGroupId == groupId).ToList();
                    var ftre = pgFeatures.Select(item => db.Features.FirstOrDefault(f => f.Id == item.FeatureId)).ToList();
                    features.AddRange(ftre);
                }
            }

            features = features.DistinctBy(f => f.Id).ToList();
            var obj = features.Select(item => new FeaturesObjViewModel() { Id = item.Id, Title = item.FeatureTitle }).ToList();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductGroupBrands(int id)
        {
            var pgBrands = db.ProductGroupBrands.Where(f => f.ProductGroupId == id).ToList();
            var brands = pgBrands.Select(item => db.Brands.FirstOrDefault(f => f.Id == item.BrandId)).ToList();
            var obj = brands.Select(item => new BrandsObjViewModel() { Id = item.Id, Name = item.BrandName }).ToList();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewProducts(int selectedProductGroup, List<int> selectedBrands, List<int> selectedSubFeatures)
        {
            var products = new List<Product>();
            if (selectedProductGroup == 0)
                products = db.Products.Where(p=>p.IsDeleted != true && p.ProductNotShow == false).Include(p => p.ProductMainFeatures).Include(p => p.ProductFeatureValues).ToList();
            else
            {
                products = db.Products.Where(p => p.ProductGroupId == selectedProductGroup && p.IsDeleted != true && p.ProductNotShow == false).Include(p => p.ProductMainFeatures).Include(p => p.ProductFeatureValues).ToList();
                var childrenGroupIds = GetAllChildrenGroupIds(selectedProductGroup);
                foreach (var item in childrenGroupIds)
                {
                    products.AddRange(db.Products.Where(p => p.ProductGroupId == item).Include(p => p.ProductMainFeatures).Include(p => p.ProductFeatureValues).ToList());
                }
            }
            if (selectedBrands.Any() && selectedBrands.All(id => id != 0))
            {
                var productFilteredByBrand = new List<Product>();
                foreach (var brand in selectedBrands)
                {
                    productFilteredByBrand.AddRange(products.Where(p => p.BrandId == brand));
                }
                products = productFilteredByBrand;
            }
            if (selectedSubFeatures.Any() && selectedSubFeatures.All(id => id != 0))
            {
                var productFilteredBySubFeature = new List<Product>();
                foreach (var subFeature in selectedSubFeatures)
                {
                    productFilteredBySubFeature.AddRange(products.Where(p => p.ProductFeatureValues.Any(pf => pf.SubFeatureId == subFeature) || p.ProductMainFeatures.Any(pf => pf.SubFeatureId == subFeature)));
                }
                products = productFilteredBySubFeature;
            }

            var vm = new List<ProductCard>();
            foreach (var product in products)
            {
                var card = new ProductCard();
                card.Id = product.Id;
                card.ProductName = product.ProductName;
                card.ProductImg = product.ProductImg;
                card.Price = product.ProductPrice;
                card.ProductDiscountPercent = product.ProductDiscountPercent;
                vm.Add(card);
            }
            return PartialView(vm);
        }
        public List<int> GetAllChildrenGroupIds(int id)
        {
            var ids = new List<int>();
            ids.AddRange(db.ProductGroups.Where(p => (p.IsDeleted != true && p.GroupNotShow==false) && p.ParentId == id).Select(p => p.Id).ToList());
            foreach (var item in ids.ToList())
            {
                var childIds = GetAllChildrenGroupIds(item);
                if (childIds.Any())
                {
                    ids.AddRange(childIds);
                }
            }
            return ids;
        }
        public int GetGroupProductCount(int id)
        {
            var count = 0;
            using (var context = new SmartStoreContext())
            {
                count += context.Products.Where(p => p.ProductGroupId == id && (p.IsDeleted!= true && p.ProductNotShow == false)).Count();
                var childrenGroupIds = context.ProductGroups.Where(p => (p.IsDeleted != true && p.GroupNotShow == false) && p.ParentId == id).Select(p => p.Id).ToList();
                foreach (var item in childrenGroupIds.ToList())
                {
                    count += GetGroupProductCount(item);
                }
            }
            return count;
        }
    }
}