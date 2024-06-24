using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFormQuestions
{
    public interface IGetDataFormQuestionsService
    {
        Task<DataFormQuestionsDto> Execute(int? id = null);
    }
}
