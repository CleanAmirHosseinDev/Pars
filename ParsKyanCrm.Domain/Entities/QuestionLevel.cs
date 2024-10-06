using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class QuestionLevel
    {
        public int QuestionLevelId { get; set; }
        public string LevelTitle { get; set; }
        public string LevelDescription { get; set; }
        public byte? IsActive { get; set; }
    }
}
