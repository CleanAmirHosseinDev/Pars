using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class Customers_RegisterLanding
    {                       

        public int CustomerId { get; set; }        
        public string CompanyName { get; set; }        
        public int? KindOfCompanyId { get; set; }
        public int? TypeServiceRequestedId { get; set; }        
        public string AddressCompany { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string AgentName { get; set; }
        public string AgentMobile { get; set; }
        public string CeoName { get; set; }
        public string CeoMobile { get; set; }        
        public int? CountOfPersonal { get; set; }
        public decimal? AmountOsLastSales { get; set; }
        public DateTime SaveDate { get; set; }
        public byte IsActive { get; set; }
        public string Ip { get; set; }        
        public int? TypeGroupCompanies { get; set; }
        public string EmailRepresentative { get; set; }
        public string Description { get; set; }

    }
}
