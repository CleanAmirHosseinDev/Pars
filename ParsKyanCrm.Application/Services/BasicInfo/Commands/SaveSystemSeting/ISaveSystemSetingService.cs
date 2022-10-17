using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveSystemSeting
{
    public interface ISaveSystemSetingService
    {
        Task<ResultDto<SystemSetingDto>> Execute(SystemSetingDto request);
    }
}
