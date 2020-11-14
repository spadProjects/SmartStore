using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Model.Entities.Base.Authorization
{
    public class AuthorizationItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FarsiTitle { get; set; }
        public int ItemType { get; set; }
    }
}
