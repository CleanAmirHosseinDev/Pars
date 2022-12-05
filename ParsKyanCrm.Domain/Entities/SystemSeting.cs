using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class SystemSeting
    {
        public SystemSeting()
        {
            Activity = new HashSet<Activity>();
            BoardOfDirectorsMemberEduction = new HashSet<BoardOfDirectors>();
            BoardOfDirectorsMemberPost = new HashSet<BoardOfDirectors>();
            BoardOfDirectorsUniversity = new HashSet<BoardOfDirectors>();
            CompaniesCompanyGroup = new HashSet<Companies>();
            CompaniesKindOfCompanyNavigation = new HashSet<Companies>();
            Contract = new HashSet<Contract>();
            CustomersHowGetKnowCompany = new HashSet<Customers>();
            CustomersKindOfCompany = new HashSet<Customers>();
            CustomersTypeServiceRequested = new HashSet<Customers>();
            ManagerOfParsKyanPosition = new HashSet<ManagerOfParsKyan>();
            ManagerOfParsKyanTitel = new HashSet<ManagerOfParsKyan>();
            NewsAndContent = new HashSet<NewsAndContent>();
            OtherDocuments = new HashSet<OtherDocuments>();
            RequestForRating = new HashSet<RequestForRating>();
            ServiceFee = new HashSet<ServiceFee>();
        }

        public int SystemSetingId { get; set; }
        public string Label { get; set; }
        public int? ParentCode { get; set; }
        public byte IsActive { get; set; }
        public int? BaseAmount { get; set; }
        public string TitleBaseAmount { get; set; }
        public double? FromAmount { get; set; }
        public double? ToAmount { get; set; }
        public int? ChangeBy { get; set; }
        public DateTime? ChangeDate { get; set; }
        public string Value { get; set; }
        public int? LabelCode { get; set; }

        public virtual ICollection<Activity> Activity { get; set; }
        public virtual ICollection<BoardOfDirectors> BoardOfDirectorsMemberEduction { get; set; }
        public virtual ICollection<BoardOfDirectors> BoardOfDirectorsMemberPost { get; set; }
        public virtual ICollection<BoardOfDirectors> BoardOfDirectorsUniversity { get; set; }
        public virtual ICollection<Companies> CompaniesCompanyGroup { get; set; }
        public virtual ICollection<Companies> CompaniesKindOfCompanyNavigation { get; set; }
        public virtual ICollection<Contract> Contract { get; set; }
        public virtual ICollection<Customers> CustomersHowGetKnowCompany { get; set; }
        public virtual ICollection<Customers> CustomersKindOfCompany { get; set; }
        public virtual ICollection<Customers> CustomersTypeServiceRequested { get; set; }
        public virtual ICollection<ManagerOfParsKyan> ManagerOfParsKyanPosition { get; set; }
        public virtual ICollection<ManagerOfParsKyan> ManagerOfParsKyanTitel { get; set; }
        public virtual ICollection<NewsAndContent> NewsAndContent { get; set; }
        public virtual ICollection<OtherDocuments> OtherDocuments { get; set; }
        public virtual ICollection<RequestForRating> RequestForRating { get; set; }
        public virtual ICollection<ServiceFee> ServiceFee { get; set; }
    }
}
