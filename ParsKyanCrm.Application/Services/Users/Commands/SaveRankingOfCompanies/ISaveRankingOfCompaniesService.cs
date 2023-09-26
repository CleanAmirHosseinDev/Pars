
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveRankingOfCompanies
{
    public interface ISaveRankingOfCompaniesService
    {
        Task<ResultDto<RankingOfCompaniesDto>> Execute(RankingOfCompaniesDto request);
    }
}
