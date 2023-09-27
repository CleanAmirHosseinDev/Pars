

using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.InsertLoginLog
{
    public interface IInsertLoginLogService
    {
        Task Execute(LoginLogDto request, bool isLogin = true);
    }
}
