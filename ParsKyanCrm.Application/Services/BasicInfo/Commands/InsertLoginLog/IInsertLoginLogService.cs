using ParsKyanCrm.Application.Dtos.BasicInfo;

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.InsertLoginLog
{
    public interface IInsertLoginLogService
    {
        void Execute(LoginLogDto request, bool isLogin = true);
    }
}
