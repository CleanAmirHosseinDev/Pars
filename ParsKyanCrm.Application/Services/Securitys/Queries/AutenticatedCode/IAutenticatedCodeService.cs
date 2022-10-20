using ParsKyanCrm.Application.Services.Securitys.Queries.Logins;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Securitys.Queries.AutenticatedCode
{
    public interface IAutenticatedCodeService
    {
        Task<ResultDto<ResultLoginDto>> Execute(RequestAutenticatedCodeDto request);
    }
}
