
using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class WorkExperience
    {
        public int SkilsId { get; set; }
        public int? CustomerId { get; set; }
        public int? BoardOfDirectorsId { get; set; }
        public int? InsuranceHistory { get; set; }
        public int? OfficialNewspaperHistory { get; set; }
        public int? EtcHistory { get; set; }

        public byte? IsActive { get; set; }

        public string PictureOfEtcHistory { get; set; }
               
        public DateTime? SaveDate { get; set; }

        public virtual BoardOfDirectors BoardOfDirectors { get; set; }
        public virtual Customers Customer { get; set; }
    }
}
