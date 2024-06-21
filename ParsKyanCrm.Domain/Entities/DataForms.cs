using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class DataForms
    {
        public int FormId { get; set; }
        public string FormTitle { get; set; }
        public bool? IsTable { get; set; }
        public int? CategoryId { get; set; }
        public byte IsActive { get; set; }
    }
}
