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
    [Table("City")]
    public class City
    {

        [Key]
        public int CityID { get; set; }

        [ForeignKey("State")]
        public int? StateID { get; set; }

        [StringLength(50)]
        public string CityName { get; set; }        

        public virtual State State { get; set; }

        public virtual ICollection<Customers> Customers { get; set; }

    }
}
