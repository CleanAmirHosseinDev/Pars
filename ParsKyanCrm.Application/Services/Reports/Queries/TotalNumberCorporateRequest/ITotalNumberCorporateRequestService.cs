using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Reports.Queries.TotalNumberCorporateRequest
{
    public interface ITotalNumberCorporateRequestService
    {
        Task<ResultDto<IEnumerable<ResultTotalNumberCorporateRequestDto>>> Execute(RequestTotalNumberCorporateRequestDto request);

        Task<byte[]> Execute1(RequestTotalNumberCorporateRequestDto request);

    }
}
