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
    public class Blog : EntityBase<int>
    {
        [Display(Name = "BlogImage")]
        public string BlogImage { get; set; }

        [Display(Name = "BlogTitle")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        public string BlogTitle { get; set; }

        [Display(Name = "BlogSubTitle")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        public string BlogSubTitle { get; set; }

        [Display(Name = "CreateDate")]
        public string CreateDate { get; set; }

        [Display(Name = "CreatorName")]
        public string CreatorName { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Order")]
        public int Order { get; set; }

        [Display(Name = "NotShow")]
        public bool NotShow { get; set; }


    }
}
