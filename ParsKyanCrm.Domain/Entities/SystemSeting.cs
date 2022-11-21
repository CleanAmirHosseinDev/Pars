using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class SystemSeting
    {
        public int SystemSetingId { get; set; }
        public string Label { get; set; }
        public int? LabelCode { get; set; }
        public int? ParentCode { get; set; }
        public byte IsActive { get; set; }
        public int? BaseAmount { get; set; }
        public string TitleBaseAmount { get; set; }
        public double? FromAmount { get; set; }
        public double? ToAmount { get; set; }
        public int? ChangeBy { get; set; }
        public DateTime? ChangeDate { get; set; }
        public string Value { get; set; }
        
    }
}
