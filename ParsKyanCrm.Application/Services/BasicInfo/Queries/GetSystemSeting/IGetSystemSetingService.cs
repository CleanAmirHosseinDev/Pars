using ParsKyanCrm.Application.Dtos.BasicInfo;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetSystemSeting
{
    public interface IGetSystemSetingService
    {
        Task<SystemSetingDto> Execute(RequestSystemSetingDto request);
    }
}
