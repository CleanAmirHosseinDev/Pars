using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveQuestionLevel
{
    public interface ISaveQuestionLevelService
    {
        Task<ResultDto<QuestionLevelDto>> Execute(QuestionLevelDto request);
    }

    
}
