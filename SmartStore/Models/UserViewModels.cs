using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartStore.Model.Entities;

namespace SmartStore.Models
{
    public class IntroducersChartViewModel
    {
        public IntroducersCardViewModel Parent { get; set; }
        public IntroducersCardViewModel User { get; set; }
        public List<IntroducersCardViewModel> Children { get; set; }
    }
    public class IntroducersCardViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserImage { get; set; }
        public string Role { get; set; }
        public float Points { get; set; }
    }
    public class UserTableViewModel
    {
        public User User { get; set; }
        public bool HasTwoSubsets { get; set; }
    }
}