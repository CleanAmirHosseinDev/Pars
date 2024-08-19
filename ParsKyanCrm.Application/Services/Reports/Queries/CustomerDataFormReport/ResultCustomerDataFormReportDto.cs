using DocumentFormat.OpenXml.Office2010.ExcelAc;
using ParsKyanCrm.Domain.Entities;
using System.Collections.Generic;

namespace ParsKyanCrm.Application.Services.Reports.Queries.CustomerDataFormReport
{
    public class ReportQuestion
    {
        public int DataFormQuestionId { get; set; }
        public int? DataFormId { get; set; }
        public string DataFormTitle { get; set; }
        public int AnswerId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public int? Score { get; set; }
        public int? Version { get; set; }
        public string Answer { get; set; }
        public double? SystemScore { get; set; }
        public double? AnalizeScore { get; set; }
        public string Description { get; set; }

        public ReportQuestion(int dataFormQuestionId, int? dataFormId, int answerId, string questionText,
            string questionType, int? score, int? version, string answer, double? systemScore, double? analizeScore,
            string description, string dataFormTitle)
        {
            DataFormQuestionId = dataFormQuestionId;
            DataFormId = dataFormId;
            AnswerId = answerId;
            QuestionText = questionText;
            QuestionType = questionType;
            Score = score;
            Version = version;
            Answer = answer;
            SystemScore = systemScore;
            AnalizeScore = analizeScore;
            Description = description;
            DataFormTitle = dataFormTitle;
        }
    }
    public class ResultCustomerDataFormReportDto
    {
        public string CustomerName { get; set; }
        public int? RequestNo { get; set; }
        public List<ReportQuestion> Reports { get; set; }
    }
}
