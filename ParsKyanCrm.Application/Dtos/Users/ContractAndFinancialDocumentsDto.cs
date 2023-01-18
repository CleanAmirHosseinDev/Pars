using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{

    public class RequestContractAndFinancialDocumentsDto : PageingParamerDto
    {
        public int? RequestID { get; set; }
    }
    public class ContractAndFinancialDocumentsDto
    {

        public int FinancialId { get; set; }
        public string FinancialDocument { get; set; }
        public string ContractDocument { get; set; }
        public int? RequestID { get; set; }
        public DateTime? SaveDate { get; set; }
        public string ContentContract { get; set; }
        public decimal? PriceContract { get; set; }
        public decimal? Tax { get; set; }

    }
}
