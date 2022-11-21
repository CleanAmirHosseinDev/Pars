using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class RankingCalculationResults
    {
        public int CalcId { get; set; }
        public int SubDomainid { get; set; }
        public int IndexId { get; set; }
        public int Customerid { get; set; }
        public int RequestId { get; set; }
        public double Score { get; set; }
        public double Weight { get; set; }
        public int IsActive { get; set; }
        public DateTime ChangeDate { get; set; }
        public int ChangeByUser { get; set; }
        public string Comment { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual SystemSeting11 Index { get; set; }
        public virtual RequestForRating Request { get; set; }
        public virtual SystemSeting11 SubDomain { get; set; }
    }
}
