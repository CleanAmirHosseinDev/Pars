using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveUsers
{
    public interface ISaveUsersService
    {
        Task<ResultDto<UserRolesDto>> Execute(UserRolesDto request);
    }
}
