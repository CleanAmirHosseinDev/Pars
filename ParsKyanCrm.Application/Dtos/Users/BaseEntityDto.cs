using ParsKyanCrm.Common.Enums;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{
    public class BaseEntityDto
    {
        public byte IsActive { get; set; }
        public string IsActiveStr
        {
            get
            {
                TablesGeneralIsActive itemDisplay = (TablesGeneralIsActive)IsActive;
                return EnumOperation<TablesGeneralIsActive>.GetDisplayValue(itemDisplay);
            }
        }
    }
}
