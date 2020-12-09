using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Model.Entities
{
    public class UserPoint
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public float Point { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
