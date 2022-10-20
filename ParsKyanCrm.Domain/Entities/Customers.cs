using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class Customers
    {
        public Customers()
        {
            ContractAndFinancialDocuments = new HashSet<ContractAndFinancialDocuments>();
            EducationCourses = new HashSet<EducationCourses>();
            OtherDocuments = new HashSet<OtherDocuments>();
            RequestForReating = new HashSet<RequestForReating>();
            SkillsOfEmployees = new HashSet<SkillsOfEmployees>();
            WorkExperience = new HashSet<WorkExperience>();
        }

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
        public byte IsActive { get; set; }
        public string Ip { get; set; }
        public string AuthenticateCode { get; set; }

        public virtual City City { get; set; }
        public virtual SystemSeting HowGetKnowCompany { get; set; }
        public virtual SystemSeting KindOfCompany { get; set; }
        public virtual SystemSeting TypeServiceRequested { get; set; }
        public virtual ICollection<ContractAndFinancialDocuments> ContractAndFinancialDocuments { get; set; }
        public virtual ICollection<EducationCourses> EducationCourses { get; set; }
        public virtual ICollection<OtherDocuments> OtherDocuments { get; set; }
        public virtual ICollection<RequestForReating> RequestForReating { get; set; }
        public virtual ICollection<SkillsOfEmployees> SkillsOfEmployees { get; set; }
        public virtual ICollection<WorkExperience> WorkExperience { get; set; }
    }
}
