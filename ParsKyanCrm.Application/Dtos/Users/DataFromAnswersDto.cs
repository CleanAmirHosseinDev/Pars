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
    public class RequestDataFromAnswersDto : PageingParamerDto
    {
        public int? RequestId { get; set; }
        public int? FormId { get; set; }
    }

    public class DataFromAnswersDto
    {
        public int AnswerId { get; set; }
        public int? RequestId { get; set; }
        public int? FormId { get; set; }
        public int? DataFormQuestionId { get; set; }
        public string Answer { get; set; }
        public string Description { get; set; }
        public string FileName1 { get; set; }
        public byte? IsActive { get; set; }
        public IFormFile Result_Final_FileName1 { get; set; }
        public string FileName1Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(FileName1, VaribleForName.CustomerFurtherInfoFolder, false);
            }
        }

    }
}
