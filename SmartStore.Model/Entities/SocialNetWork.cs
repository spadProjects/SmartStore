using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using SmartStore.Models.Basement;

namespace SmartStore.Model.Entities
{
    public class SocialNetWork : EntityBase<int>
    {

        [Display(Name = "Name")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        public string Name { get; set; }

        [Display(Name = "Icon")]
        [MaxLength(100, ErrorMessage = "Must not exceed {1} characters")]
        public string Icon { get; set; }

        [Display(Name = "Link")]
        [MaxLength(100, ErrorMessage = "Must not exceed {1} characters")]
        public string Link { get; set; }

        [Display(Name = "Order")]
        public int Order { get; set; }

        [Display(Name = "NotShow")]
        public bool NotShow { get; set; }
    }
}