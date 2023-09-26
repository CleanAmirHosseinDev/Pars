
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetStates
{
    public interface IGetStatesService
    {
        Task<ResultDto<IEnumerable<StateDto>>> Execute(RequestStateDto request);
    } 
}
