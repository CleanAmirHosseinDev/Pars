using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetBoardOfDirectors
{
    public interface IGetBoardOfDirectorsService
    {
        Task<BoardOfDirectorsDto> Execute(RequestBoardOfDirectorsDto request);
    }
}
