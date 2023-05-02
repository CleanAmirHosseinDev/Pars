using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetContract
{
    public interface IGetContractPagessService
    {
        Task<ResultDto<IEnumerable<ContractPagesDto>>> Execute(RequestContractPagesDto request);

    }
}
