using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class ServiceFee
    {
        public int ServiceFeeId { get; set; }
        public int? KindOfService { get; set; }
        public int? FromCompanyRange { get; set; }
        public int? ToCompanyRange { get; set; }
        public decimal? FixedCost { get; set; }
        public decimal? VariableCost { get; set; }
        public int? ChangeBy { get; set; }
        public DateTime? ChangeDate { get; set; }
        public int IsActive { get; set; }

        public virtual SystemSeting11 KindOfServiceNavigation { get; set; }
    }
}
