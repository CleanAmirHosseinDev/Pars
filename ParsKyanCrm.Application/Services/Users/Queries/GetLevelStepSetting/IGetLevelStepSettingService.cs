using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetLevelStepSetting
{
    public interface IGetLevelStepSettingService
    {
        Task<LevelStepSettingDto> Execute(RequestLevelStepSettingDto request);
    }
}
