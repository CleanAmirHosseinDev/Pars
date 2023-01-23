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

    public class RequestCustomersDto : PageingParamerDto
    {
        public int? CustomerId { get; set; }
    }

    public class CustomersDto : BaseEntityDto
    {

        public bool IsProfileComplete { get; set; }

        public int CustomerId { get; set; }
        public int? CityId { get; set; }
        /// <summary>
        /// نام شرکت
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// اسامی امضاکنندگان مجاز
        /// </summary>
        public string NamesAuthorizedSignatories { get; set; }
        /// <summary>
        /// نوع شرکت
        /// </summary>
        public int? KindOfCompanyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? TypeServiceRequestedId { get; set; }
        /// <summary>
        /// نحوه آشنایی با شرکت
        /// </summary>
        public int? HowGetKnowCompanyId { get; set; }
        /// <summary>
        /// آدرس شرکت
        /// </summary>
        public string AddressCompany { get; set; }
        /// <summary>
        /// شناسه ملی  شرکت
        /// </summary>
        public string NationalCode { get; set; }
        /// <summary>
        /// کد پستی
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// کد اقتصادی   (تغییر کرد به شماره ثبت شرکت)
        /// </summary>
        public string EconomicCode { get; set; }
        /// <summary>
        /// تلفن شرکت
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// نام رابط و نماینده شرکت
        /// </summary>
        public string AgentName { get; set; }
        /// <summary>
        /// موبایل نماینده
        /// </summary>
        public string AgentMobile { get; set; }
        /// <summary>
        /// نام و نام خانوادگی مدیر عامل
        /// </summary>
        public string CeoName { get; set; }
        /// <summary>
        /// موبایل مدیر عامل
        /// </summary>
        public string CeoMobile { get; set; }
        /// <summary>
        /// کد ملی مدیرعامل
        /// </summary>
        public string CeoNationalCode { get; set; }
        /// <summary>
        /// تعداد پرسنل شرکت
        /// </summary>
        public int? CountOfPersonal { get; set; }
        /// <summary>
        /// مبلغ کل فروش اظهار شده
        /// </summary>
        public decimal? AmountOsLastSales { get; set; }

        public DateTime SaveDate { get; set; }
        public string SaveDateStr
        {
            get
            {
                return DateTimeOperation.ToPersianDate(SaveDate);
            }
        }

        public string Ip { get; set; }
        /// <summary>
        /// کد احراز هویت
        /// </summary>
        public string AuthenticateCode { get; set; }

        /// <summary>
        /// آخرین تغییرات روزنامه رسمی
        /// </summary>
        public string LastChangeOfficialNewspaper { get; set; }
        public string LastChangeOfficialNewspaperFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(LastChangeOfficialNewspaper, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_LastChangeOfficialNewspaper { get; set; }

        /// <summary>
        /// آخرین اظهار نامه
        /// </summary>
        public string LastAuditingTaxList { get; set; }


        /// <summary>
        /// آخرین لیست بیمه
        /// </summary>
        public string LastInsuranceList { get; set; }
        public string LastInsuranceListFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(LastInsuranceList, VaribleForName.CustomersFolder, false);
            }
        }
        public IFormFile Result_Final_LastInsuranceList { get; set; }

        public IFormFile Result_Final_AuditedFinancialStatements { get; set; }
        /// <summary>
        /// آخرین صورتحسابهای مالی حسابرسی شده
        /// </summary>
        public string AuditedFinancialStatements { get; set; }
        public string AuditedFinancialStatementsFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(AuditedFinancialStatements, VaribleForName.CustomersFolder, false);
            }
        }

        public bool CanSeeFurtherInfo { get; set; }
        public CityDto City { get; set; }
        public SystemSetingDto HowGetKnowCompany { get; set; }
        public SystemSetingDto KindOfCompany { get; set; }
        public SystemSetingDto TypeServiceRequested { get; set; }

        public int? CustomerPersonalityType { get; set; }
        public int? TypeGroupCompanies { get; set; }

    }    

}
