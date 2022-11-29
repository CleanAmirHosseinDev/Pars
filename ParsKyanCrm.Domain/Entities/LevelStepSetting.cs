using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class LevelStepSetting
    {
        public LevelStepSetting()
        {
            RequestReferences = new HashSet<RequestReferences>();
        }

        public int LevelStepSettingIndexId { get; set; }
        public int? KindOfRequest { get; set; }
        public string LevelStepAccessRole { get; set; }
        public string AccessRoleName { get; set; }
        public int? LevelStepIndex { get; set; }
        public string LevelStepStatus { get; set; }
        public string DestLevelStep { get; set; }
        public string DestLevelStepButton { get; set; }
        public string SmsContent { get; set; }
        public bool? SmsType { get; set; }

        public virtual ICollection<RequestReferences> RequestReferences { get; set; }
    }
}
