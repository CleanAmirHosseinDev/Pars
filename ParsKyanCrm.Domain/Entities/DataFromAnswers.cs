using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Domain.Entities
{
    public partial class DataFromAnswers
    {
        public int AnswerId { get; set; }
        public int? CustomerId { get; set; }
        public int? FormId { get; set; }
        public int? DataFormQuestionId { get; set; }
        public string Answer { get; set; }
        public string FileName1 { get; set; }
    }
}
