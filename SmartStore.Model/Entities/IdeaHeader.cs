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
   public class IdeaHeader : EntityBase<int>
    {
        [Display(Name = "Header")]
        [DataType(DataType.MultilineText)]
        public string Header { get; set; }

        [Display(Name = "NotShow")]
        public bool NotShow { get; set; }
    }
}
