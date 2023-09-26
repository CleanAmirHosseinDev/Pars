
using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{

    public class RequestServiceFeeDto : PageingParamerDto
    {
        public int? ServiceFeeId { get; set; }
    }

    public class ServiceFeeDto : BaseEntityDto
    {

        public int ServiceFeeId { get; set; }
        public int? KindOfService { get; set; }
        public int? FromCompanyRange { get; set; }
        public int? ToCompanyRange { get; set; }
        public decimal? FixedCost { get; set; }
        public decimal? VariableCost { get; set; }        

        public int? ChangeBy { get; set; }
        public DateTime? ChangeDate { get; set; }

        public string StartDate { get; set; }

        public decimal? Fee1 { get; set; }

        public decimal? Fee2 { get; set; }

        public SystemSetingDto KindOfServiceNavigation { get; set; }

    }
}
