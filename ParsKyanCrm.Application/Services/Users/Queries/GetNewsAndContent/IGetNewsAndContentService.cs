
using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetNewsAndContent
{
    public interface IGetNewsAndContentService
    {
        Task<NewsAndContentDto> Execute(RequestNewsAndContentDto request);
    }
}
