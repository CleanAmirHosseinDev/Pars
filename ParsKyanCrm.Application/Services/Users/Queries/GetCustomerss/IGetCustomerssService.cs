using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetCustomerss
{
    public interface IGetCustomerssService
    {
        Task<ResultDto<IEnumerable<CustomersDto>>> Execute(RequestCustomersDto request);
    }
}
