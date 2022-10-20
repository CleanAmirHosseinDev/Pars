using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class RequestReferences
    {
        public int ReferenceId { get; set; }
        public int? Requestid { get; set; }
        public int? CurrentUser { get; set; }
        public DateTime? ResiveTime { get; set; }
        public DateTime? SendTime { get; set; }
        public string Comment { get; set; }

        public virtual Users CurrentUserNavigation { get; set; }
        public virtual RequestForReating Request { get; set; }
    }
}
