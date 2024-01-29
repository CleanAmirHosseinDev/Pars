using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveDataFromAnswers
{
    public interface ISaveDataFromAnswersService
    {
        Task<ResultDto<DataFromAnswersDto>> Execute(DataFromAnswersDto request);
        Task<ResultDto<DataFromAnswersDto>> ExecuteCopy(string Request);
    }
}
