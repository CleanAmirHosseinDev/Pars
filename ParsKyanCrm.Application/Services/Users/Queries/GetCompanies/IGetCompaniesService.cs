using ParsKyanCrm.Application.Dtos.Users;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetCompanies
{
    public interface IGetCompaniesService
    {
        Task<CompaniesDto> Execute(RequestCompaniesDto request);
    }
}
