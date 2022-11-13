using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class SkillsOfEmployees
    {
        public int SkilsId { get; set; }
        public int? CustomerId { get; set; }
        public string EmployeName { get; set; }
        public string TrainingCourseName { get; set; }
        public int? Duration { get; set; }
        public string PlaceOfTraining { get; set; }
        public string PictureOfSkill { get; set; }
        public byte? IsActive { get; set; }

        public virtual Customers Customer { get; set; }
    }
}
