using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveDataFormAnswerTables
{
    public interface ISaveDataFormAnswerTablesService
    {
        Task<ResultDto<DataFormAnswerTablesDto>> Execute(DataFormAnswerTablesDto request);
    }
}
