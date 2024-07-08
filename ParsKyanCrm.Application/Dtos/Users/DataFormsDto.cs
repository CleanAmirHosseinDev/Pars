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
    public class RequestDataFormsDto : PageingParamerDto
    {
        public int? FormId { get; set; }
        public int? CategoryId { get; set; }
    }

    public class DataFormsDto
    {
        public int FormId { get; set; }
        public string FormCode { get; set; }
        public string FormTitle { get; set; }
        public bool? IsTable { get; set; }
        public int? CategoryId { get; set; }
        public byte? IsActive { get; set; }

    }
}
