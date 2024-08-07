using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveDataFormReports
{
    public interface ISaveDataFormReportsService
    {
        Task<ResultDto<DataFormReportDto>> Execute(DataFormReportDto request);
    }
}
