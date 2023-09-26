using ParsKyanCrm.Common.Dto;

namespace ParsKyanCrm.Application.Services.Users.Commands.DeleteActivity
{
    public interface IDeleteActivityService
    {
        ResultDto Execute(int id);
    }
}
