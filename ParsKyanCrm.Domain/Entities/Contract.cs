using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class Contract
    {
        public int ContractId { get; set; }
        public string ContractText { get; set; }
        public int? KinfOfRequest { get; set; }

        public virtual SystemSeting KinfOfRequestNavigation { get; set; }
    }
}
