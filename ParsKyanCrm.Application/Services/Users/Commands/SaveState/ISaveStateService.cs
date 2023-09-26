
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveState
{
    public interface ISaveStateService
    {
        Task<ResultDto<StateDto>> Execute(StateDto request);
    }
}
