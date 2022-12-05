using ParsKyanCrm.Common;
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
        public int? ActivityTitel { get; set; }
        public string ActivityComment { get; set; }

        public string Picture1 { get; set; }
        public string Result_Final_Picture1 { get; set; }
        public string Picture1Full
        {
            get
            {
                if (VaribleForName.IsDebug == true)
                    return ServiceImage.ConvertImageToByte(AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin")) + VaribleForName.ActivityFolder + (string.IsNullOrEmpty(Picture1) ? VaribleForName.No_Photo : Picture1));
                else
                    return ServiceImage.ConvertImageToByte(AppContext.BaseDirectory + VaribleForName.ActivityFolder + (string.IsNullOrEmpty(Picture1) ? VaribleForName.No_Photo : Picture1));
            }
        }

        public string Picture2 { get; set; }
        public string Picture2Full
        {
            get
            {
                if (VaribleForName.IsDebug == true)
                    return ServiceImage.ConvertImageToByte(AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin")) + VaribleForName.ActivityFolder + (string.IsNullOrEmpty(Picture2) ? VaribleForName.No_Photo : Picture2));
                else
                    return ServiceImage.ConvertImageToByte(AppContext.BaseDirectory + VaribleForName.ActivityFolder + (string.IsNullOrEmpty(Picture2) ? VaribleForName.No_Photo : Picture2));
            }
        }
        public string Result_Final_Picture2 { get; set; }

        public SystemSetingDto ActivityTitelNavigation { get; set; }

    }
}
