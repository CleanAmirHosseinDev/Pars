using ParsKyanCrm.Application.Dtos.BasicInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{
    public class CustomersDto : BaseEntityDto
    {

        public int CustomerId { get; set; }
        public int? CityId { get; set; }
        public string CompanyName { get; set; }
        public string NamesAuthorizedSignatories { get; set; }
        public int? KindOfCompanyId { get; set; }
        public int? TypeServiceRequestedId { get; set; }
        public int? HowGetKnowCompanyId { get; set; }
        public string AddressCompany { get; set; }
        public string NationalCode { get; set; }
        public string PostalCode { get; set; }
        public string EconomicCode { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string AgentName { get; set; }
        public string AgentMobile { get; set; }
        public string CeoName { get; set; }
        public string CeoMobile { get; set; }
        public int? CountOfPersonal { get; set; }
        public decimal? AmountOsLastSaels { get; set; }
        public DateTime SaveDate { get; set; }        
        public string Ip { get; set; }
        public string AuthenticateCode { get; set; }

        public CityDto City { get; set; }
        public SystemSetingDto HowGetKnowCompany { get; set; }
        public SystemSetingDto KindOfCompany { get; set; }
        public SystemSetingDto TypeServiceRequested { get; set; }

    }
}
