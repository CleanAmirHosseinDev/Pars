using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.BasicInfo
{

    public class RequestActivityDto : PageingParamerDto
    {

        public int? ActivityId { get; set; }

    }

    public class ActivityDto
    {

        public int ActivityId { get; set; }
        public int? ActivityTitle { get; set; }
        public string ActivityComment { get; set; }

        public string Picture1 { get; set; }
        public string Result_Final_Picture1 { get; set; }
        public string Picture1Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(Picture1, VaribleForName.ActivityFolder);
            }
        }

        public string Picture2 { get; set; }
        public string Picture2Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(Picture2, VaribleForName.ActivityFolder);
            }
        }
        public string Result_Final_Picture2 { get; set; }

        public SystemSetingDto ActivityTitleNavigation { get; set; }

    }
}
