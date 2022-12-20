using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveRankingOfCompanies
{
    public interface ISaveRankingOfCompaniesService
    {
        Task<ResultDto<RankingOfCompaniesDto>> Execute(RankingOfCompaniesDto request);
    }
}
