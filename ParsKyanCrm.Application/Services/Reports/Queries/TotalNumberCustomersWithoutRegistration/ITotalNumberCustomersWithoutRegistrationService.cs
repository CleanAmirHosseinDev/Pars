using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Reports.Queries.TotalNumberCustomersWithoutRegistration
{
    public interface ITotalNumberCustomersWithoutRegistrationService
    {
        Task<ResultDto<IEnumerable<ResultTotalNumberCustomersWithoutRegistrationDto>>> Execute(RequestTotalNumberCustomersWithoutRegistrationDto request);

        Task<byte[]> Execute1(RequestTotalNumberCustomersWithoutRegistrationDto request);
    }
}
