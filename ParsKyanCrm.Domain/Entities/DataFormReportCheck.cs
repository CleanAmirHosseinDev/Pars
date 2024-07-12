using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class DataFormReportCheck
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
        public string Document { get; set; }
        public double? SystemScore { get; set; }
        public string SuperVisorDescription { get; set; }
        public string CostumerDescriptionBeforEdit { get; set; }
        public string CostumerDescriptionAfterEdit { get; set; }
        public byte IsActive { get; set; }
    }
}
