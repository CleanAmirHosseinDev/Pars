using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetRoless
{
    public interface IGetRolessService
    {
        Task<ResultDto<IEnumerable<RolesDto>>> Execute(RequestRolesDto request);
    }
}
