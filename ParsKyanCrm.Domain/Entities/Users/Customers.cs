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

        [ForeignKey("KindOfCompany")]
        public int? KindOfCompanyID { get; set; }

        public string AddressCompany { get; set; }

        [StringLength(50)]
        public string NationalCode { get; set; }

        [StringLength(50)]
        public string PostalCode { get; set; }

        [StringLength(50)]
        public string EconomicCode { get; set; }

        [StringLength(11)]
        public string Tel { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string AgentName { get; set; }

        [StringLength(11)]
        public string AgentMobile { get; set; }

        [StringLength(50)]
        public string CeoName { get; set; }

        [StringLength(11)]
        public string CeoMobile { get; set; }

        public int? AmountOfPersonal { get; set; }

        public decimal? AmountOsLastSaels { get; set; }

        public DateTime SaveDate { get; set; }

        [StringLength(50)]
        public string Ip { get; set; }

        public virtual City City { get; set; }

        public virtual SystemSeting KindOfCompany { get; set; }

        public virtual ICollection<RequestForReating> RequestForReatings { get; set; }


    }
}
