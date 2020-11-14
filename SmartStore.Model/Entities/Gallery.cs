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
    public class Gallery : EntityBase<int>
    {
        public int ProductId { get; set; }

        [Display(Name = "ImageGallery")]
        public string Image { get; set; }

        [Display(Name = "Order")]
        public int Order { get; set; }

        [Display(Name = "NotShow")]
        public bool NotShow { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

    }
}
