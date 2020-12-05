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
    public class ProductFeature : EntityBase<int>
    {
        //public int ProductId { get; set; }
        public int FeatureId { get; set; }

        [Display(Name = "Feature Value")]
        //[MaxLength(20, ErrorMessage = "Must not exceed {1} characters")]
        public string FeatureValue { get; set; }

        [Display(Name = "ProductFeatureOrder")]
        public int ProductFeatureOrder { get; set; }

        [Display(Name = "ProductFeatueNotShow")]
        public bool ProductFeatueNotShow { get; set; }

        //[ForeignKey("ProductId")]
        //public virtual Product Product { get; set; }

        //[ForeignKey("FeatureId")]
        //public virtual Feature Feature { get; set; }


    }
}