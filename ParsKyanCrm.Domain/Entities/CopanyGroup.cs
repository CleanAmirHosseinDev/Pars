using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class CopanyGroup
    {
        public CopanyGroup()
        {
            Companies = new HashSet<Companies>();
        }

        public int CompanyGroupId { get; set; }
        public string GroupName { get; set; }
        public byte? IsActive { get; set; }

        public virtual ICollection<Companies> Companies { get; set; }
    }
}
