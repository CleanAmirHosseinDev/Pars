using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class BoardOfDirectors
    {
        public BoardOfDirectors()
        {
            EducationCourses = new HashSet<EducationCourses>();
        }

        public int BoardOfDirectorsId { get; set; }
        public int CustomerId { get; set; }
        public string MemberName { get; set; }
        public int? MemberPostId { get; set; }
        public int? MemberEductionId { get; set; }
        public int? UniversityId { get; set; }
        public string AcademicDegreePicture { get; set; }
        public byte? IsMemeberOfBoard { get; set; }
        public double? OwnershipPercentage { get; set; }
        public int? OwnershipCount { get; set; }
        public double? InsuranceHistory { get; set; }
        public double? ManagersExperience { get; set; }
        public byte? IsActive { get; set; }

        public virtual SystemSeting MemberEduction { get; set; }
        public virtual SystemSeting MemberPost { get; set; }
        public virtual SystemSeting University { get; set; }
        public virtual ICollection<EducationCourses> EducationCourses { get; set; }
    }
}
