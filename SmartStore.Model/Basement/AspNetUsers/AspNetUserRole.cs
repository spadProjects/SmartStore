using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Model.Entities
{
    public class AspNetUserRole
    {
        [Key]
        [StringLength(128)]
        public string UserId { get; set; }
        
        [StringLength(256)]
        public string RoleId { get; set; }
    }
}
