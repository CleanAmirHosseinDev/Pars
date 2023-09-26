using Microsoft.AspNetCore.Http;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveManagerOfParsKyan
{
    public interface ISaveManagerOfParsKyanService
    {
        Task<ResultDto<ManagerOfParsKyanDto>> Execute(ManagerOfParsKyanDto request);
    }

}
