using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveFurtherInfo
{
    public interface ISaveFurtherInfoService
    {
        Task<ResultDto<FurtherInfoDto>> Execute(FurtherInfoDto request);
        Task<ResultDto<FurtherInfoDto>> ExecuteCopy(string Request);
    }
}
