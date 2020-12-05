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
        [Display(Name = "Feature Title")]
        [MaxLength(20, ErrorMessage = "Must not exceed {1} characters")]
        public string FeatureTitle { get; set; }

        public ICollection<ProductGroupFeature> ProductGroupFeatures { get; set; }
        public ICollection<SubFeature> SubFeatures { get; set; }
        public ICollection<ProductFeatureValue> ProductFeatureValues { get; set; }
    }
}
