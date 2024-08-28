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
        public int? CountOfPersonel { get; set; }
        public decimal? AmountOfLastSale { get; set; }
        public byte? IsActive { get; set; }
        public string LastAuditingTaxList { get; set; }
        public string LastInsuranceList { get; set; }
    }
}
