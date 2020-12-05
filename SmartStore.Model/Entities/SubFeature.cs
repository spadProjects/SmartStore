using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartStore.Models.Basement;

namespace SmartStore.Model.Entities
{
    public class SubFeature : EntityBase<int>
    {
        public string Value { get; set; }
        public string OtherInfo { get; set; }
        public int? FeatureId { get; set; }
        public Feature Feature { get; set; }
        public ICollection<ProductMainFeature> ProductMainFeatures { get; set; }
        public ICollection<ProductFeatureValue> ProductFeatureValues { get; set; }
    }
}
