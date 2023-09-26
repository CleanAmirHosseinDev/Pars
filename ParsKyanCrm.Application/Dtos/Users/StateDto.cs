using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{

    public class RequestStateDto : PageingParamerDto
    {

    }

    public class StateDto
    {

        public int StateId { get; set; }
        public string StateName { get; set; }

    }
}
