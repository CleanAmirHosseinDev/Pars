using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetLicensesAndHonorss
{
    public interface IGetLicensesAndHonorssService
    {
        Task<ResultDto<IEnumerable<LicensesAndHonorsDto>>> Execute(RequestLicensesAndHonorsDto request);
    }
}
