using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class UserReferrals
    {
        public int UserReferralsId { get; set; }
        public int? UserId { get; set; }
        public string UserReferrals1 { get; set; }
        public byte? IsActive { get; set; }
        public DateTime? ChangeDate { get; set; }
        public int? ChangeBy { get; set; }
        public int? KindOfRequest { get; set; }

        public virtual Users ChangeByNavigation { get; set; }
        public virtual SystemSeting KindOfRequestNavigation { get; set; }
        public virtual Users User { get; set; }
    }
}
