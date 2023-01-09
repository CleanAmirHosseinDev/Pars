using ParsKyanCrm.Common.Dto;

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.DeleteRankingOfCompanies
{
    public interface IDeleteRankingOfCompaniesService
    {
        ResultDto Execute(int id);
    }
}
