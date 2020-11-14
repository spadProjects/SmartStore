using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartStore.Models.Basement;

namespace SmartStore.Model.Entities
{
   public class ProductGroup : EntityBase<int>
    {
        [Display(Name = "GroupImage")]
        public string GroupImage { get; set; }

        [Display(Name = "GroupName")]
        public string GroupName { get; set; }

        [Display(Name = "GroupOrder")]
        public int GroupOrder { get; set; }

        [Display(Name = "GroupNotShow")]
        public bool GroupNotShow { get; set; }

        [Display(Name = "ParentId")]
        public int? ParentId { get; set; }

        public ProductGroup()
        {
            this.ProductGroup1 = new HashSet<ProductGroup>();
        }

        public virtual ICollection<ProductGroup> ProductGroup1 { get; set; }
        public virtual ProductGroup ProductGroup2 { get; set; }

        //public virtual ICollection<Brand> Brands { get; set; }

    }
}
