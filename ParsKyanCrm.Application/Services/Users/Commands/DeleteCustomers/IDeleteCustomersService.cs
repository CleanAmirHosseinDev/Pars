using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.DeleteCustomers
{
    public interface IDeleteCustomersService
    {
        Task<ResultDto> Execute(int id);
    }
}
