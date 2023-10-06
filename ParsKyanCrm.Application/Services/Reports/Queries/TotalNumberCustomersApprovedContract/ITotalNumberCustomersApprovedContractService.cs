using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Reports.Queries.TotalNumberCustomersApprovedContract
{
    public interface ITotalNumberCustomersApprovedContractService
    {
        Task<ResultDto<IEnumerable<ResultTotalNumberCustomersApprovedContractDto>>> Execute(RequestTotalNumberCustomersApprovedContractDto request);

        Task<byte[]> Execute1(RequestTotalNumberCustomersApprovedContractDto request);

    }
}
