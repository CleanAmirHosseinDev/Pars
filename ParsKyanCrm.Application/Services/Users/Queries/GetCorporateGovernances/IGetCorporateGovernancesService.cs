using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetCorporateGovernances
{
    public interface IGetCorporateGovernancesService
    {
        Task<ResultDto<CorporateGovernanceDto>> Execute(RequestCorporateGovernanceDto request);
    }
}
