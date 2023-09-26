
using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetCity
{
    public interface IGetCityService
    {
        Task<CityDto> Execute(int? id = null);
    }
}
