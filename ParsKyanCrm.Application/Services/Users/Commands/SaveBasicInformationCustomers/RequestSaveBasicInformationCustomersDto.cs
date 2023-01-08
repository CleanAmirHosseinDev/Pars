using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
using Microsoft.AspNetCore.Http;
using FluentValidation;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveBasicInformationCustomers
{
    public class RequestSaveBasicInformationCustomersDto
    {

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
        /// کد اقتصادی( به شماره ثبت تغییر کرد
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

    }

    public class ValidatorRequestSaveBasicInformationCustomersDto : AbstractValidator<RequestSaveBasicInformationCustomersDto>
    {

        public ValidatorRequestSaveBasicInformationCustomersDto()
        {

            RuleFor(p => p.CompanyName).NotEmpty().WithMessage("نام شرکت را وارد کنید");

            RuleFor(p => p.CeoName).NotEmpty().WithMessage("نام مدیر عامل را وارد کنید");

            RuleFor(p => p.CeoMobile).NotEmpty().WithMessage("موبایل مدیر عامل را وارد کنید");

            RuleFor(p => p.CeoNationalCode).NotEmpty().WithMessage("کد ملی مدیر عامل را وارد کنید");

            RuleFor(p => p.EconomicCode).NotEmpty().WithMessage("شماره ثبت را وارد کنید");

            RuleFor(p => p.NationalCode).NotEmpty().WithMessage("شناسه ملی شرکت را وارد کنید");

            RuleFor(p => p.AgentName).NotEmpty().WithMessage("نام نماینده شرکت را وارد کنید");

            RuleFor(p => p.AgentMobile).NotEmpty().WithMessage("شماره نماینده شرکت را وارد کنید");

            RuleFor(p => p.NamesAuthorizedSignatories).NotEmpty().WithMessage("اسامی امضاکنندگان مجاز را وارد کنید");

            RuleFor(p => p.CountOfPersonal).NotEmpty().WithMessage("تعداد کارکنان شرکت را وارد کنید");

            RuleFor(p => p.HowGetKnowCompanyId).NotEmpty().WithMessage("نحوه آشنایی با شرکت را انتخاب کنید");

            RuleFor(p => p.KindOfCompanyId).NotEmpty().WithMessage("نوع شرکت را انتخاب کنید");

            RuleFor(p => p.Email).NotEmpty().WithMessage("ایمیل را وارد کنید").EmailAddress().WithMessage("ایمیل معتبر وارد کنید");

            RuleFor(p => p.Tel).NotEmpty().WithMessage("شماره تماس را وارد کنید");

            RuleFor(p => p.PostalCode).NotEmpty().WithMessage("کد پستی را وارد کنید");

            RuleFor(p => p.AddressCompany).NotEmpty().WithMessage("آدرس را وارد کنید");

            RuleFor(p => p.AmountOsLastSales).NotEmpty().WithMessage("درآمد عملیاتی بر اساس صورت های مالی حسابرسی شده را وارد کنید");

        }

    }
}
