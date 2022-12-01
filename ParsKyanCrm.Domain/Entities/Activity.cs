using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class Activity
    {
        public int ActivityId { get; set; }
        public int? ActivityTitel { get; set; }
        public string ActivityComment { get; set; }
        public string Picture1 { get; set; }
        public string Picture2 { get; set; }

        public virtual SystemSeting ActivityTitelNavigation { get; set; }
    }
}
