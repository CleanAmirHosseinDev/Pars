using Microsoft.AspNetCore.Http;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
using System;

namespace ParsKyanCrm.Application.Dtos.BasicInfo
{

    public class RequestManagerOfParsKyanDto : PageingParamerDto
    {

        public int? ManagersId { get; set; }

    }

    public class ManagerOfParsKyanDto
    {

        public int ManagersId { get; set; }
        public string NameOfManager { get; set; }
        public int? PositionId { get; set; }
        public int? TitleId { get; set; }
        public string EmailAddress { get; set; }
        public string TwitterId { get; set; }

        public string Picture { get; set; }
        public string Result_Final_Picture { get; set; }
        public string PictureFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(Picture, VaribleForName.ManagerOfParsKyanFolder);
            }
        }

        public string ResumeSummary { get; set; }

        public string ResumeFile { get; set; }
        public string ResumeFileFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(ResumeFile, VaribleForName.ManagerOfParsKyanFolder, false);
            }
        }
        public IFormFile Result_Final_ResumeFile { get; set; }

        public int? Userid { get; set; }
        public DateTime? SaveAndEditDate { get; set; }

        public SystemSetingDto Position { get; set; }
        public SystemSetingDto Title { get; set; }
        public UsersDto User { get; set; }

    }
}
