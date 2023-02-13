using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetPublicActivities
{
    public interface IGetPublicActivitiesService
    {
        Task<ResultDto<PublicActivitiesDto>> Execute(RequestPublicActivitiesDto request);
    }
}
