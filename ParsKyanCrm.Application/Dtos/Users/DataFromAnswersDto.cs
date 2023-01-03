using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{
    public class RequestDataFromAnswersDto : PageingParamerDto
    {
        public int? CustomerId { get; set; }
        public int? FormId { get; set; }
    }

    public class DataFromAnswersDto
    {
        public int AnswerId { get; set; }
        public int? CustomerId { get; set; }
        public int? FormId { get; set; }
        public int? DataFormQuestionId { get; set; }
        public string Answer { get; set; }
    }
}
