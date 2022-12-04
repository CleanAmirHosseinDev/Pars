using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{

    public class RequestLevelStepSettingDto : PageingParamerDto
    {

        public int? LevelStepSettingIndexId { get; set; }

    }

    public class LevelStepSettingDto
    {
        public int LevelStepSettingIndexId { get; set; }
        public int? KindOfRequest { get; set; }
        public string LevelStepAccessRole { get; set; }
        public string AccessRoleName { get; set; }
        public int? LevelStepIndex { get; set; }
        public string LevelStepStatus { get; set; }
        public string DestLevelStepIndex { get; set; }
        public string DestLevelStepIndexButton { get; set; }
        public string SmsContent { get; set; }
        public bool? SmsType { get; set; }
    }
}
