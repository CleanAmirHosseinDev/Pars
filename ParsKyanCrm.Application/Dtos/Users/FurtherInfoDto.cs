
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

    public class RequestFurtherInfoDto : PageingParamerDto
    {
        public int? RequestId { get; set; }
    }

    public class FurtherInfoDto : BaseEntityDto
    {
        public int FurtherInfoId { get; set; }
        public int? RequestId { get; set; }

        #region LastAuditingTaxList
        public string LastAuditingTaxList { get; set; }
        public string LastAuditingTaxListFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(LastAuditingTaxList, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_LastAuditingTaxList { get; set; }
        #endregion

        #region LastChangeOfficialNewspaper
        public string LastChangeOfficialNewspaper { get; set; }
        public string LastChangeOfficialNewspaperFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(LastChangeOfficialNewspaper, VaribleForName.CustomersFolder, false);
            }
        }

        public IFormFile Result_Final_LastChangeOfficialNewspaper { get; set; }
        #endregion

        #region StatuteDoc
        public string StatuteDoc { get; set; }
        public string StatuteDocFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(StatuteDoc, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_StatuteDoc { get; set; }
        #endregion

        #region OfficialNewspaper
        public string OfficialNewspaper { get; set; }
        public string OfficialNewspaperFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(OfficialNewspaper, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_OfficialNewspaper { get; set; }
        #endregion

        #region StatementTaxList
        public string StatementTaxList { get; set; }

        public string StatementTaxListFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(StatementTaxList, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_StatementTaxList { get; set; }
        #endregion






    }

}
