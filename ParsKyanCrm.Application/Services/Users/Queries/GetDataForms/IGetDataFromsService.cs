using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataForms
{
    public interface IGetDataFormsService
    {
        Task<ResultDto<IEnumerable<DataFormsDto>>> Execute(RequestDataFormsDto request);
    }
}
