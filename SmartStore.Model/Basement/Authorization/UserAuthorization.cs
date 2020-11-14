using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Model.Entities.Base.Authorization
{
    public class UserAuthorization
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public int AuthorizationId { get; set; }
    }
}
