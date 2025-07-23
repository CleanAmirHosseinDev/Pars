using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class RequestForRating
    {
        public RequestForRating()
        {

            RequestReferences = new HashSet<RequestReferences>();
        }

        public int RequestId { get; set; }
        public int? RequestNo { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? DateOfRequest { get; set; }
        public DateTime? DateOfConfirmed { get; set; }
        public int? KindOfRequest { get; set; }
        public bool IsFinished { get; set; }

        public DateTime? ChangeDate { get; set; }

        public string Assessment { get; set; }

        public string ReasonAssessment1 { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual SystemSeting KindOfRequestNavigation { get; set; }

        public virtual ICollection<RequestReferences> RequestReferences { get; set; }
        public string CodalNumber { get; set; }

        public DateTime? CodalDate { get; set; }
        public string Comment { get; set; }
    }
}
