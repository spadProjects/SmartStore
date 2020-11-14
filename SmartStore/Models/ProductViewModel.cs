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
}
