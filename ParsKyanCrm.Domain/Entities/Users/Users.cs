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
    [Table("Users")]
    public class Users : BaseEntity
    {

        [Key]
        public int UserID { get; set; }            

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(500)]
        public string Password { get; set; }

        public bool Status { get; set; }

        [StringLength(11)]
        public string Mobile { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Ip { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }

        public virtual ICollection<RequestForReating> RequestForReatings { get; set; }

    }
}
