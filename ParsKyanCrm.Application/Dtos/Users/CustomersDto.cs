using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ParsKyanCrm.Application.Dtos.Users
{

    public class RequestCustomersDto : PageingParamerDto
    {
        public int? CustomerId { get; set; }
    }

    public class CustomersDto : BaseEntityDto
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
        /// چگونه شناسه شرکت را بشناسیم
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
        /// کد اقتصادی
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
        /// تعداد پرسنل شرکت
        /// </summary>
        public int? CountOfPersonal { get; set; }
        /// <summary>
        /// مبلغ کل فروش اظهار شده
        /// </summary>
        public decimal? AmountOsLastSaels { get; set; }
        public DateTime SaveDate { get; set; }
        public string Ip { get; set; }
        /// <summary>
        /// کد احراز هویت
        /// </summary>
        public string AuthenticateCode { get; set; }
        public CityDto City { get; set; }
        public SystemSetingDto HowGetKnowCompany { get; set; }
        public SystemSetingDto KindOfCompany { get; set; }
        public SystemSetingDto TypeServiceRequested { get; set; }

    }

    public class ValidatorCustomersDto : AbstractValidator<CustomersDto>
    {
        public ValidatorCustomersDto()
        {
            RuleFor(p => p.AgentName).NotEmpty().WithMessage("نام نماینده را وارد کنید");

        }

    }

}
