using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFormQuestionss
{
    public interface IGetDataFormQuestionssService
    {
        Task<ResultDto<IEnumerable<DataFormQuestionsDto>>> Execute(RequestDataFormQuestionsDto request);
    }
}
