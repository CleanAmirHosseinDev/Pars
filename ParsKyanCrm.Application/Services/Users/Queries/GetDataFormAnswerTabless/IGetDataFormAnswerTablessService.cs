using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFormAnswerTabless
{
    public interface IGetDataFormAnswerTablessService
    {
        Task<ResultDto<IEnumerable<DataFormAnswerTablesDto>>> Execute(RequestDataFormAnswerTablesDto request);
       
    }
}
