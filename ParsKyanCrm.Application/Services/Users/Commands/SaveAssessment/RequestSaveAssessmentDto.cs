namespace ParsKyanCrm.Application.Services.Users.Commands.SaveAssessment
{
    public class RequestSaveAssessmentDto
    {
        public string CaptchaCodes { get; set; }

        public string RequestID { get; set; }

        public string Assessment { get; set; }

        public string ReasonAssessment1 { get; set; }

    }
}
