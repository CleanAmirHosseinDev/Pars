
using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetState
{
    public interface IGetStateService
    {
        Task<StateDto> Execute(int? id = null);
    }
}
