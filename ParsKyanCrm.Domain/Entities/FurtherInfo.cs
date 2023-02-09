using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class FurtherInfo
    {
        public int FurtherInfoId { get; set; }
        public int? RequestId { get; set; }
        public string LastAuditingTaxList { get; set; }
        public string LastChangeOfficialNewspaper { get; set; }
        public string StatuteDoc { get; set; }

        public string OfficialNewspaper { get; set; }

    }
}
