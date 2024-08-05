using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Domain.Entities
{
    public partial class DataFormDocument
    {
        public int DataFormDocumentId { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string HelpText { get; set; }
        public byte? IsActive { get; set; }
        public bool? IsRequierd { get; set; }
    }
}
