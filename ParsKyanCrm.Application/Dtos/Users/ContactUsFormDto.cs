using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{

    public class ContactUsFormDto
    {

        public string name { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
        public string phone { get; set; }
        public string message { get; set; }
        public string CaptchaCodes { get; set; }

    }
}
