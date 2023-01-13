using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFormAnswerTables
{
    public interface IGetDataFormAnswerTablesService
    {
        Task<DataFormAnswerTablesDto> Execute(int? id = null);
    }
}
