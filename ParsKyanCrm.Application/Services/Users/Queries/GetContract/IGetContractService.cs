using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetContract
{
    public interface IGetContractService
    {

        Task<ContractDto> Execute(RequestContractDto request);

    }
}
