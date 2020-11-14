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
   public class Role : EntityBase<int>
    {
        [Display(Name = "Role Name")]
        [MaxLength(20, ErrorMessage = "Must not exceed {1} characters")]
        public string RoleName { get; set; }

        [Display(Name = "Role Title")]
        [MaxLength(20, ErrorMessage = "Must not exceed {1} characters")]
        public string RoleTitle { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
