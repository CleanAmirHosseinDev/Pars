using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFromAnswerss
{
    public interface IGetDataFromAnswerssService
    {
        Task<ResultDto<IEnumerable<DataFromAnswersDto>>> Execute(RequestDataFromAnswersDto request);
    }
}
