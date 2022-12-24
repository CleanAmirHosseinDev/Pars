using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetNewsAndContents
{
    public interface IGetNewsAndContentsService
    {
        Task<ResultDto<IEnumerable<NewsAndContentDto>>> Execute(RequestNewsAndContentDto request);
    }
}
