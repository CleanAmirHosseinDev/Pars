using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class Users
    {
        public Users()
        {
            AboutUs = new HashSet<AboutUs>();
            LicensesAndHonors = new HashSet<LicensesAndHonors>();
            ManagerOfParsKyan = new HashSet<ManagerOfParsKyan>();
            NewsAndContent = new HashSet<NewsAndContent>();
            RankingOfCompanies = new HashSet<RankingOfCompanies>();
            RequestReferences = new HashSet<RequestReferences>();
            UserRoles = new HashSet<UserRoles>();
        }

        public int UserId { get; set; }
        public int? CustomerId { get; set; }
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Ip { get; set; }
        public byte IsActive { get; set; }

        public string AuthenticateCode { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual ICollection<AboutUs> AboutUs { get; set; }
        public virtual ICollection<LicensesAndHonors> LicensesAndHonors { get; set; }
        public virtual ICollection<ManagerOfParsKyan> ManagerOfParsKyan { get; set; }
        public virtual ICollection<NewsAndContent> NewsAndContent { get; set; }
        public virtual ICollection<RankingOfCompanies> RankingOfCompanies { get; set; }
        public virtual ICollection<RequestReferences> RequestReferences { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
