using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFromReports
{
    public interface IGetDataFormReportsService
    {
        Task<ResultDto<IEnumerable<DataFormReportDto>>> Execute(RequestDataFormReportDto request);
    }
}
