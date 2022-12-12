using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.InitReferral
{
    public interface IInitReferralService
    {

        Task<ResultDto<IEnumerable<LevelStepSettingDto>>> Execute(string loginName, int? id = null);

    }
}
