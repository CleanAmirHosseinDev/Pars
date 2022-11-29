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
    public class RequestEducationCoursesDto : PageingParamerDto
    {
        public int? EducationCoursesId { get; set; }
        public int? CustomerId { get; set; }
        public int? BoardOfDirectorsId { get; set; }
    }

    public class EducationCoursesDto : BaseEntityDto
    {
        public int EducationCoursesId { get; set; }
        public int? CustomerId { get; set; }
        public int? BoardOfDirectorsId { get; set; }
        public string TitelCourses { get; set; }
        public int? TimeOfCource { get; set; }
        public string PlaceOfTraining { get; set; }
        public string CourseOrganizer { get; set; }

        public string PictureOfEducationCourse { get; set; }
        public string Result_Final_PictureOfEducationCourse { get; set; }
        public string PictureOfEducationCourseFull
        {
            get
            {
                if (VaribleForName.IsDebug == true)
                    return ServiceImage.ConvertImageToByte(AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin")) + VaribleForName.EducationCoursesFolder + (string.IsNullOrEmpty(PictureOfEducationCourse) ? VaribleForName.No_Photo : PictureOfEducationCourse));
                else
                    return ServiceImage.ConvertImageToByte(AppContext.BaseDirectory + VaribleForName.EducationCoursesFolder + (string.IsNullOrEmpty(PictureOfEducationCourse) ? VaribleForName.No_Photo : PictureOfEducationCourse));
            }
        }

        public DateTime? SaveDate { get; set; }

        public BoardOfDirectorsDto BoardOfDirectors { get; set; }
        public CustomersDto Customer { get; set; }
    }
}
