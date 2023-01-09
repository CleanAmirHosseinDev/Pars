using ParsKyanCrm.Common.Dto;

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.DeleteActivity
{
    public interface IDeleteActivityService
    {
        ResultDto Execute(int id);
    }
}
