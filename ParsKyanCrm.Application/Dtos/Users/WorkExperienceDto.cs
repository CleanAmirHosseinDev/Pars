using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{


    public class RequestWorkExperienceDto : PageingParamerDto
    {

        public int? SkilsId { get; set; }

        public int? CustomerId { get; set; }

        public int? BoardOfDirectorsId { get; set; }

    }

    public class WorkExperienceDto : BaseEntityDto
    {

        public int SkilsId { get; set; }
        public int? CustomerId { get; set; }
        public int? BoardOfDirectorsId { get; set; }
        public int? InsuranceHistory { get; set; }
        public int? OfficialNewspaperHistory { get; set; }
        public int? EtcHistory { get; set; }

        public string PictureOfEtcHistory { get; set; }
        public string Result_Final_PictureOfEtcHistory { get; set; }
        public string PictureOfEtcHistoryFull
        {
            get
            {
                if (VaribleForName.IsDebug == true)
                    return ServiceImage.ConvertImageToByte(AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin")) + VaribleForName.WorkExperienceFolder + (string.IsNullOrEmpty(PictureOfEtcHistory) ? VaribleForName.No_Photo : PictureOfEtcHistory));
                else
                    return ServiceImage.ConvertImageToByte(AppContext.BaseDirectory + VaribleForName.WorkExperienceFolder + (string.IsNullOrEmpty(PictureOfEtcHistory) ? VaribleForName.No_Photo : PictureOfEtcHistory));
            }
        }


        public DateTime? SaveDate { get; set; }

        public BoardOfDirectorsDto BoardOfDirectors { get; set; }
        public CustomersDto Customer { get; set; }

    }
}
