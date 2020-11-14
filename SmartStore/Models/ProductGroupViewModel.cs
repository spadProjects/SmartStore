using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Models
{
    public class ProductGroupViewModel
    {
        public int GroupId { get; set; }
        public string Title { get; set; }

        private List<ProductGroupViewModel> _subGroups;

        public List<ProductGroupViewModel> SubGroups
        {
            get { return _subGroups??(_subGroups = new List<ProductGroupViewModel>()); }
            set { _subGroups = value; }
        }

    }
}
