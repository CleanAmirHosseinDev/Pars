using ParsKyanCrm.Common.Dto;

namespace ParsKyanCrm.Application.Services.Users.Commands.DeleteManagerOfParsKyan
{
    public interface IDeleteManagerOfParsKyanService
    {
        ResultDto Execute(int id);
    }
}
