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
        public int? RequestID { get; set; }        
        public DateTime? SaveDate { get; set; }
        public string ContentContract { get; set; }
        public decimal? PriceContract { get; set; }
        public decimal? Tax { get; set; }        
        public string EvaluationFile { get; set; }
        public decimal? DisCountMoney { get; set; }
        public float? DicCountPerecent { get; set; }
    }
}
