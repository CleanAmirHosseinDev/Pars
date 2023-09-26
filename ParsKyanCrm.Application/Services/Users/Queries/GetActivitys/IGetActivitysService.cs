
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetActivitys
{
    public interface IGetActivitysService
    {
        Task<ResultDto<IEnumerable<ActivityDto>>> Execute(RequestActivityDto request);
    }
}
