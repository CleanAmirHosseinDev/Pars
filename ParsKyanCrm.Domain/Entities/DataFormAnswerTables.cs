using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class DataFormAnswerTables
    {

        public string FileName1 { get; set; }
        public string FileName2 { get; set; }

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

        public byte IsActive { get; set; }

    }
}
