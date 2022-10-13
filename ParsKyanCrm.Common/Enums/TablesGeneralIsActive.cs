using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Common.Enums
{
    public enum TablesGeneralIsActive : byte
    {
        [Display(Name = "غیر فعال")]
        InActive = 14,

        [Display(Name = "فعال")]
        Active = 15,
    }
}
