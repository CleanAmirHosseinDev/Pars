
using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetActivity
{
    public interface IGetActivityService
    {
        Task<ActivityDto> Execute(RequestActivityDto request);
    }
}
