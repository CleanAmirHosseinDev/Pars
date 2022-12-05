using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetManagerOfParsKyans
{
    public interface IGetManagerOfParsKyansService
    {
        Task<ResultDto<IEnumerable<ManagerOfParsKyanDto>>> Execute(RequestManagerOfParsKyanDto request);
    }
}
