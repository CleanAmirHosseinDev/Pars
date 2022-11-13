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

        public int LevelStepSettingId { get; set; }
        public int? KindOfRequest { get; set; }
        public int? SrcLevel { get; set; }
        public int? SrcStep { get; set; }
        public string DestLevelSteps { get; set; }
        public string AccessRoles { get; set; }
        public string StepComment { get; set; }
        public string Refrences { get; set; }

        public virtual SystemSeting KindOfRequestNavigation { get; set; }
        public virtual ICollection<RequestReferences> RequestReferences { get; set; }
    }
}
