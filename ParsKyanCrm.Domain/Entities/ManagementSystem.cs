using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class ManagementSystem
    {
        public int ManagementSystemId { get; set; }
        public int? CustomerId { get; set; }
        public int? RequestId { get; set; }
        public string GuidelinesAndRegulations { get; set; }
        public string GuidelinesAndRegulationsFile { get; set; }
        public bool? IsGuideLineOrProcess { get; set; }
        public string CertificateAndPermitAndStandard { get; set; }
        public string CertificateAndPermitAndStandardFile { get; set; }
        public string ExporterReferenceForCertificate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string Awards { get; set; }
        public string AwardsFile { get; set; }
        public string ExporterReferenceForAwards { get; set; }
        public DateTime? DateOfExporter { get; set; }
        public bool? Status { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual RequestForRating Request { get; set; }
    }
}
