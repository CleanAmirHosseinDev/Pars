using FluentValidation;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{

    public class RequestCustomers_RegisterLandingDto : PageingParamerDto
    {

    }

    public class Customers_RegisterLandingDto : BaseEntityDto
    {
        public string CaptchaCodes { get; set; }
        public int CustomerId { get; set; }
        public string CompanyName { get; set; }
        public int? KindOfCompanyId { get; set; }
        public int? TypeServiceRequestedId { get; set; }
        public string AddressCompany { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string AgentName { get; set; }
        public string AgentMobile { get; set; }
        public string CeoName { get; set; }
        public string CeoMobile { get; set; }
        public int? CountOfPersonal { get; set; }
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
        public int? TypeGroupCompanies { get; set; }
        public string EmailRepresentative { get; set; }
        public string Description { get; set; }
    }

    public class ValidatorCustomers_RegisterLandingDto : AbstractValidator<Customers_RegisterLandingDto>
    {

        public ValidatorCustomers_RegisterLandingDto()
        {

            //RuleFor(p => p.CompanyName).NotEmpty().WithMessage("نام شرکت را وارد کنید").Length(5, 50).WithMessage("نام شرکت باید حداقل 5 حرف و حداکثر 50 حرف باشد");

            RuleFor(p => p.CeoMobile).Must(Utility.CheckMobile).WithMessage("موبایل مدیر عامل را به درستی وارد کنید");

            //RuleFor(p => p.CeoName).NotEmpty().WithMessage("نام مدیر عامل را وارد کنید").Length(5, 50).WithMessage("نام مدیر عامل باید حداقل 5 حرف و حداکثر 50 حرف باشد");                                         

            //RuleFor(p => p.AgentName).NotEmpty().WithMessage("نام نماینده شرکت/ مشاور شرکت را وارد کنید").Length(5, 50).WithMessage("نام نماینده شرکت/ مشاور شرکت باید حداقل 5 حرف و حداکثر 50 حرف باشد");

            RuleFor(p => p.AgentMobile).Must(Utility.CheckMobile).WithMessage("شماره نماینده شرکت/مشاور را به درستی وارد کنید");

            //RuleFor(p => p.AddressCompany).NotEmpty().WithMessage("آدرس را وارد کنید");

            //RuleFor(p => p.CountOfPersonal).NotEmpty().WithMessage("تعداد کارکنان شرکت را وارد کنید");                

            //RuleFor(p => p.KindOfCompanyId).NotEmpty().WithMessage("نوع شرکت را انتخاب کنید");

            //RuleFor(p => p.TypeGroupCompanies).NotEmpty().WithMessage("نوع گروه شرکتها را انتخاب کنید");

            RuleFor(p => p.Email).EmailAddress().WithMessage("ایمیل شرکت معتبر وارد کنید").Length(5, 50).WithMessage("ایمیل شرکت باید حدقل 5 حرف و حداکثر 50 حرف باشد");

            RuleFor(p => p.EmailRepresentative).EmailAddress().WithMessage("ایمیل نماینده شرکت/ مشاور معتبر وارد کنید").Length(5, 50).WithMessage("ایمیل نماینده شرکت/ مشاور باید حدقل 5 حرف و حداکثر 50 حرف باشد");

            RuleFor(p => p.Tel).Must(Utility.CheckTel).WithMessage("شماره تلفن ثابت را به درستی وارد کنید");

            //RuleFor(p => p.AmountOsLastSales).NotEmpty().WithMessage("درآمد عملیاتی بر اساس صورت های مالی حسابرسی شده را وارد کنید");                                

        }

    }
}
