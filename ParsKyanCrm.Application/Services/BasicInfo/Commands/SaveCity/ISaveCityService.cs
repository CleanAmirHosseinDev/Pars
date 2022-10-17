using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveCity
{
    public interface ISaveCityService
    {
        Task<ResultDto<CityDto>> Execute(CityDto request);
    }
}
