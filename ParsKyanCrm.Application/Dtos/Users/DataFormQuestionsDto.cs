using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{

    public class RequestDataFormQuestionsDto : PageingParamerDto
    {
        public int? DataFormId { get; set; }
        public int DataFormQuestionId { get; set; }
        public int? DataFormType { get; set; } // 1 - Normal Question  | 2 - Corporate Question
    }

    public class DataFormQuestionsDto
    {

        public int DataFormQuestionId { get; set; }
        public int? DataFormId { get; set; }
        public int? DataFormType { get; set; } // 1 - Normal Question  | 2 - Corporate Question
        public string? QuestionText { get; set; }
        public string? QuestionName { get; set; }
        public string? QuestionType { get; set; }       
        public int? QuestionOrder { get; set; }
        public int? Score { get; set; }
        public string? HelpText { get; set; }
    }
}
