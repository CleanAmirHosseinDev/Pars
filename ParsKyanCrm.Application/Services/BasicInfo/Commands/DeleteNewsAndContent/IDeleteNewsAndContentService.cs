using ParsKyanCrm.Common.Dto;

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.DeleteNewsAndContent
{
    public interface IDeleteNewsAndContentService
    {
        ResultDto Execute(int id);
    }
}
