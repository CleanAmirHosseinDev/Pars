using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetQuestionLevels
{
    public interface IGetQuestionLevelsService
    {
        Task<ResultDto<IEnumerable<QuestionLevelDto>>> Execute(RequestCQuestionLevelDto request);
    }
}
