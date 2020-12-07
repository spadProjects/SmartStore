using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using SmartStore.Models.Basement;
using System.Web;

namespace SmartStore.Model.Entities
{
    public class Product : EntityBase<int>
    {
        public int ProductGroupId { get; set; }

        public int BrandId { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Product Img")]
        public string ProductImg { get; set; }

        [Display(Name = "Product Price")]
        public decimal ProductPrice { get; set; }
        
        [Display(Name = "Discount Percent")]
        public int ProductDiscountPercent { get; set; }

        [Display(Name = "Product Description")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string ProductDescription { get; set; }

        [Display(Name = "Product ShortDescription")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string ProductShortDescription { get; set; }

        [Display(Name = "ProductOrder")]
        public int ProductOrder { get; set; }

        [Display(Name = "ProductNotShow")]
        public bool ProductNotShow { get; set; }

        [Display(Name = "Product Cost")]
        //[MaxLength(100, ErrorMessage = "Must not exceed {1} characters")]
        public int ProductCost { get; set; }

        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }

        [ForeignKey("ProductGroupId")]
        public virtual ProductGroup ProductGroup { get; set; }

        [NotMapped]
        public HttpPostedFileBase File { get; set; }

        public virtual ICollection<Gallery> Galleries { get; set; }
        public virtual ICollection<ShopCartItem> ShopCartItem { get; set; }
        public virtual ICollection<ProductFeatureValue> ProductFeatureValues { get; set; }
        public virtual ICollection<ProductMainFeature> ProductMainFeatures { get; set; }

    }
    
}
