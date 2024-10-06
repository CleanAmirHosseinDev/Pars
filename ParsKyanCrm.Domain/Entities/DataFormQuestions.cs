using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class DataFormQuestions
    {
        public int DataFormQuestionId { get; set; }
        public int? DataFormId { get; set; }
        public int? DataFormType { get; set; } // 1 - Normal Question  | 2 - Corporate Question
        public string QuestionText { get; set; }
        public string QuestionName { get; set; }
        public string QuestionType { get; set; }
        public int? QuestionOrder { get; set; }
        public int? Score { get; set; }
        public string HelpText { get; set; }
        public byte IsActive { get; set; }
        //public virtual DataForms DataForms { get; set; }
        public int? Version { get; set; }
        public int? QuestionLevel { get; set; }
        
    }
}
