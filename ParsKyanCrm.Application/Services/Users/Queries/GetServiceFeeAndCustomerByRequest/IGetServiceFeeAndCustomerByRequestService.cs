using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetServiceFeeAndCustomerByRequest
{
    public interface IGetServiceFeeAndCustomerByRequestService
    {
        Task<ResultGetServiceFeeAndCustomerByRequestDto> Execute(int ri);
    }
}
