using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetCustomers
{
    public interface IGetCustomersService
    {
        Task<CustomersDto> Execute(RequestCustomersDto request);
    }
}
