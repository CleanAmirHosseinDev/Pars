using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetSystemSetings
{
    public interface IGetSystemSetingsService
    {
        Task<ResultDto<IEnumerable<SystemSetingDto>>> Execute(RequestSystemSetingDto request);
    }
}
