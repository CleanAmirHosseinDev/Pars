using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveDataFormQuestionsOption
{
    public interface ISaveDataFormQuestionsOptionService
    {
        Task<ResultDto<DataFormQuestionsOptionDto>> Execute(DataFormQuestionsOptionDto request);
        Task<ResultDto<DataFormQuestionsOptionDto>> ExecuteCopy(string request);
    }
}
