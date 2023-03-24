using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;

namespace ParsKyanCrm.Application.Dtos.Users
{

    public class RequestValueChainDto : PageingParamerDto
    {
        public int? RequestId { get; set; }
    }

    public  class ValueChainDto
    {
        public int ValueChainId { get; set; }
        public int? RequestId { get; set; }
        public string vc1 { get; set; }

        
        public string vc2 { get; set; }
        public string vc3 { get; set; }
        public string vc4 { get; set; }
        public string vc5 { get; set; }
        public string vc6 { get; set; }
        public string vc7 { get; set; }
        public string vc8 { get; set; }
        public string vc9 { get; set; }
        public string vc10 { get; set; }
        public string vc11 { get; set; }
        public string vc12 { get; set; }
        public string vc13 { get; set; }
        public string vc14 { get; set; }
        public string vc15 { get; set; }
        public string vc16 { get; set; }
        public string vc17 { get; set; }
        public string vc18 { get; set; }
        public string vc19 { get; set; }
        public string vc20 { get; set; }
        public string vc21 { get; set; }
        public string vc22 { get; set; }
        public string vc23 { get; set; }
        public string vc24 { get; set; }
        public string vc25 { get; set; }

        public IFormFile Result_Final_vc1 { get; set; }
        public IFormFile Result_Final_vc2 { get; set; }
        public IFormFile Result_Final_vc3 { get; set; }
        public IFormFile Result_Final_vc4 { get; set; }
        public IFormFile Result_Final_vc5 { get; set; }
        public IFormFile Result_Final_vc6 { get; set; }
        public IFormFile Result_Final_vc7 { get; set; }
        public IFormFile Result_Final_vc8 { get; set; }
        public IFormFile Result_Final_vc9 { get; set; }
        public IFormFile Result_Final_vc10 { get; set; }
        public IFormFile Result_Final_vc11 { get; set; }
        public IFormFile Result_Final_vc12 { get; set; }
        public IFormFile Result_Final_vc13 { get; set; }
        public IFormFile Result_Final_vc14 { get; set; }
        public IFormFile Result_Final_vc15 { get; set; }
        public IFormFile Result_Final_vc16 { get; set; }
        public IFormFile Result_Final_vc17 { get; set; }
        public IFormFile Result_Final_vc18 { get; set; }
        public IFormFile Result_Final_vc19 { get; set; }

        public IFormFile Result_Final_vc20 { get; set; }
        public IFormFile Result_Final_vc21 { get; set; }
        public IFormFile Result_Final_vc22 { get; set; }
        public IFormFile Result_Final_vc23 { get; set; }
        public IFormFile Result_Final_vc24 { get; set; }
        public IFormFile Result_Final_vc25 { get; set; }
        public string Vc1Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc1, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc2Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc2, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc3Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc3, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc4Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc4, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc5Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc5, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc6Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc6, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc7Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc7, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc8Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc8, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc9Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc9, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc10Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc10, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc11Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc11, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc12Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc12, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc13Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc13, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc14Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc14, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc15Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc15, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc16Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc16, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc17Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc17, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc18Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc18, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc19Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc19, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc20Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc20, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc21Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc21, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc22Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc22, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc23Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc23, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc24Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc24, VaribleForName.CustomersFolder, false);
            }
        }
        public string Vc25Full
        {
            get
            {
                return ServiceFileUploader.GetFullPath(vc25, VaribleForName.CustomersFolder, false);
            }
        }



    }

}
