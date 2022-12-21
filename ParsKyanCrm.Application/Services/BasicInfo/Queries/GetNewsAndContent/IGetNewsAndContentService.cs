using ParsKyanCrm.Application.Dtos.BasicInfo;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetNewsAndContent
{
    public interface IGetNewsAndContentService
    {
        Task<NewsAndContentDto> Execute(RequestNewsAndContentDto request);
    }
}
