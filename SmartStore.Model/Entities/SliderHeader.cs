using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using SmartStore.Models.Basement;

namespace SmartStore.Model.Entities
{
  public class SliderHeader : EntityBase<int>
    {
        [Display(Name = "Slider Title")]
        public string SliderTitle { get; set; }

        [Display(Name = "Slider SubTitle")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string SliderSubTitle { get; set; }

        [Display(Name = "Slider Description")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string SliderDescription { get; set; }

        [Display(Name = "Slider Image")]
        public string SliderImg { get; set; }

        [Display(Name = "Slider Url")]
        public string SliderUrl { get; set; }

        [Display(Name = "Slider Order")]
        public int SliderOrder { get; set; }

        [Display(Name = "Slider NotShow")]
        public bool SliderNotShow { get; set; }


    }
}
