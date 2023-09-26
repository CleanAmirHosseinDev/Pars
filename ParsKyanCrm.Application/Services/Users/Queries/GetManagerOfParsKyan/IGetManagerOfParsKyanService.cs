
using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetManagerOfParsKyan
{
    public interface IGetManagerOfParsKyanService
    {
        Task<ManagerOfParsKyanDto> Execute(RequestManagerOfParsKyanDto request);
    }
}
