

using ParsKyanCrm.Application.Dtos.Users;

namespace ParsKyanCrm.Application.Services.Users.Commands.InsertLoginLog
{
    public interface IInsertLoginLogService
    {
        void Execute(LoginLogDto request, bool isLogin = true);
    }
}
