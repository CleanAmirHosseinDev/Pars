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
    public class RequestDataFormReportCheckDto : PageingParamerDto
    {
        public int CheckId { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public int? FormId { get; set; }
        public int? RequestId { get; set; }
        public int? CategoryId { get; set; }
        public int? DocumentId { get; set; }
        public byte? IsActive { get; set; }
    }

    public class DataFormReportCheckDto
    {
        public int CheckId { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public int? FormId { get; set; }
        public int? RequestId { get; set; }
        public int? CategoryId { get; set; }
        public int? DocumentId { get; set; }
        public string FormCode { get; set; }
        public string AnswerBeforEdit { get; set; }
        public string AnswerAfterEdit { get; set; }
        public string DocumentBeforEdit { get; set; }
        public string DocumentAfterEdit { get; set; }
        public double? SystemScore { get; set; }
        public string SuperVisorDescription { get; set; }
        public string CostumerDescriptionBeforEdit { get; set; }
        public string CostumerDescriptionAfterEdit { get; set; }
        public byte? IsActive { get; set; }

    }
}
