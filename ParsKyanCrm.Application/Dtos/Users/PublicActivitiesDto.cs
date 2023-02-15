using ParsKyanCrm.Application.Dtos.BasicInfo;
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

    public class RequestPublicActivitiesDto : PageingParamerDto
    {
        public int? RequestId { get; set; }
    }

    public class PublicActivitiesDto : BaseEntityDto
    {
        public int PublicActivitiesID { get; set; }
        public int? RequestId { get; set; }

        public int? CustomerID { get; set; }
        public bool IsPublicActivityFile { get; set; }
        public string IsPublicActivityFileStr { get; set; }
        public string Investment { get; set; }

        public string EmploymentDisabled { get; set; }


    }

}
