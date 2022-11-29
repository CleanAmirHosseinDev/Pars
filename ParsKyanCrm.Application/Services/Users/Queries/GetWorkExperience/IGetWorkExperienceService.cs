using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetWorkExperience
{
    public interface IGetWorkExperienceService
    {
        Task<WorkExperienceDto> Execute(RequestWorkExperienceDto request);
    }
}
