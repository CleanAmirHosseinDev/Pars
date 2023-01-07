using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveCustomers
{
    public interface ISaveCustomersService
    {
        Task<ResultDto> Execute(CustomersDto request);
    }
}
