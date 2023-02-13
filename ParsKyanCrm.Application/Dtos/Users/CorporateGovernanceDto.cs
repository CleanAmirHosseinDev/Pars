using Microsoft.AspNetCore.Http;
using ParsKyanCrm.Application.Dtos.BasicInfo;
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
   
    public class RequestCorporateGovernanceDto : PageingParamerDto
    {
        public int? RequestId { get; set; }
    }

    public class CorporateGovernanceDto : BaseEntityDto
    {
        public int CorporateGovernanceId { get; set; }
        public int? CustomerId { get; set; }      
        public int? RequestId { get; set; }

        #region حاکمیت شرکتی
        public string CompanyWebSite { get; set; }
        public string OrganazationChart { get; set; }
        public string OrganazationChartFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(OrganazationChart, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_OrganazationChart { get; set; }

        public string OrganizationalDuties { get; set; }
        public string OrganizationalDutiesFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(OrganizationalDuties, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_OrganizationalDuties { get; set; }

        public string RiskManagementGuidelines { get; set; }
        public string RiskManagementGuidelinesFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(RiskManagementGuidelines, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_RiskManagementGuidelines { get; set; }


        public string TransactionRegulations { get; set; }
        public string TransactionRegulationsFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(TransactionRegulations, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_TransactionRegulations { get; set; }




        public string DeductionTaxAccount { get; set; }
        public string DeductionTaxAccountFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(DeductionTaxAccount, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_DeductionTaxAccount { get; set; }



        public string CrmSoftwareContract { get; set; }
        public string CrmSoftwareContractFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(CrmSoftwareContract, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_CrmSoftwareContract { get; set; }

        public bool HaveRepresentative { get; set; }
        public bool HaveAuditCommittee { get; set; }

        public string RepresentativeFile { get; set; }
        public string RepresentativeFileFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(RepresentativeFile, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_RepresentativeFile { get; set; }



        public string LetterOfCommendation { get; set; }
        public string LetterOfCommendationFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(LetterOfCommendation, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_LetterOfCommendation { get; set; }


        public string InovationFile { get; set; }
        public string InovationFileFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(InovationFile, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_InovationFile { get; set; }



        public string HighProductKnowledge { get; set; }

       
        public string Proceedings { get; set; }
        public string ProceedingsFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(Proceedings, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_Proceedings { get; set; }

        #endregion
    }

}
