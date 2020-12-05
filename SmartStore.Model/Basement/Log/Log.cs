using SmartStore.Models.Basement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Model.Entities.Base.Log
{
    public class Log : EntityBase<int>
    {
        [StringLength(300)]
        public string Message { get; set; }

        public int LogType { get; set; }
        [StringLength(300)]
        public string Event { get; set; }
        [StringLength(50)]
        public string IpAddress { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
    }
}
