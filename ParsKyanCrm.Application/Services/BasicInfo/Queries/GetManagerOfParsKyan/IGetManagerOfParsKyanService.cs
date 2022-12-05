using ParsKyanCrm.Application.Dtos.BasicInfo;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetManagerOfParsKyan
{
    public interface IGetManagerOfParsKyanService
    {
        Task<ManagerOfParsKyanDto> Execute(RequestManagerOfParsKyanDto request);
    }
}
