using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class SystemSeting11
    {
        public SystemSeting11()
        {
            BoardOfDirectorsMemberEduction = new HashSet<BoardOfDirectors>();
            BoardOfDirectorsMemberPost = new HashSet<BoardOfDirectors>();
            BoardOfDirectorsUniversity = new HashSet<BoardOfDirectors>();
            Companies = new HashSet<Companies>();
            CustomersHowGetKnowCompany = new HashSet<Customers>();
            CustomersKindOfCompany = new HashSet<Customers>();
            CustomersTypeServiceRequested = new HashSet<Customers>();
            LevelStepSetting = new HashSet<LevelStepSetting>();
            NewsAndContent = new HashSet<NewsAndContent>();
            OtherDocuments = new HashSet<OtherDocuments>();
            RequestForRating = new HashSet<RequestForRating>();
            ServiceFee = new HashSet<ServiceFee>();
        }

        public int SystemSetingId { get; set; }
        public string Label { get; set; }
        public int? LabelCode { get; set; }
        public string Value { get; set; }
        public byte IsActive { get; set; }
        public int? BaseAmount { get; set; }
        public string TitleBaseAmount { get; set; }
        public double? FromAmount { get; set; }
        public double? ToAmount { get; set; }
        public int? ChangeBy { get; set; }
        public DateTime? ChangeDate { get; set; }

        public virtual Users ChangeByNavigation { get; set; }
        public virtual ICollection<BoardOfDirectors> BoardOfDirectorsMemberEduction { get; set; }
        public virtual ICollection<BoardOfDirectors> BoardOfDirectorsMemberPost { get; set; }
        public virtual ICollection<BoardOfDirectors> BoardOfDirectorsUniversity { get; set; }
        public virtual ICollection<Companies> Companies { get; set; }
        public virtual ICollection<Customers> CustomersHowGetKnowCompany { get; set; }
        public virtual ICollection<Customers> CustomersKindOfCompany { get; set; }
        public virtual ICollection<Customers> CustomersTypeServiceRequested { get; set; }
        public virtual ICollection<LevelStepSetting> LevelStepSetting { get; set; }
        public virtual ICollection<NewsAndContent> NewsAndContent { get; set; }
        public virtual ICollection<OtherDocuments> OtherDocuments { get; set; }
        public virtual ICollection<RequestForRating> RequestForRating { get; set; }
        public virtual ICollection<ServiceFee> ServiceFee { get; set; }
    }
}
