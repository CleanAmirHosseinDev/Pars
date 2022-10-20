using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class WorkExperience
    {
        public int WorkExperinceId { get; set; }
        public int? CustomerId { get; set; }
        public int? BoardOfDirectorsId { get; set; }
        public int? InsuranceHistory { get; set; }
        public int? OfficialNewspaper { get; set; }
        public int? ManagersExperience { get; set; }
        public string InsurancListPicture { get; set; }
        public byte? IsActive { get; set; }

        public virtual BoardOfDirectors BoardOfDirectors { get; set; }
        public virtual Customers Customer { get; set; }
    }
}
