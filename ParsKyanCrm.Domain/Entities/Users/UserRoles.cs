using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Domain.Entities.Users
{
    [Table("UserRoles")]
    public class UserRoles
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [ForeignKey("Role")]
        public int RoleID { get; set; }

        public DateTime? ExpiredDate { get; set; }

        public string Roles { get; set; }

        public virtual Users User { get; set; }

        public virtual Roles Role { get; set; }

    }
}
