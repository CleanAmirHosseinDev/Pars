using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class CompanyGroup
    {
        public int CompanyGroupId { get; set; }
        public string GroupName { get; set; }
        public byte? IsActive { get; set; }
    }
}
