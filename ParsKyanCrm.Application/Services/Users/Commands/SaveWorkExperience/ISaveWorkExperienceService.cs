using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveWorkExperience
{
    public interface ISaveWorkExperienceService
    {
        Task<ResultDto<WorkExperienceDto>> Execute(WorkExperienceDto request);
    }
}
