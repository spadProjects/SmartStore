using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartStore.Models.Basement;

namespace SmartStore.Model.Entities
{
    public class ProductGroupBrand : EntityBase<int>
    {
        public int ProductGroupId { get; set; }
        public ProductGroup ProductGroup { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
