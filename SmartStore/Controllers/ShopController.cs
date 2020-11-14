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
using BitPayLight;
using BitPayLight.Models.Invoice;

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

            var groupList = new List<int>();

            groupList.Add(groupid ?? 0);

            var subGroups = db.ProductGroups.Where(w => w.ParentId == groupid).Select(s => s.Id).ToList();

            subGroups.ForEach(item =>
            {
                groupList.Add(item);

                var subSubGroup = db.ProductGroups.Where(w => w.ParentId == item).Select(s => s.Id).ToList();

                groupList.AddRange(subSubGroup);
            });

            List<SmartStore.Model.Entities.Product> prdlist = new List<Product>();

            prdlist = db.Products.Where(m => groupList.Contains( m.ProductGroupId)).ToList();
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

        public ActionResult RemoveFromCart(int id)
        {
            List<ShopCartItem> cart = Session["ShopCart"] as List<ShopCartItem>;
            int remove = cart.FindIndex(p => p.ProductId == id);
            cart.RemoveAt(remove);
            Session["ShopCart"] = cart;
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
                        p.ProductImg,
                        p.ProductName,
                        p.ProductPrice,
                    }).Single();
                    list.Add(new SmartStore.Models.ShapCartViewModel()
                    {
                        Count = item.Count,
                        ProductId = item.ProductId,
                        ProductName = product.ProductName,
                        ProductImg = product.ProductImg,
                        ProductPrice = product.ProductPrice,
                        Sum = item.Count * product.ProductPrice,
                    });
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
                        p.ProductPrice
                    }).Single();
                    list.Add(new SmartStore.Models.ShapCartViewModel()
                    {
                        Count = item.Count,
                        ProductId = item.ProductId,
                        ProductPrice = product.ProductPrice,
                        ProductImg = product.ProductImg,
                        ProductName = product.ProductName,
                        Sum = item.Count * product.ProductPrice
                    });
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

        public ActionResult SubmitFactor()
        {
            ViewBag.MyUserId = db.Users.Single(u => u.UserEmail == User.Identity.Name).Id;
            int userId = db.Users.Single(u => u.UserEmail == User.Identity.Name).Id;
            var user = db.Users.FirstOrDefault(u => u.UserEmail == User.Identity.Name);
            var product = db.Products.FirstOrDefault();
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

            Factor factor = new Factor()
            {
                UserId = userId,
                Price = 2,
                CreationDate = DateTime.Now,
                IsPay = false,
                FactorNumber = MyFactorNumber.ToString(),
                PayNumber = MyPayNumber.ToString(),
                InsertUser = "admin",
                InsertDate = DateTime.Now,
                PayDate = DateTime.Now.ToString("dd/MM/yyyy"),
                PayTime = DateTime.Now.ToString("hh:mm tt"),
            };
            db.Factors.Add(factor);

            var listDetails = getListOrder();

            foreach (var item in listDetails)
            {
                db.FactorDetails.Add(new FactorDetail()
                {
                    DetailCount = item.Count,
                    FactorId = factor.Id,
                    UnitPrice = item.ProductPrice,
                    ProductId = item.ProductId,
                    InsertDate = DateTime.Now,
                    InsertUser = "admin"
                });
            }
            db.SaveChanges();
            return RedirectToAction("Payment", new { factorId = factor.Id });
        }
        [Authorize]
        public ActionResult Payment(int factorId)
        {
            var factor = db.Factors.Find(factorId);
            return View(factor);
        }

        [HttpPost]
        public ActionResult Checkout(int id)
        {

            var factor = db.Factors.Where(w => w.Id == id).FirstOrDefault();

            var price = (double)factor.Price;

            //var bitpay = new IranianBazar.Controllers.HomeController();

            var bitpay = new BitPay("CPQMfPuwEHV8Ss1GefgErbqCsDSFw9uRTpiLNFfuTbYu");
            var invoice = new Invoice();

            invoice = new Invoice(price, "GBP");
            invoice.Buyer = new Buyer();
            invoice.Buyer.Name = factor.User?.UserLastName;
            invoice.Buyer.Email = "Customer@outlook.com";
            invoice.FullNotifications = true;
            invoice.NotificationEmail = "Customer@yahoo.com";
            string data = factor.FactorNumber;
            invoice.PosData = data;

            invoice = bitpay.CreateInvoice(invoice).Result;
            var url = invoice.Url;
            ViewBag.data = data;

            return Redirect(url);

            //return bitpay.Index(price);
        }

    }
}