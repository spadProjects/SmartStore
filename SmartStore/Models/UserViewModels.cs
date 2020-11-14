using SmartStore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.Models
{
    public class IntroducersChartViewModel
    {
        public User Parent { get; set; }
        public User User { get; set; }
        public List<User> Children { get; set; }
    }
}