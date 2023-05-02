using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetContract
{
    public interface IGetContractPagesService
    {

        Task<ContractPagesDto> Execute(RequestContractPagesDto request);

    }
}
