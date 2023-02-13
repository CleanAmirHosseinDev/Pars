using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetValueChain
{
    public interface IGetValueChainService
    {
        Task<ResultDto<ValueChainDto>> Execute(RequestValueChainDto request);
    }
}
