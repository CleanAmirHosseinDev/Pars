using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveLicensesAndHonors
{
    public interface ISaveLicensesAndHonorsService
    {
        Task<ResultDto<LicensesAndHonorsDto>> Execute(LicensesAndHonorsDto request);
    }
}
