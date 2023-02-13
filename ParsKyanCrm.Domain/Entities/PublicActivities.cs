using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class PublicActivities
    {
        public int PublicActivitiesID { get; set; }
        public int? RequestId { get; set; }

        public int? CustomerID { get; set; }
        public bool IsPublicActivityFile { get; set; }

        public string Investment { get; set; }

        public string EmploymentDisabled { get; set; }

    }
}
