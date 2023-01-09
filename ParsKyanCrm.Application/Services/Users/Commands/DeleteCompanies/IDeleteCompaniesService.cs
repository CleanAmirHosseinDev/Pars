using ParsKyanCrm.Common.Dto;

namespace ParsKyanCrm.Application.Services.Users.Commands.DeleteCompanies
{
    public interface IDeleteCompaniesService
    {
        ResultDto Execute(int id);
    }
}
