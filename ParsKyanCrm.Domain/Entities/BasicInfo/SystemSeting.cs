
using ParsKyanCrm.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Domain.Entities.BasicInfo
{

    [Table("SystemSeting")]
    public class SystemSeting : BaseEntity
    {


        [Key]
        public int SystemSetingID { get; set; }

        [StringLength(50)]
        public string Label { get; set; }

        public int? LabeCode { get; set; }

        [StringLength(100)]
        public string Value { get; set; }

        public int? BaseAmount { get; set; }

        public double? FromAmount { get; set; }

        public double? ToAmount { get; set; }

        public virtual ICollection<Customers> CustomerHowGetKnowCompanies { get; set; }
        public virtual ICollection<Customers> CustomerKindOfCompanies { get; set; }
        public virtual ICollection<Customers> CustomerTypeServiceRequesteds { get; set; }

    }
}
