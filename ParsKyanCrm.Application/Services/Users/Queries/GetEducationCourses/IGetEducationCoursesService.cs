using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetEducationCourses
{
    public interface IGetEducationCoursesService
    {
        Task<EducationCoursesDto> Execute(RequestEducationCoursesDto request);
    }
}
