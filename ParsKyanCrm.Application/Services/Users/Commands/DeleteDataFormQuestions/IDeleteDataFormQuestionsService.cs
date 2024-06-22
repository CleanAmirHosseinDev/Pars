using ParsKyanCrm.Common.Dto;

namespace ParsKyanCrm.Application.Services.Users.Commands.DeleteDataFormQuestions
{
    public interface IDeleteDataFormQuestionsService
    {
        ResultDto Execute(int id);
    }
}
