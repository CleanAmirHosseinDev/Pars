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

    }
    public class RequestForRatingDto
    {

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

        public int? KindOfRequest { get; set; }

        public bool IsFinished { get; set; }

        public CustomersDto Customer { get; set; }
        public SystemSetingDto KindOfRequestNavigation { get; set; }

    }
}
