using ParsKyanCrm.Application.Dtos.BasicInfo;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetState
{
    public interface IGetStateService
    {
        Task<StateDto> Execute(int? id = null);
    }
}
