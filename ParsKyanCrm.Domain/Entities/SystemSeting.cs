using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class SystemSeting
    {
        public SystemSeting()
        {
            BoardOfDirectorsMemberEduction = new HashSet<BoardOfDirectors>();
            BoardOfDirectorsMemberPost = new HashSet<BoardOfDirectors>();
            BoardOfDirectorsUniversity = new HashSet<BoardOfDirectors>();
            Companies = new HashSet<Companies>();
            CustomersHowGetKnowCompany = new HashSet<Customers>();
            CustomersKindOfCompany = new HashSet<Customers>();
            CustomersTypeServiceRequested = new HashSet<Customers>();
            NewsAndContent = new HashSet<NewsAndContent>();
            OtherDocuments = new HashSet<OtherDocuments>();
            RequestForReating = new HashSet<RequestForReating>();
            ReturnRequest = new HashSet<ReturnRequest>();
            UserReferrals = new HashSet<UserReferrals>();
        }

        public int SystemSetingId { get; set; }
        public string Label { get; set; }
        public int? LabeCode { get; set; }
        public string Value { get; set; }
        public byte IsActive { get; set; }
        public int? BaseAmount { get; set; }
        public string TiTleBaseAmount { get; set; }
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
        public virtual ICollection<NewsAndContent> NewsAndContent { get; set; }
        public virtual ICollection<OtherDocuments> OtherDocuments { get; set; }
        public virtual ICollection<RequestForReating> RequestForReating { get; set; }
        public virtual ICollection<ReturnRequest> ReturnRequest { get; set; }
        public virtual ICollection<UserReferrals> UserReferrals { get; set; }
    }
}
