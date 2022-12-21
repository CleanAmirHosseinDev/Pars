using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveNewsAndContent
{
    public interface ISaveNewsAndContentService
    {
        Task<ResultDto<NewsAndContentDto>> Execute(NewsAndContentDto request);
    }
}
