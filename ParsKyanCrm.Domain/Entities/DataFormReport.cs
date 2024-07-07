using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Domain.Entities
{
    public partial class DataFormReport
    {
        public int DataReportId { get; set; }
        public int RequestId { get; set; }
        public int DataFormAnswerId { get; set; }
        public float SystemScore { get; set; }
        public float AnalizeScore { get; set; }
        public string Description { get; set; }
        public byte IsActive { get; set; }
    }
}
