using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Models.Basement
{
    public class EntityBase<TPrimaryKey>
    {
        [Key]
        public TPrimaryKey Id { get; set; }
        public int? ParentHistoryId { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsArchived { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
