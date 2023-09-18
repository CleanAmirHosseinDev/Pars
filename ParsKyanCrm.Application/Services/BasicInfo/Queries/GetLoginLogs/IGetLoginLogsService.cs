using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetLoginLogs
{
    public interface IGetLoginLogsService
    {
        Task<ResultDto<IEnumerable<LoginLogDto>>> Execute(RequestLoginLogDto request);
    }
}
