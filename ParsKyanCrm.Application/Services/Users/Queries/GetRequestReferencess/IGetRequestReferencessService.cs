using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetRequestReferencess
{
    public interface IGetRequestReferencessService
    {
        Task<ResultDto<IEnumerable<RequestReferencesDto>>> Execute(RequestRequestReferencesDto request);
    }
}
