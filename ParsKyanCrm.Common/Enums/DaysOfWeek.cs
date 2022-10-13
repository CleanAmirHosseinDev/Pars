using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Common.Enums
{
    public enum DaysOfWeek : byte
    {
        [Display(Name = "شنبه")]
        Saturday,

        [Display(Name = "یک شنبه")]
        Sunday,

        [Display(Name = "دو شنبه")]
        Monday,

        [Display(Name = "سه شنبه")]
        Tuesday,

        [Display(Name = "چهار شنبه")]
        Wednesday,

        [Display(Name = "پنج شنبه")]
        Thursday,

        [Display(Name = "جمعه")]
        Friday,

    }
}
