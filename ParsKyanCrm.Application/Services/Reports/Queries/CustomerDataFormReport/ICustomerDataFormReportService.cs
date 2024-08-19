using ParsKyanCrm.Application.Services.Reports.Queries.TotalNumberCorporateRequest;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Reports.Queries.CustomerDataFormReport
{
    public interface ICustomerDataFormReportService
    {
        Task<ResultDto<ResultCustomerDataFormReportDto>> Execute(RequestCustomerDataFormReportDto request);
        Task<byte[]> ToExcel(RequestCustomerDataFormReportDto request);
    }
}
