using ParsKyanCrm.Common.Dto;

namespace ParsKyanCrm.Application.Services.Users.Commands.DeleteServiceFee
{
    public interface IDeleteServiceFeeService
    {
        ResultDto Execute(int id);
    }
}
