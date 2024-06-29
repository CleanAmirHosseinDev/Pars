using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveDataForm
{
    public interface ISaveDataFormService
    {
        Task<ResultDto<DataFormsDto>> Execute(DataFormsDto request);
    }

    
}
