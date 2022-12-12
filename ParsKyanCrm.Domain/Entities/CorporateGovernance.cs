using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class CorporateGovernance
    {
        public int CorporateGovernanceId { get; set; }
        public int? CustomerId { get; set; }
        public int? RequestId { get; set; }
        public string CompanyWebSite { get; set; }
        public string OrganazationChart { get; set; }
        public string OrganizationalDuties { get; set; }
        public string RiskManagementGuidelines { get; set; }
        public string TransactionRegulations { get; set; }
        public string DeductionTaxAccount { get; set; }
        public string CrmSoftwareContract { get; set; }
        public bool? HaveRepresentative { get; set; }
        public string RepresentativeFile { get; set; }
        public string LetterOfCommendation { get; set; }
        public bool? HaveAuditCommittee { get; set; }
        public string Proceedings { get; set; }
        public string InovationFile { get; set; }
        public string HighProductKnowledge { get; set; }
    }
}
