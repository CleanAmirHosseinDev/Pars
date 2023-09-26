
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveCity
{
    public interface ISaveCityService
    {
        Task<ResultDto<CityDto>> Execute(CityDto request);
    }
}
