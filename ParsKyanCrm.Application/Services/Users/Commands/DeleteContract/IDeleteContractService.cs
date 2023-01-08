using ParsKyanCrm.Common.Dto;

namespace ParsKyanCrm.Application.Services.Users.Commands.DeleteContract
{
    public interface IDeleteContractService
    {
        ResultDto Execute(int id);
    }
}
