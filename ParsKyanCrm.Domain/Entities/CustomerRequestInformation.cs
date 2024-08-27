using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class CustomerRequestInformation
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? RequestId { get; set; }
        public int? CountOfPersonal { get; set; }
        public decimal? AmountOsLastSales { get; set; }
        public byte? IsActive { get; set; }
        public string Ip { get; set; }
        public string LastAuditingTaxList { get; set; }
        public string LastInsuranceList { get; set; }
    }
}
