using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.BasicInfo
{

    public class RequestLicensesAndHonorsDto : PageingParamerDto
    {

        public int? LicensesAndHonorsId { get; set; }        

    }

    public class LicensesAndHonorsDto
    {

        public int LicensesAndHonorsId { get; set; }
        public string Titel { get; set; }

        public string Picture { get; set; }
        public string Result_Final_Picture { get; set; }
        public string PictureFull
        {
            get
            {
                if (VaribleForName.IsDebug == true)
                    return ServiceImage.ConvertImageToByte(AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin")) + VaribleForName.LicensesAndHonorsFolder + (string.IsNullOrEmpty(Picture) ? VaribleForName.No_Photo : Picture));
                else
                    return ServiceImage.ConvertImageToByte(AppContext.BaseDirectory + VaribleForName.LicensesAndHonorsFolder + (string.IsNullOrEmpty(Picture) ? VaribleForName.No_Photo : Picture));
            }
        }

        public int? Userid { get; set; }
        public DateTime? SaveOrdEditDate { get; set; }

        public UsersDto User { get; set; }

    }
}
