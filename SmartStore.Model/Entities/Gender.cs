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
   public class Gender : EntityBase<int>
    {
        [Display(Name = "Gender Type")]
        public string GenderType { get; set; }

        [Display(Name = "Is Selected")]
        public bool IsSelected { get; set; }
    }
}
