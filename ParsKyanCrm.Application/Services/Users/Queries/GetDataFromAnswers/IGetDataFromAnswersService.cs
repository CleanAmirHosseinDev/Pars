using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFromAnswers
{
    public interface IGetDataFromAnswersService
    {
        Task<DataFromAnswersDto> Execute(int? id = null);
    }
}
