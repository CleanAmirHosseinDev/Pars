
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetManagerOfParsKyans
{
    public interface IGetManagerOfParsKyansService
    {
        Task<ResultDto<IEnumerable<ManagerOfParsKyanDto>>> Execute(RequestManagerOfParsKyanDto request);
    }
}
