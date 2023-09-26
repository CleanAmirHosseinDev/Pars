using ParsKyanCrm.Common.Dto;

namespace ParsKyanCrm.Application.Services.Users.Commands.DeleteNewsAndContent
{
    public interface IDeleteNewsAndContentService
    {
        ResultDto Execute(int id);
    }
}
