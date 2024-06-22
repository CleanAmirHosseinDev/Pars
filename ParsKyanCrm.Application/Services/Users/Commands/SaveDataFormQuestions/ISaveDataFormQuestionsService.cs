using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveDataFormQuestions
{
    public interface ISaveDataFormQuestionsService
    {
        Task<ResultDto<DataFormQuestionsDto>> Execute(DataFormQuestionsDto request);
        Task<ResultDto<DataFormQuestionsDto>> ExecuteCopy(string request);
    }
}
