
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetCitys
{
    public interface IGetCitysService
    {
        Task<ResultDto<IEnumerable<CityDto>>> Execute(RequestCityDto request);
    }
}
