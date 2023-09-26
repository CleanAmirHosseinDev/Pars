
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveAboutUs
{
    public interface ISaveAboutUsService
    {
        Task<ResultDto<AboutUsDto>> Execute(AboutUsDto request);
    }
}
