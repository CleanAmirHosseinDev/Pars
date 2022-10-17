using ParsKyanCrm.Application.Dtos.BasicInfo;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetCity
{
    public interface IGetCityService
    {
        Task<CityDto> Execute(int? id = null);
    }
}
