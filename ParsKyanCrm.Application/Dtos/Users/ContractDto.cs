using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{

    public class RequestContractDto : PageingParamerDto
    {

        public int? ContractId { get; set; }

    }

    public class ContractDto
    {

        public int ContractId { get; set; }
        public string ContractText { get; set; }

        public int? KinfOfRequest { get; set; }

        public SystemSetingDto KinfOfRequestNavigation { get; set; }

    }
}
