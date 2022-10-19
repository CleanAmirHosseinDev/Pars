using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{
    public class RequestForReatingDto
    {
        
        public int RequestID { get; set; }

        public int? RequestNo { get; set; }

        
        public int? CustomerID { get; set; }

        
        public int? UserID { get; set; }

        public DateTime? DateOfRequest { get; set; }

        public DateTime? DateOfAssignUsers { get; set; }

        public DateTime? DateOfAcceptRequest { get; set; }

        public DateTime? DateOfConfirmed { get; set; }

        public int? Status { get; set; }

        public CustomersDto Customer { get; set; }

        public UsersDto User { get; set; }
    }
}
