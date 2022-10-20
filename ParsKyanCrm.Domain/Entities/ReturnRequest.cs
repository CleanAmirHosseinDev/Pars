using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class ReturnRequest
    {
        public int ReturnRequestId { get; set; }
        public int CustomerId { get; set; }
        public int? ReturnResult { get; set; }
        public DateTime? DateOfReturn { get; set; }
        public int? UserId { get; set; }
        public int? RequestId { get; set; }

        public virtual RequestForReating Request { get; set; }
        public virtual SystemSeting ReturnResultNavigation { get; set; }
        public virtual Users User { get; set; }
    }
}
