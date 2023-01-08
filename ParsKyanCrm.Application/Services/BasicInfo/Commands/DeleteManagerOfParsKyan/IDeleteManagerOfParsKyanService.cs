using ParsKyanCrm.Common.Dto;

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.DeleteManagerOfParsKyan
{
    public interface IDeleteManagerOfParsKyanService
    {
        ResultDto Execute(int id);
    }
}
