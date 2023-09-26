using ParsKyanCrm.Common.Dto;

namespace ParsKyanCrm.Application.Services.Users.Commands.DeleteLicensesAndHonors
{
    public interface IDeleteLicensesAndHonorsService
    {
        ResultDto Execute(int id);
    }
}
