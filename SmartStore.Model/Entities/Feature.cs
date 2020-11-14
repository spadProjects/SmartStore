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
    public class Feature : EntityBase<int>
    {
        public int ProductGroupId { get; set; }

        [Display(Name = "Feature Title")]
        [MaxLength(20, ErrorMessage = "Must not exceed {1} characters")]
        public string FeatureTitle { get; set; }

        [Display(Name = "Feature Order")]
        public int FeatureOrder { get; set; }

        [Display(Name = "Featue NotShow")]
        public bool FeatueNotShow { get; set; }

        [ForeignKey("ProductGroupId")]
        public virtual ProductGroup ProductGroup { get; set; }

        public virtual ICollection<ProductFeature> ProductFeatures { get; set; }
    }
}
