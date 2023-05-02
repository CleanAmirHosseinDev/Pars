using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class Customers
    {
        public Customers()
        {
          
            RequestForRating = new HashSet<RequestForRating>();
            Users = new HashSet<Users>();
        }

        public string EmailRepresentative { get; set; }

        public string NationalCodeRepresentative { get; set; }

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
        public string CeoNationalCode { get; set; }
        public int? CountOfPersonal { get; set; }
        public decimal? AmountOsLastSales { get; set; }
        public DateTime SaveDate { get; set; }
        public byte IsActive { get; set; }
        public string Ip { get; set; }
        public string AuthenticateCode { get; set; }
        public bool IsProfileComplete { get; set; }
        public string LastAuditingTaxList { get; set; }
        public string LastInsuranceList { get; set; }
        public string OfficialNewspaper { get; set; }
        public string LastChangeOfficialNewspaper { get; set; }
        public string Statute { get; set; }
        public string AuditedFinancialStatements { get; set; }
       
        public int? CustomerPersonalityType { get; set; }
        public int? TypeGroupCompanies { get; set; }

        public string EconomicCodeReal { get; set; }


        public virtual City City { get; set; }
        public virtual SystemSeting HowGetKnowCompany { get; set; }
        public virtual SystemSeting KindOfCompany { get; set; }
        public virtual SystemSeting TypeServiceRequested { get; set; }

      
        public virtual ICollection<RequestForRating> RequestForRating { get; set; }
        public virtual ICollection<Users> Users { get; set; }

    }
}
