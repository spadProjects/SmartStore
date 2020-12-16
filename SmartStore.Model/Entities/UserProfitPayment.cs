using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Model.Entities
{
    public class UserProfitPayment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
