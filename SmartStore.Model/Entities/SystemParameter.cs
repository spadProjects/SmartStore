using SmartStore.Models.Basement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Model.Entities
{
    public class SystemParameter : EntityBase<int>
    {
        public string ParameterName { get; set; }
        public string Value { get; set; }
        public string PersianTitle { get; set; }
        public string Description { get; set; }
        public bool IsSystemic { get; set; }
    }
}
