using FluentValidation;

namespace ParsKyanCrm.Application.Services.Users.Commands.UpdatePassUsers
{
    public class RequestUpdatePassUsersDto
    {

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }

        public int UserID { get; set; }

    }

    public class ValidatorRequestUpdatePassUsersDto : AbstractValidator<RequestUpdatePassUsersDto>
    {

        public ValidatorRequestUpdatePassUsersDto()
        {

            RuleFor(p => p.OldPassword).NotEmpty().WithMessage("کلمه عبور جاری را وارد کنید");

            RuleFor(p => p.NewPassword).NotEmpty().WithMessage("کلمه عبور جدید را وارد کنید");

            RuleFor(p => p.ConfirmPassword).NotEmpty().WithMessage("تکرار کلمه عبور جدید را وارد کنید").Equal(p => p.NewPassword).WithMessage("تکرار کلمه عبور جدید با کلمه عبور جدید برابر نمی باشد");

        }

    }
}
