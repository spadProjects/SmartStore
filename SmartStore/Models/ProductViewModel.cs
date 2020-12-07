using SmartStore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public int ProductGroupId { get; set; }
        public int BrandId { get; set; }
        public string ProductName { get; set; }
        public string ProductImg { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductDiscountPercent { get; set; }
        public string ProductDescription { get; set; }
        public string ProductShortDescription { get; set; }
        public int ProductOrder { get; set; }
        public bool ProductNotShow { get; set; }
        public int ProductCost { get; set; }


        private List<ProductGroup> _productGroupList;

        public List<ProductGroup> ProductGroupList
        {
            get { return _productGroupList ?? new List<ProductGroup>(); }
            set { _productGroupList = value; }
        }



    }
    public class NewProductViewModel
    {
        public int? Id { get; set; }
        public string ProductName { get; set; }
        public string ProductShortDescription { get; set; }
        public string ProductDescription { get; set; }
        public int Brand { get; set; }
        public int ProductGroup { get; set; }
        public List<ProductFeaturesViewModel> ProductFeatures { get; set; }

    }

    public class ProductFeaturesViewModel
    {
        public int? ProductId { get; set; }
        public int FeatureId { get; set; }
        public int? SubFeatureId { get; set; }
        public string Value { get; set; }
        public bool IsMain { get; set; }
        public int? Quantity { get; set; }
        public long? Price { get; set; }
    }

    public class ProductListViewModel
    {
        public int? SelectedGroupId { get; set; }
        public int ProductCount { get; set; }
        public List<ProductGroup> ProductGroups { get; set; }
        public List<Feature> Features { get; set; }
        public List<Brand> Brands { get; set; }

    }

    public class ProductCard
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public long Price { get; set; }
        public string ProductImg { get; set; }
    }
}
