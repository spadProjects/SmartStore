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
   public class Brand : EntityBase<int>
    {
        public int ProductGroupId { get; set; }

        [Display(Name ="BrandName")]
        [MaxLength(30,ErrorMessage = "Must not exceed {1} characters")]
        public string BrandName { get; set; }

        [Display(Name ="BrandImg")]
        [MaxLength(100,ErrorMessage ="Must not exceed {1} characters")]
        public string BrandImg { get; set; }

        [Display(Name ="BrandOrder")]
        public int BrandOrder { get; set; }

        [Display(Name ="BrandNotShow")]

        //[ForeignKey("ProductGroupId")]
        //public virtual ProductGroup ProductGroup { get; set; }

        public bool BrandNotShow { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
