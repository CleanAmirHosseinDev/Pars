using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{

    public class RequestDataFormQuestionsOptionDto : PageingParamerDto
    {
        public int? DataFormQuestionsId { get; set; }
        public int Id { get; set; }
        public byte? IsActive { get; set; }
    }

    public class DataFormQuestionsOptionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int DataFormQuestionsId { get; set; }
        public double? Ratio { get; set; }
        public byte? IsActive { get; set; }
    }
}
