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
   public class Factor : EntityBase<int>
    {
        public int UserId { get; set; }

        [Display(Name = "Factor Number")]
        public string FactorNumber { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Pay Date")]
        public string PayDate { get; set; }

        [Display(Name = "Pay Time")]
        public string PayTime { get; set; }

        [Display(Name = "Pay Number")]
        public string PayNumber { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Is Pay")]
        public bool IsPay { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
