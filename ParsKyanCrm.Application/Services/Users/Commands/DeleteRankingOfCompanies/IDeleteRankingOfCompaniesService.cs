using ParsKyanCrm.Common.Dto;

namespace ParsKyanCrm.Application.Services.Users.Commands.DeleteRankingOfCompanies
{
    public interface IDeleteRankingOfCompaniesService
    {
        ResultDto Execute(int id);
    }
}
