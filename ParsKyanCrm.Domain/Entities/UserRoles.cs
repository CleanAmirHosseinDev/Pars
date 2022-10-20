using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class UserRoles
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public string Roles { get; set; }

        public virtual Roles Role { get; set; }
        public virtual Users User { get; set; }
    }
}
