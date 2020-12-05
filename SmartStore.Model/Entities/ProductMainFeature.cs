using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartStore.Models.Basement;

namespace SmartStore.Model.Entities
{
   public class ProductMainFeature : EntityBase<int>
    {
        public string Value { get; set; }
        public string OtherInfo { get; set; }
        public int? FeatureId { get; set; }
        public Feature Feature { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public int? SubFeatureId { get; set; }
        public SubFeature SubFeature { get; set; }
        public int Quantity { get; set; }
        public long Price { get; set; }
    }
}
