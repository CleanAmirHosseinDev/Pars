
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveActivity
{
    public interface ISaveActivityService
    {
        Task<ResultDto<ActivityDto>> Execute(ActivityDto request);
    }
}
