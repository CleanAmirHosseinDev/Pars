
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFormQuestionsOption
{
    public interface IGetDataFormQuestionsOptionService
    {
        Task<ResultDto<IEnumerable<DataFormQuestionsOptionDto>>> Execute(int? id);
    }
}
