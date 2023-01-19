using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetContractAndFinancialDocuments
{
    public interface IGetContractAndFinancialDocumentsService
    {
        Task<ContractAndFinancialDocumentsDto> Execute(RequestContractAndFinancialDocumentsDto request);
    }
}
