using Microsoft.AspNetCore.Http;
using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{
    public class RequestDataFormDocumentsDto : PageingParamerDto
    {
        public int DataFormDocumentId { get; set; }
        public int CategoryId { get; set; }
        public bool? IsRequierd { get; set; }
    }

    public class DataFormDocumentsDto
    {
        public int DataFormDocumentId { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string HelpText { get; set; }
        public byte? IsActive { get; set; }
        public bool? IsRequierd { get; set; }
    }
}
