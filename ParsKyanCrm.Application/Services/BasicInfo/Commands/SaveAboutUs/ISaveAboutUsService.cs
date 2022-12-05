using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveAboutUs
{
    public interface ISaveAboutUsService
    {
        Task<ResultDto<AboutUsDto>> Execute(AboutUsDto request);
    }
}
