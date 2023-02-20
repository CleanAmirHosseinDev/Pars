using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetLevelStepSettings
{
    public interface IGetLevelStepSettingsService
    {
        Task<ResultDto<IEnumerable<LevelStepSettingDto>>> Execute(RequestLevelStepSettingDto request);
    }
}
