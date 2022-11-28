using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetWorkExperiences
{
    public interface IGetWorkExperiencesService
    {
        Task<ResultDto<IEnumerable<WorkExperienceDto>>> Execute(RequestWorkExperienceDto request);
    }
}
