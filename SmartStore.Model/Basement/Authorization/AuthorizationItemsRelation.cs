using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Model.Entities.Base.Authorization
{
   public class AuthorizationItemsRelation
    {

        public int Id { get; set; }
        public int FirstItemId { get; set; }
        public int SecondItemId { get; set; }
    }
}
