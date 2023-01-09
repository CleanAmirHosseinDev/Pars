using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.UpdatePassUsers
{
    public interface IUpdatePassUsersService
    {
        Task<ResultDto> Execute(RequestUpdatePassUsersDto request);
    }
}
