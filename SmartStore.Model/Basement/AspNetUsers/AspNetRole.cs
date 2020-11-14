using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Model.Entities
{
    public class AspNetRole
    {
        [Key]
        [StringLength(128)]
        public string Id { get; set; }

        [StringLength(256)]
        public string Name { get; set; }
    }
}
