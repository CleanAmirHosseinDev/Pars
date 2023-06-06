using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveCustomers_RegisterLanding
{
    public interface ISaveCustomers_RegisterLandingService
    {
        Task<ResultDto> Execute(Customers_RegisterLandingDto request);
    }
}
