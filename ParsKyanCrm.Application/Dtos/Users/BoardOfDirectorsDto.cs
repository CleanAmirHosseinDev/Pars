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

    public class RequestBoardOfDirectorsDto : PageingParamerDto
    {

        public int? CustomerId { get; set; }

        public int? BoardOfDirectorsId { get; set; }

    }

    public class BoardOfDirectorsDto : BaseEntityDto
    {        

        public int BoardOfDirectorsId { get; set; }
        public int CustomerId { get; set; }
        public string MemberName { get; set; }
        public int? MemberPostId { get; set; }
        public int? MemberEductionId { get; set; }
        public int? UniversityId { get; set; }

        public string AcademicDegreePicture { get; set; }
        public string Result_Final_AcademicDegreePicture { get; set; }
        public string AcademicDegreePictureFull
        {
            get
            {
                if (VaribleForName.IsDebug == true)
                    return ServiceImage.ConvertImageToByte(AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin")) + VaribleForName.BoardOfDirectorsFolder + (string.IsNullOrEmpty(AcademicDegreePicture) ? VaribleForName.No_Photo : AcademicDegreePicture));
                else
                    return ServiceImage.ConvertImageToByte(AppContext.BaseDirectory + VaribleForName.BoardOfDirectorsFolder + (string.IsNullOrEmpty(AcademicDegreePicture) ? VaribleForName.No_Photo : AcademicDegreePicture));
            }
        }

        public byte? IsMemeberOfBoard { get; set; }
        public double? OwnershipPercentage { get; set; }
        public int? OwnershipCount { get; set; }
        public double? InsuranceHistory { get; set; }
        public double? ManagersExperience { get; set; }
        public DateTime? SaveDate { get; set; }
        public SystemSetingDto MemberEduction { get; set; }
        public SystemSetingDto MemberPost { get; set; }
        public SystemSetingDto University { get; set; }
    }
}
