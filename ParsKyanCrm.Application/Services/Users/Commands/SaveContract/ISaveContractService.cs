using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveContract
{
    public interface ISaveContractService
    {
        Task<ResultDto<ContractDto>> Execute(ContractDto request);
    }
}
