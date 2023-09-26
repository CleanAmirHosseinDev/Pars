
using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetRankingOfCompanies
{
    public interface IGetRankingOfCompaniesService
    {
        Task<RankingOfCompaniesDto> Execute(RequestRankingOfCompaniesDto request);
    }
}
