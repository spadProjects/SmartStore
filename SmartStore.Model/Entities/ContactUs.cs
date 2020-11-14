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
    public class ContactUs : EntityBase<int>
    {

        [Display(Name = "Image")]
        public string Image { get; set; }

        [Display(Name = "Address")]
        [MaxLength(100, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "Tel")]
        [MaxLength(11, ErrorMessage = "Must not exceed {1} characters")]
        public string Tel { get; set; }


        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "AboutUs")]
        [MaxLength(200, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.MultilineText)]
        public string AboutUs { get; set; }

        [Display(Name = "ShortAboutUs")]
        [MaxLength(50, ErrorMessage = "Must not exceed {1} characters")]
        [DataType(DataType.MultilineText)]
        public string ShortAboutUs { get; set; }

        [Display(Name = "NotShow")]
        public bool NotShow { get; set; }
    }
}