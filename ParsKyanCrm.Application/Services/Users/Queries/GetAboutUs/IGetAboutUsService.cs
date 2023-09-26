
using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetAboutUs
{
    public interface IGetAboutUsService
    {
        Task<AboutUsDto> Execute();
    }
}
