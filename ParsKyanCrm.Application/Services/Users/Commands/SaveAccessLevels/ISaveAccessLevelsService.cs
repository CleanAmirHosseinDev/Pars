using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveAccessLevels
{
    public interface ISaveAccessLevelsService
    {
        ResultDto Execute(UserRolesDto request);
    }
}
