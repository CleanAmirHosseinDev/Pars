using ParsKyanCrm.Domain.Entities.BasicInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Domain.Entities.Users
{
    [Table("Roles")]
    public class Roles
    {

        [Key]
        public int RoleID { get; set; }

        [StringLength(50)]
        public string RoleTitle { get; set; }

        [StringLength(50)]
        public string RoleDesc { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }

    }
}
