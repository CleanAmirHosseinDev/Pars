using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class LicensesAndHonors
    {
        public int LicensesAndHonorsId { get; set; }
        public string Titel { get; set; }
        public string Picture { get; set; }
        public int? Userid { get; set; }
        public DateTime? SaveOrdEditDate { get; set; }

        public virtual Users User { get; set; }
    }
}
