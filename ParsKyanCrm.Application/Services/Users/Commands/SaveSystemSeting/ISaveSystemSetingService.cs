
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveSystemSeting
{
    public interface ISaveSystemSetingService
    {
        Task<ResultDto<SystemSetingDto>> Execute(SystemSetingDto request);
    }
}
