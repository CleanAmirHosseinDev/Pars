
using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{

    public class RequestCompaniesDto : PageingParamerDto
    {

        public int? CompaniesId { get; set; }

    }

    public class CompaniesDto : BaseEntityDto
    {

        public int CompaniesId { get; set; }
        public string CompanyName { get; set; }
        public int? CompanyGroupId { get; set; }
        public int? KindOfCompany { get; set; }

        public SystemSetingDto CompanyGroup { get; set; }
        public SystemSetingDto KindOfCompanyNavigation { get; set; }

    }
}
