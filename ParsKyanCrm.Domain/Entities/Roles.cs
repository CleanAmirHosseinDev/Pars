using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class Roles
    {
        public Roles()
        {
            RequestReferences = new HashSet<RequestReferences>();
            UserRoles = new HashSet<UserRoles>();
        }

        public int RoleId { get; set; }
        public string RoleTitle { get; set; }
        public string RoleDesc { get; set; }

        public virtual ICollection<RequestReferences> RequestReferences { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
