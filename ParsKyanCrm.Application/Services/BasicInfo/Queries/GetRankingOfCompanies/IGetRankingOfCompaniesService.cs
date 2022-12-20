using ParsKyanCrm.Application.Dtos.BasicInfo;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetRankingOfCompanies
{
    public interface IGetRankingOfCompaniesService
    {
        Task<RankingOfCompaniesDto> Execute(RequestRankingOfCompaniesDto request);
    }
}
