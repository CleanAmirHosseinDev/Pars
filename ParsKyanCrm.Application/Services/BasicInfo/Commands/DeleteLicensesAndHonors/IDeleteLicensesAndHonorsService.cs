using ParsKyanCrm.Common.Dto;

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.DeleteLicensesAndHonors
{
    public interface IDeleteLicensesAndHonorsService
    {
        ResultDto Execute(int id);
    }
}
