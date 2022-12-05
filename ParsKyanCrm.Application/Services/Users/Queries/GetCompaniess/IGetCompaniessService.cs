using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetCompaniess
{
    public interface IGetCompaniessService
    {
        Task<ResultDto<IEnumerable<CompaniesDto>>> Execute(RequestCompaniesDto request);
    }
}
