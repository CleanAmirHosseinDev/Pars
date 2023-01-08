using ParsKyanCrm.Common.Dto;

namespace ParsKyanCrm.Application.Services.Users.Commands.DeleteUsers
{
    public interface IDeleteUsersService
    {
        ResultDto Execute(int id);
    }
}
