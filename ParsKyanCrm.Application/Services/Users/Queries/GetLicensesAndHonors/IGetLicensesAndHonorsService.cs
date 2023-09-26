
using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetLicensesAndHonors
{
    public interface IGetLicensesAndHonorsService
    {
        Task<LicensesAndHonorsDto> Execute(RequestLicensesAndHonorsDto request);
    }
}
