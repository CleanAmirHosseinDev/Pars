using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveBoardOfDirectors
{
    public interface ISaveBoardOfDirectorsService
    {
        Task<ResultDto<BoardOfDirectorsDto>> Execute(BoardOfDirectorsDto request);
    }
}
