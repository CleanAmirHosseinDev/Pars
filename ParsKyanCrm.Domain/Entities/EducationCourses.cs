using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class EducationCourses
    {
        public int EducationCoursesId { get; set; }
        public int? CustomerId { get; set; }
        public int? BoardOfDirectorsId { get; set; }
        public string TitelCourses { get; set; }
        public int? TimeOfCource { get; set; }
        public string PlaceOfTraining { get; set; }
        public string CourseOrganizer { get; set; }
        public byte? IsActive { get; set; }

        public virtual BoardOfDirectors BoardOfDirectors { get; set; }
        public virtual Customers Customer { get; set; }
    }
}
