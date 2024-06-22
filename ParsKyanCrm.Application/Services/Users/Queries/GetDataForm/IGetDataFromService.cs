using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataForm
{
    public interface IGetDataFormService
    {
        Task<DataFormsDto> Execute(int id);
    }
}
