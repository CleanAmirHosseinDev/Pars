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
        public DateTime? SendTime { get; set; }
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

    }

    public class ValidatorRequestReferencesDto : AbstractValidator<RequestReferencesDto>
    {

        public ValidatorRequestReferencesDto()
        {

            RuleFor(p => p.Request.KindOfRequest).NotEmpty().WithMessage("نوع درخواست را انتخاب کنید");

        }

    }


}
