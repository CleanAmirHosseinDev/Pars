using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class ManagerOfParsKyan
    {
        public int ManagersId { get; set; }
        public string NameOfManager { get; set; }
        public int? PositionId { get; set; }
        public int? TitelId { get; set; }
        public string EmailAddress { get; set; }
        public string TwitterId { get; set; }
        public string Picture { get; set; }
        public string ResumeSummary { get; set; }
        public string ResumeFile { get; set; }
        public int? Userid { get; set; }
        public DateTime? SaveAndEditDate { get; set; }

        public virtual SystemSeting Position { get; set; }
        public virtual SystemSeting Titel { get; set; }
        public virtual Users User { get; set; }
    }
}
