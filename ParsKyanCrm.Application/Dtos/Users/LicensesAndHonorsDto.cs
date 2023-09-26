using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{

    public class RequestLicensesAndHonorsDto : PageingParamerDto
    {

        public int? LicensesAndHonorsId { get; set; }        

    }

    public class LicensesAndHonorsDto : BaseEntityDto
    {

        public int LicensesAndHonorsId { get; set; }
        public string Title { get; set; }

        public string Picture { get; set; }
        public string Result_Final_Picture { get; set; }
        public string PictureFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(Picture, VaribleForName.LicensesAndHonorsFolder);
            }
        }

        public int? Userid { get; set; }
        public DateTime? SaveOrEditDate { get; set; }

        public UsersDto User { get; set; }

    }
}
