using Microsoft.AspNetCore.Http;
using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
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
        public string PriceContractStr { get; set; }
        public decimal? Tax { get; set; }
        public string EvaluationFile { get; set; }

        public string FinancialDocumentFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(FinancialDocument, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_FinancialDocument { get; set; }

        public string ContractDocumentFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(ContractDocument, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_ContractDocument { get; set; }

        public string EvaluationFileFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(EvaluationFile, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_EvaluationFile { get; set; }

    }
}
