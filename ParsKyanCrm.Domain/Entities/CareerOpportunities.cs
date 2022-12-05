using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class CareerOpportunities
    {
        public int CareerOpportunitiesId { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public int? CityId { get; set; }
        public DateTime? Brithday { get; set; }
        public string Mobile { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public string NationalCode { get; set; }
        public DateTime? SaveDate { get; set; }
        public byte? IsActive { get; set; }
        public string Postion { get; set; }
        public string Education { get; set; }
        public string EducationLevel { get; set; }
        public string ResumeFile { get; set; }
    }
}
