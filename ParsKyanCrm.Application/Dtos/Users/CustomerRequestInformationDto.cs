
using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;

namespace ParsKyanCrm.Application.Dtos.Users
{

    public class RequestCustomerRequestInformationDto : PageingParamerDto
    {
        public int? Id { get; set; }
        public int? CustomerId { get; set; }
        public int? RequestId { get; set; }
        public int? QuestionLevelId { get; set; }
    }

    public class CustomerRequestInformationsDto : BaseEntityDto
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? RequestId { get; set; }
        public int? CountOfPersonel { get; set; }
        public decimal? AmountOfLastSale { get; set; }
        public byte? IsActive { get; set; }
        public string LastAuditingTaxList { get; set; }
        public string LastAuditingTaxListFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(LastAuditingTaxList, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_LastAuditingTaxList { get; set; }
        public string LastInsuranceList { get; set; }
        public string LastInsuranceListFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(LastInsuranceList, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_LastInsuranceList { get; set; }
        public int? QuestionLevelId { get; set; }
    }    
}
