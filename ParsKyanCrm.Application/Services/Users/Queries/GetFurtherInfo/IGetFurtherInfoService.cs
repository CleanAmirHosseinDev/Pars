using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetFurtherInfo
{
    public interface IGetFurtherInfoService
    {
        Task<ResultDto<FurtherInfoDto>> Execute(RequestFurtherInfoDto request);
    }
}
