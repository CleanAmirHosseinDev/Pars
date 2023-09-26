
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetSystemSetings
{
    public interface IGetSystemSetingsService
    {
        Task<ResultDto<IEnumerable<SystemSetingDto>>> Execute(RequestSystemSetingDto request);
    }
}
