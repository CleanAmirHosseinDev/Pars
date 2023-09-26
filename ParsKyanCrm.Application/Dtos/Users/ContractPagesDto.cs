
using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{

    public class RequestContractPagesDto : PageingParamerDto
    {
        public int? ContractId { get; set; }
        public int? ContractPageId { get; set; }
    }

    public class ContractPagesDto : BaseEntityDto
    {
        public int ContractPageId { get; set; }
        public int ContractId { get; set; }
        public string ContractText { get; set; }
        public int PageNumber { get; set; }

    }
}
