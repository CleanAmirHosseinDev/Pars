using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetContractAndFinancialDocuments
{
    public interface IGetContractAndFinancialDocumentsService
    {
        Task<ResultDto<ContractAndFinancialDocumentsDto>> Execute(RequestContractAndFinancialDocumentsDto request);
    }
}
