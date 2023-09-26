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
    public class RequestDataFormAnswerTablesDto : PageingParamerDto
    {

        public int? FormId { get; set; }
        public int? RequestId { get; set; }

        public int? AnswerTableId { get; set; }

    }
    public class DataFormAnswerTablesDto : BaseEntityDto
    {

        public int AnswerTableId { get; set; }
        public int? FormId { get; set; }
        public int? RequestId { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer2Val { get; set; }
        public string Answer3 { get; set; }
        public string Answer3Val { get; set; }
        public string Answer4 { get; set; }
        public string Answer4Val { get; set; }
        public string Answer5 { get; set; }
        public string Answer5Val { get; set; }
        public string Answer6 { get; set; }
        public string Answer6Val { get; set; }
        public string Answer7 { get; set; }
        public string Answer7Val { get; set; }
        public string Answer8 { get; set; }
        public string Answer8Val { get; set; }
        public string Answer9 { get; set; }
        public string Answer9Val { get; set; }
        public string Answer10 { get; set; }
        public string Answer10Val { get; set; }
        public string FileName1 { get; set; }
        public string FileName2 { get; set; }

        public IFormFile Result_Final_FileName1 { get; set; }
        
        public string FileName1Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(FileName1, VaribleForName.CustomerFurtherInfoFolder, false);
            }
        }

        public IFormFile Result_Final_FileName2 { get; set; }

        public string FileName2Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(FileName2, VaribleForName.CustomerFurtherInfoFolder, false);
            }
        }

       

        public string FileName3 { get; set; }
        public IFormFile Result_Final_FileName3 { get; set; }
        public string FileName3Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(FileName3, VaribleForName.CustomerFurtherInfoFolder, false);
            }
        }
        public string FileName4 { get; set; }
        public IFormFile Result_Final_FileName4 { get; set; }
        public string FileName4Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(FileName4, VaribleForName.CustomerFurtherInfoFolder, false);
            }
        }

    }
}
