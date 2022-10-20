using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class State
    {
        public State()
        {
            City = new HashSet<City>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; }

        public virtual ICollection<City> City { get; set; }
    }
}
