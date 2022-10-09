using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetStates
{
    public interface IGetStatesService
    {
        Task<ResultDto<IEnumerable<StateDto>>> Execute(RequestStateDto request);
    } 
}
