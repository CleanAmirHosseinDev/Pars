using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class RankingOfCompanies
    {
        public int RankingId { get; set; }
        public int? ComanyId { get; set; }
        public DateTime? PublishDate { get; set; }
        public string LongTermRating { get; set; }
        public string ShortTermRating { get; set; }
        public string Vistion { get; set; }
        public string PressRelease { get; set; }
        public string SummaryRanking { get; set; }
        public int? UserId { get; set; }
        public int? IsActive { get; set; }

        public virtual Companies Comany { get; set; }
        public virtual Users User { get; set; }
    }
}
