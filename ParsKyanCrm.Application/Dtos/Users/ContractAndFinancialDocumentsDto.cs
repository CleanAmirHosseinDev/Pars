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

        public List<Common.Dto.NormalJsonClassDto> Values { get; set; }

        public int FinancialId { get; set; }
        public string FinancialDocument { get; set; }
        public string ContractDocument { get; set; }
        public int? RequestID { get; set; }

        public DateTime? SaveDate { get; set; }
        public string SaveDateStr
        {
            get
            {
                return SaveDate.HasValue ? SaveDate.Value.ToPersianDate() : string.Empty;
            }
        }

        public string ContentContract { get; set; }
        public decimal? PriceContract { get; set; }
        public decimal? FinalPriceContract { get; set; }
        public string PriceContractStr { get; set; }
        public string FinalPriceContractStr { get; set; }
        public decimal? Tax { get; set; }
        public string EvaluationFile { get; set; }
        public decimal? DisCountMoney { get; set; }
        public decimal? DicCountPerecent { get; set; }

        public string ContractDocumentCustomer { get; set; }
        public string ContractDocumentCustomerFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(ContractDocumentCustomer, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_ContractDocumentCustomer { get; set; }
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

        public string LastFinancialDocument { get; set; }
        public string LastFinancialDocumentFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(LastFinancialDocument, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_LastFinancialDocument { get; set; }

        public string CommitteeEvaluationFile { get; set; }
        public string CommitteeEvaluationFileFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(CommitteeEvaluationFile, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_CommitteeEvaluationFile { get; set; }
        public string ContractCode { get; set; }
        public string ContractMainCode { get; set; }
        public int StepCode { get; set; }
        public bool IsCustomer { get; set; }
        public string LeaderEvaluationFile { get; set; }
        public string LeaderEvaluationFileFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(LeaderEvaluationFile, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_LeaderEvaluationFile { get; set; }
        public string ConfirmCommitteeEvaluation { get; set; }
        public byte IsActive { get; set; }
        public bool CanSeePreFactor { get; set; }
        public string CanSeePreFactorStr { get; set; }
        public int EditStatuse { get; set; }

    }
}
