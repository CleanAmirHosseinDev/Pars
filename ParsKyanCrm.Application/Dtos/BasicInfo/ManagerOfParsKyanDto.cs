using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int? TitelId { get; set; }
        public string EmailAddress { get; set; }
        public string TwitterId { get; set; }
        public string Picture { get; set; }
        public string ResumeSummary { get; set; }
        public string ResumeFile { get; set; }
        public int? Userid { get; set; }
        public DateTime? SaveAndEditDate { get; set; }

        public SystemSetingDto Position { get; set; }
        public SystemSetingDto Titel { get; set; }
        public UsersDto User { get; set; }

    }
}
