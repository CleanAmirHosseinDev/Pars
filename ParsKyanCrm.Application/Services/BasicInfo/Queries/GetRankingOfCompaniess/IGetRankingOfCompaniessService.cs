using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetRankingOfCompaniess
{
    public interface IGetRankingOfCompaniessService
    {
        Task<ResultDto<IEnumerable<RankingOfCompaniesDto>>> Execute(RequestRankingOfCompaniesDto request);
    }
}
