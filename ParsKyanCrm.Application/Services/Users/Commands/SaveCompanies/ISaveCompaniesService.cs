using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveCompanies
{
    public interface ISaveCompaniesService
    {
        Task<ResultDto<CompaniesDto>> Execute(CompaniesDto request);
    }

}
