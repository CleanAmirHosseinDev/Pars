using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetCustomerRequestInformation
{
    public interface IGetCustomerRequestInformationService
    {
        Task<CustomerRequestInformationsDto> Execute(RequestCustomerRequestInformationDto request);
    }
}
