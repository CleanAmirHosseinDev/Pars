using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetUsers
{
    public interface IGetUsersService
    {
        Task<UserRolesDto> Execute(RequestUserRolesDto request);
    }
}
