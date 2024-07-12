using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFormReportChecks
{
    public interface IGetDataFormReportChecksService
    {
        Task<ResultDto<IEnumerable<DataFormReportCheckDto>>> Execute(RequestDataFormReportCheckDto request);
    }
}
