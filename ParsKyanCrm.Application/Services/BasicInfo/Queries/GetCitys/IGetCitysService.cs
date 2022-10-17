using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetCitys
{
    public interface IGetCitysService
    {
        Task<ResultDto<IEnumerable<CityDto>>> Execute(RequestCityDto request);
    }
}
