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
        public double? DicCountPerecent { get; set; }

        public string ContractCode { get; set; }

        public string ContractMainCode { get; set; }
        public decimal? FinalPriceContract { get; set; }

        public string ContractDocumentCustomer { get; set; }
        public string CommitteeEvaluationFile { get; set; }
        public string LastFinancialDocument { get; set; }
        public string LeaderEvaluationFile { get; set; }
        public bool ConfirmCommitteeEvaluation { get; set; }
        public byte IsActive { get; set; }
        public bool CanSeePreFactor { get; set; }


    }
}
