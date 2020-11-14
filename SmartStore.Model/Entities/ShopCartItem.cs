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
   public class ShopCartItem : EntityBase<int>
    {
        public int ProductId { get; set; }
        public int Count { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }


    }
}
