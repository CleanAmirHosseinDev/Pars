
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveNewsAndContent
{
    public interface ISaveNewsAndContentService
    {
        Task<ResultDto<NewsAndContentDto>> Execute(NewsAndContentDto request);
    }
}
