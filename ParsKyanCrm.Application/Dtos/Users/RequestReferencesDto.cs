using FluentValidation;
using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{

    public class RequestRequestReferencesDto : PageingParamerDto
    {
        public int? Requestid { get; set; }
    }

    public class RequestReferencesDto
    {
        public int ReferenceId { get; set; }
        public int? Requestid { get; set; }
        public string DestLevelStepIndex { get; set; }

        public string ReciveUser { get; set; }

        public DateTime? SendTime { get; set; }
        public string SendTimeStr
        {
            get
            {

                return SendTime.HasValue ? Infrastructure.DateTimeOperation.ToPersianDate(SendTime.Value) : string.Empty;

            }
        }

        public string SendTimeTimeStr
        {
            get
            {

                return SendTime.HasValue ? SendTime.Value.ToShortTimeString() : string.Empty;

            }
        }

        public string Comment { get; set; }
        public int? SendUser { get; set; }

        public string LevelStepAccessRole { get; set; }

        public string LevelStepStatus { get; set; }

        public RequestForRatingDto Request { get; set; }
        public UsersDto SendUserNavigation { get; set; }

        public string AgentName { get; set; }

        public string RealName { get; set; }

        public string UserName { get; set; }

        public string RoleDesc { get; set; }

        public string KindOfRequestName { get; set; }

        public string RequestNo { get; set; }

        public int? LevelStepIndex { get; set; }
        public int? DestLevelStepAccessRole { get; set; }
        public string UserRoleDes { get; set; }

        public string SmsContent { get; set; }

        public bool? SmsType { get; set; }

        public string DestLevelStepIndexButton { get; set; }
        public string CompanyName { get; set; }

    }

    public class ValidatorRequestReferencesDto : AbstractValidator<RequestReferencesDto>
    {

        public ValidatorRequestReferencesDto()
        {

            RuleFor(p => p.Request.KindOfRequest).NotEmpty().WithMessage("نوع درخواست را انتخاب کنید");

        }

    }


}
