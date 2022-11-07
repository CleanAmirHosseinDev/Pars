using ParsKyanCrm.Application.Dtos.BasicInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{
    public class RequestForReatingDto
    {

        public int RequestId { get; set; }
        public string RequestNoStr { get; set; }
        public int? RequestNo { get; set; }
        public int? CustomerId { get; set; }        
        public DateTime? DateOfRequest { get; set; }                
        public DateTime? DateOfConfirmed { get; set; }
        public int? Status { get; set; }
        public int? KindOfRequest { get; set; }

        public CustomersDto Customer { get; set; }
        public SystemSetingDto KindOfRequestNavigation { get; set; }
        public UsersDto User { get; set; }

    }
}
