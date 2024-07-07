using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFromReport
{
    public interface IGetDataFormReportService
    {
        Task<DataFormReportDto> Execute(int? id = null);
        Task<DataFormReportDto> ExecuteWhithParam(DataFormReportDto request);
    }
}
