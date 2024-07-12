using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFormReportCheck
{
    public interface IGetDataFormReportCheckService
    {
        Task<DataFormReportCheckDto> Execute(RequestDataFormReportCheckDto request);
    }
}
