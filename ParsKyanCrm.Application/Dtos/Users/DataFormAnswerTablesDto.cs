using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{
    public class RequestDataFormAnswerTablesDto : PageingParamerDto
    {

        public int? FormId { get; set; }
        public int? CustomerId { get; set; }

        public int? AnswerTableId { get; set; }

    }
    public class DataFormAnswerTablesDto : BaseEntityDto
    {

        public int AnswerTableId { get; set; }
        public int? FormId { get; set; }
        public int? CustomerId { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string Answer5 { get; set; }
        public string Answer6 { get; set; }
        public string Answer7 { get; set; }
        public string Answer8 { get; set; }
        public string Answer9 { get; set; }
        public string Answer10 { get; set; }

    }
}
