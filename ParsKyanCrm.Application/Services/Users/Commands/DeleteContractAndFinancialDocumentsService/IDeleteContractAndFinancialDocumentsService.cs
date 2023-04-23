using ParsKyanCrm.Common.Dto;

namespace ParsKyanCrm.Application.Services.Users.Commands.DeleteContractAndFinancialDocumentsService
{
    public interface IDeleteContractAndFinancialDocumentsService
    {
        ResultDto Execute(int id);
    }
}
