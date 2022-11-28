using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveEducationCourses
{
    public interface ISaveEducationCoursesService
    {
        Task<ResultDto<EducationCoursesDto>> Execute(EducationCoursesDto request);
    }
}
