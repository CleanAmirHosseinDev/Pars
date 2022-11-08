using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class RequestForReating
    {
        public RequestForReating()
        {
            RequestReferences = new HashSet<RequestReferences>();
            ReturnRequest = new HashSet<ReturnRequest>();
        }

        public int RequestId { get; set; }
        public int? RequestNo { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? DateOfRequest { get; set; }
        public DateTime? DateOfConfirmed { get; set; }
        public int? Status { get; set; }
        public int? KindOfRequest { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual SystemSeting KindOfRequestNavigation { get; set; }
        public virtual ICollection<RequestReferences> RequestReferences { get; set; }
        public virtual ICollection<ReturnRequest> ReturnRequest { get; set; }
    }
}
