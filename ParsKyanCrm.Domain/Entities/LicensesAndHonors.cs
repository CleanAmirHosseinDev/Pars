using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class LicensesAndHonors
    {
        public int LicensesAndHonorsId { get; set; }
        public string Title { get; set; }
        public string Picture { get; set; }
        public int? UserId { get; set; }
        public DateTime? SaveOrEditDate { get; set; }

        public byte IsActive { get; set; }

        public virtual Users User { get; set; }
    }
}
