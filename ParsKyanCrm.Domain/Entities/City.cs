using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class City
    {
        public City()
        {
            Customers = new HashSet<Customers>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int? StateId { get; set; }

        public virtual State State { get; set; }
        public virtual ICollection<Customers> Customers { get; set; }
    }
}
