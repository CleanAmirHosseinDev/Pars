using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class Users
    {
        public Users()
        {
            NewsAndContent = new HashSet<NewsAndContent>();
            RankingOfCompanies = new HashSet<RankingOfCompanies>();
            RequestReferencesReciveUserNavigation = new HashSet<RequestReferences>();
            RequestReferencesSendUserNavigation = new HashSet<RequestReferences>();
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

        public virtual Customers Customer { get; set; }
        public virtual ICollection<NewsAndContent> NewsAndContent { get; set; }
        public virtual ICollection<RankingOfCompanies> RankingOfCompanies { get; set; }
        public virtual ICollection<RequestReferences> RequestReferencesReciveUserNavigation { get; set; }
        public virtual ICollection<RequestReferences> RequestReferencesSendUserNavigation { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
