using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetServiceFee
{
    public interface IGetServiceFeeService
    {
        Task<ServiceFeeDto> Execute(RequestServiceFeeDto request);
    }
}
