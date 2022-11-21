using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class RequestReferences
    {
        public int ReferenceId { get; set; }
        public int? Requestid { get; set; }
        public int? LevelStepsId { get; set; }
        public DateTime? ResiveTime { get; set; }
        public DateTime? SendTime { get; set; }
        public string Comment { get; set; }
        public int? ReciveUser { get; set; }
        public int? ReciveRole { get; set; }
        public int? SendUser { get; set; }

        public virtual LevelStepSetting LevelSteps { get; set; }
        public virtual Roles ReciveRoleNavigation { get; set; }
        public virtual Users ReciveUserNavigation { get; set; }
        public virtual RequestForRating Request { get; set; }
        public virtual Users SendUserNavigation { get; set; }
    }
}
