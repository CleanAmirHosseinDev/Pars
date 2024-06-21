using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class DataFormQuestions
    {
        public int DataFormQuestionId { get; set; }
        public int? DataFormId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionName { get; set; }
        public string QuestionType { get; set; }       
        public int? QuestionOrder { get; set; }
        public int? Score { get; set; }
        public string HelpText { get; set; }
    }
}
