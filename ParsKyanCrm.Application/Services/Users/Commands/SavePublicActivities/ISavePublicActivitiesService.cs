using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SavePublicActivities
{
    public interface ISavePublicActivitiesService
    {
        Task<ResultDto<PublicActivitiesDto>> Execute(PublicActivitiesDto request);
    }
}
