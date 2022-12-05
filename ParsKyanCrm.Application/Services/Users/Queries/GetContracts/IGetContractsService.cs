using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetContracts
{
    public interface IGetContractsService
    {
        Task<ResultDto<IEnumerable<ContractDto>>> Execute(RequestContractDto request);
    }

}
