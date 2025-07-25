﻿using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
using Microsoft.AspNetCore.Http;
using FluentValidation;
using ParsKyanCrm.Common;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveBasicInformationCustomers
{
    public class RequestSaveBasicInformationCustomersDto
    {

        public string EmailRepresentative { get; set; }

        public string NationalCodeRepresentative { get; set; }

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

        public string EconomicCodeReal { get; set; }
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

        public int? CustomerPersonalityType { get; set; }
        public int? TypeGroupCompanies { get; set; }

        public string ScanCustomerNationalCard { get; set; }
        public IFormFile Result_Final_ScanCustomerNationalCard { get; set; }
        public string ScanCustomerNationalCardFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(ScanCustomerNationalCard, VaribleForName.CustomersFolder, false);
            }
        }

        public string ScanManagerNationalCard { get; set; }
        public IFormFile Result_Final_ScanManagerNationalCard { get; set; }
        public string ScanManagerNationalCardFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(ScanManagerNationalCard, VaribleForName.CustomersFolder, false);
            }
        }

    }

    public class ValidatorRequestSaveBasicInformationCustomersDto : AbstractValidator<RequestSaveBasicInformationCustomersDto>
    {

        public ValidatorRequestSaveBasicInformationCustomersDto()
        {

            When(b => b.CustomerPersonalityType == 223, () =>
              {

                 
                  RuleFor(p => p.CompanyName).NotEmpty().WithMessage("نام و نام خانوادگی را وارد کنید").Length(5, 50).WithMessage("نام و نام خانوادگی باید حداقل 5 حرف و حداکثر 50 حرف باشد");
                  RuleFor(p => p.EconomicCode).Length(3, 50).WithMessage("شماره کارت بازرگانی باید حداقل 3 حرف و حداکثر 50 حرف باشد");
                  RuleFor(p => p.AgentMobile).NotEmpty().WithMessage("شماره موبایل را وارد کنید").Must(Utility.CheckMobile).WithMessage("شماره نماینده را به درستی وارد کنید");

                  RuleFor(p => p.AddressCompany).NotEmpty().WithMessage("آدرس را وارد کنید");

                  RuleFor(p => p.Result_Final_ScanCustomerNationalCard).NotNull().WithMessage("اسکن کارت ملی نماینده را بارگذاری کنید");

              });


            When(b => b.CustomerPersonalityType == null || b.CustomerPersonalityType != 223, () =>
              {

                  RuleFor(p => p.EconomicCodeReal).NotEmpty().WithMessage("کد اقتصادی را وارد کنید").Length(10, 12).WithMessage("کد اقتصادی باید حداقل 10 حرف و حداکثر 12 حرف باشد");

                  RuleFor(p => p.CompanyName).NotEmpty().WithMessage("نام شرکت را وارد کنید").Length(5, 50).WithMessage("نام شرکت باید حداقل 5 حرف و حداکثر 50 حرف باشد");

                  RuleFor(p => p.CeoMobile).NotEmpty().WithMessage("موبایل مدیر عامل را وارد کنید").Must(Utility.CheckMobile).WithMessage("موبایل مدیر عامل را به درستی وارد کنید");

                  RuleFor(p => p.CeoName).NotEmpty().WithMessage("نام مدیر عامل را وارد کنید").Length(5, 50).WithMessage("نام مدیر عامل باید حداقل 5 حرف و حداکثر 50 حرف باشد");

                  RuleFor(p => p.CeoNationalCode).NotEmpty().WithMessage("کد ملی مدیر عامل را وارد کنید").Length(10).WithMessage("کد ملی مدیر عامل باید 10 حرف باشد");

                  RuleFor(p => p.EconomicCode).NotEmpty().WithMessage("شماره ثبت را وارد کنید").Length(3, 50).WithMessage("شماره ثبت باید حداقل 3 حرف و حداکثر 50 حرف باشد");

                  RuleFor(p => p.NationalCode).NotEmpty().WithMessage("شناسه ملی شرکت را وارد کنید").Length(10, 11).WithMessage("شناسه ملی شرکت باید حداقل 10 حرف و حداکثر 11 حرف باشد");

                  RuleFor(p => p.AgentName).NotEmpty().WithMessage("نام نماینده شرکت/ مشاور شرکت را وارد کنید").Length(5, 50).WithMessage("نام نماینده شرکت/ مشاور شرکت باید حداقل 5 حرف و حداکثر 50 حرف باشد");

                  RuleFor(p => p.AgentMobile).NotEmpty().WithMessage("شماره نماینده شرکت/مشاور را وارد کنید").Must(Utility.CheckMobile).WithMessage("شماره نماینده شرکت/مشاور را به درستی وارد کنید");

                  RuleFor(p => p.AddressCompany).NotEmpty().WithMessage("آدرس را وارد کنید");

               //   RuleFor(p => p.CountOfPersonal).NotEmpty().WithMessage("تعداد کارکنان شرکت را وارد کنید");

                  RuleFor(p => p.HowGetKnowCompanyId).NotEmpty().WithMessage("نحوه آشنایی با شرکت را انتخاب کنید");

                  RuleFor(p => p.KindOfCompanyId).NotEmpty().WithMessage("نوع شرکت را انتخاب کنید");

                  RuleFor(p => p.TypeGroupCompanies).NotEmpty().WithMessage("نوع گروه شرکتها را انتخاب کنید");

                  RuleFor(p => p.Email).NotEmpty().WithMessage("ایمیل شرکت را وارد کنید").EmailAddress().WithMessage("ایمیل شرکت معتبر وارد کنید").Length(5, 50).WithMessage("ایمیل شرکت باید حدقل 5 حرف و حداکثر 50 حرف باشد");

                  RuleFor(p => p.EmailRepresentative).EmailAddress().WithMessage("ایمیل نماینده شرکت/ مشاور معتبر وارد کنید").Length(5, 50).WithMessage("ایمیل نماینده شرکت/ مشاور باید حدقل 5 حرف و حداکثر 50 حرف باشد");

                  RuleFor(p => p.Tel).NotEmpty().WithMessage("شماره تلفن ثابت را وارد کنید").Must(Utility.CheckTel).WithMessage("شماره تلفن ثابت را به درستی وارد کنید");

                  RuleFor(p => p.PostalCode).NotEmpty().WithMessage("کد پستی را وارد کنید").Length(10).WithMessage("کد پستی باید 10 حرف باشد");

                  //RuleFor(p => p.AmountOsLastSales).NotEmpty().WithMessage("درآمد عملیاتی بر اساس صورت های مالی حسابرسی شده را وارد کنید");

                  RuleFor(p => p.NamesAuthorizedSignatories).NotEmpty().WithMessage("اسامی امضاکنندگان مجاز را وارد کنید");

                  RuleFor(p => p.NationalCodeRepresentative).Length(10, 10).WithMessage("کد ملی نماینده شرکت/ مشاور باید حداقل 10 حرف و حداکثر 10 حرف باشد");

                  //When(v => (v.AmountOsLastSales != null && v.AmountOsLastSales != 0) && string.IsNullOrEmpty(v.LastAuditingTaxList), () =>
                  //   {
                  //       RuleFor(p => p.Result_Final_AuditedFinancialStatements).NotNull().WithMessage("آخرین صورت مالی حسابرسی شده یا اظهار نامه مالیاتی را بارگذاری کنید");
                  //   });

                  //When(c => (c.CountOfPersonal != null && c.CountOfPersonal != 0) && string.IsNullOrEmpty(c.LastInsuranceList), () =>
                  //  {

                  //      RuleFor(p => p.Result_Final_LastInsuranceList).NotNull().WithMessage("لیست آخرین بیمه را بارگذاری کنید");

                  //  });

              });

        }

    }
}
