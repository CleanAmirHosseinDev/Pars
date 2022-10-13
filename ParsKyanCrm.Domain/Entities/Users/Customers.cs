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
    [Table("Customers")]
    public class Customers : BaseEntity
    {

        [Key]
        public int CustomerID { get; set; }

        [ForeignKey("City")]
        public int? CityID { get; set; }

        [StringLength(50)]
        public string CompanyName { get; set; }

        [ForeignKey("SystemSeting")]
        public int? KindOfCompanyID { get; set; }

        public virtual City City { get; set; }

        public virtual SystemSeting SystemSeting { get; set; }

    }
}
