using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetServiceFees
{
    public interface IGetServiceFeesService
    {
        Task<ResultDto<IEnumerable<ServiceFeeDto>>> Execute(RequestServiceFeeDto request);
    }
}
