using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetBoardOfDirectorss
{
    public interface IGetBoardOfDirectorssService
    {
        Task<ResultDto<IEnumerable<BoardOfDirectorsDto>>> Execute(RequestBoardOfDirectorsDto request);
    }
}
