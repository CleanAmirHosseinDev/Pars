using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveDataFormDocument
{
    public interface ISaveSaveDataFormDocumentService
    {
        Task<ResultDto<DataFormDocumentsDto>> Execute(DataFormDocumentsDto request);
    }

    
}
