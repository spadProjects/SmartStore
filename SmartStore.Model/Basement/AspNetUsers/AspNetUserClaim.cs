using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Model.Entities
{
    public class AspNetUserClaim 
    {
        [Key]
        [StringLength(128)]
        public string UserId { get; set; }
        [StringLength(128)]
        public string Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
