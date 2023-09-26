
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveLicensesAndHonors
{
    public interface ISaveLicensesAndHonorsService
    {
        Task<ResultDto<LicensesAndHonorsDto>> Execute(LicensesAndHonorsDto request);
    }
}
