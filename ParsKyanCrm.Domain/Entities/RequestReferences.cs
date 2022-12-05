using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class RequestReferences
    {
        public int ReferenceId { get; set; }
        public int? Requestid { get; set; }
        public string LevelStepAccessRole { get; set; }
        public int? DestLevelStepIndex { get; set; }
        public DateTime? SendTime { get; set; }
        public string Comment { get; set; }
        public int? SendUser { get; set; }

        public virtual RequestForRating Request { get; set; }
        public virtual Users SendUserNavigation { get; set; }
    }
}
