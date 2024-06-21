using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class DataFormQuestionsOption
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int DataFormQuestionsId { get; set; }
        public double? Ratio { get; set; }
        public byte IsActive { get; set; }
    }
}
