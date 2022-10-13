using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Domain.Entities.BasicInfo
{
    [Table("State")]
    public class State
    {

        [Key]
        public int StateID { get; set; }

        [StringLength(100)]
        public string StateName { get; set; }

        public virtual ICollection<City> Cities { get; set; }

    }
}
