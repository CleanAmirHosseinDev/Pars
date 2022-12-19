using Microsoft.AspNetCore.Http;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveManagerOfParsKyan
{
    public interface ISaveManagerOfParsKyanService
    {
        Task<ResultDto<ManagerOfParsKyanDto>> Execute(ManagerOfParsKyanDto request, IFormCollection formCollection);
    }

}
