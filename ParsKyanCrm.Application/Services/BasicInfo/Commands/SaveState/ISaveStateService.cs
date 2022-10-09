using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveState
{
    public interface ISaveStateService
    {
        Task<ResultDto<StateDto>> Execute(StateDto request);
    }
}
