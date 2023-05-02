using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class ContractPages
    {
        [Key]
        public int ContractPageId { get; set; }
        public int ContractId { get; set; }
        public string ContractText { get; set; }
        public int PageNumber { get; set; }
    }
}

