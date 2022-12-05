using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveActivity
{
    public interface ISaveActivityService
    {
        Task<ResultDto<ActivityDto>> Execute(ActivityDto request);
    }
}
