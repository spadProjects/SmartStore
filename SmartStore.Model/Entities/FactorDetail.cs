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
   public class FactorDetail : EntityBase<int>
    {        
        public int FactorId { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "Detail Count")]
        public int DetailCount { get; set; }

        [Display(Name = "UnitPrice")]
        public decimal UnitPrice { get; set; }

        [ForeignKey("FactorId")]
        public virtual Factor Factor { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }


    }
}
