using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class CorporateGovernanceDetail
    {
        public int CorporateGovernanceDetailId { get; set; }
        public int? CorporateGovernanceId { get; set; }
        public string NameOfAuditCommittee { get; set; }
        public int? DegreeOfEducation { get; set; }
        public int? WorkExperience { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
    }
}
