using FluentValidation;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Enums;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{
    public class RequestRequestForRatingDto : PageingParamerDto
    {

        public int? CustomerId { get; set; }

        public string LoginName { get; set; }

        public int? RequestId { get; set; }

        public int? DestLevelStepIndex { get; set; }

        public int? UserID { get; set; }

    }
    public class RequestForRatingDto
    {


        public string ReciveUserName { get; set; }

        public string CompanyName { get; set; }
        public string DestLevelStepIndex { get; set; }
        public int RequestId { get; set; }
        public string RequestNoStr { get; set; }
        public int? RequestNo { get; set; }
        public int? CustomerId { get; set; }

        public DateTime? DateOfRequest { get; set; }
        public string DateOfRequestStr
        {
            get
            {
                if (DateOfRequest.HasValue) return DateTimeOperation.ToPersianDate(DateOfRequest.Value);
                return string.Empty;
            }
        }

        public DateTime? DateOfConfirmed { get; set; }
        public string DateOfConfirmedStr
        {
            get
            {
                if (DateOfConfirmed.HasValue) return DateTimeOperation.ToPersianDate(DateOfConfirmed.Value);
                return string.Empty;
            }
        }

        public DateTime? ChangeDate { get; set; }

        public int? KindOfRequest { get; set; }

        public string KindOfRequestName { get; set; }

        public bool IsFinished { get; set; }

        public string LevelStepStatus { get; set; }

        public string LevelStepAccessRole { get; set; }

        public string AgentName { get; set; }

        public string AgentMobile { get; set; }
        public string DestLevelStepAccessRole { get; set; }
        public string Comment { get; set; }
        public string DestLevelStepIndexButton { get; set; }

        public string ReciveUser { get; set; }

    }
}
