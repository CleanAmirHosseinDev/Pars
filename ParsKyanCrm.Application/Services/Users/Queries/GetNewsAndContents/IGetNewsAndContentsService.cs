
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetNewsAndContents
{
    public interface IGetNewsAndContentsService
    {
        Task<ResultDto<IEnumerable<NewsAndContentDto>>> Execute(RequestNewsAndContentDto request);
    }
}
