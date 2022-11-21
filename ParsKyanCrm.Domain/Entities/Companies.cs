using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class Companies
    {
        public Companies()
        {
            RankingOfCompanies = new HashSet<RankingOfCompanies>();
        }

        public int CompaniesId { get; set; }
        public string CompanyName { get; set; }
        public int? CompanyGroupId { get; set; }
        public int? KindOfCompany { get; set; }
        public int? IsActive { get; set; }

        public virtual CompanyGroup CompanyGroup { get; set; }
        public virtual SystemSeting11 KindOfCompanyNavigation { get; set; }
        public virtual ICollection<RankingOfCompanies> RankingOfCompanies { get; set; }
    }
}
