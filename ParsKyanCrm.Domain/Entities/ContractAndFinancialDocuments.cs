using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class ContractAndFinancialDocuments
    {
        public int FinancialId { get; set; }
        public string FinancialDocument { get; set; }
        public string ContractDocument { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? SaveDate { get; set; }

        public virtual Customers Customer { get; set; }
    }
}
