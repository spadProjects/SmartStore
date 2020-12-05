using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.Models
{
    public class NewProductGroupViewModel
    {
        public string GroupName { get; set; }
        public List<int> BrandIds { get; set; }
        public int ParentGroupId { get; set; }
        public List<int> ProductGroupFeatureIds { get; set; }
    }
    public class UpdateProductGroupViewModel
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<int> BrandIds { get; set; }
        public int ParentGroupId { get; set; }
        public List<int> ProductGroupFeatureIds { get; set; }
    }

    public class FeaturesObjViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class BrandsObjViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class SubFeaturesObjViewModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}