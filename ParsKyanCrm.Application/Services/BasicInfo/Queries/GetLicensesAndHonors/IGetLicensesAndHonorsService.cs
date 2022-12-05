using ParsKyanCrm.Application.Dtos.BasicInfo;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetLicensesAndHonors
{
    public interface IGetLicensesAndHonorsService
    {
        Task<LicensesAndHonorsDto> Execute(RequestLicensesAndHonorsDto request);
    }
}
