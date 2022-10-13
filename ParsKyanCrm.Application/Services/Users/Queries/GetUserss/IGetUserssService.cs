using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetUserss
{
    public interface IGetUserssService
    {
        Task<ResultDto<IEnumerable<UserRolesDto>>> Execute(RequestUserRolesDto request);
    }
}
