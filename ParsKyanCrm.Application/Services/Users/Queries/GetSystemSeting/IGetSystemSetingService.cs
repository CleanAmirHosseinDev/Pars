
using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetSystemSeting
{
    public interface IGetSystemSetingService
    {
        Task<SystemSetingDto> Execute(RequestSystemSetingDto request);
    }
}
