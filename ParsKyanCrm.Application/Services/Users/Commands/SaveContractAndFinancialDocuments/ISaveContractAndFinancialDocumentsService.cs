using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveContractAndFinancialDocuments
{
    public interface ISaveContractAndFinancialDocumentsService
    {
        Task<ResultDto<ContractAndFinancialDocumentsDto>> Execute(ContractAndFinancialDocumentsDto request);
    }
}
