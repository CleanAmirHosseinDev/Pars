using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFormDocuments
{
    public interface IGetDataFormDocumentsService
    {
        Task<ResultDto<IEnumerable<DataFormDocumentsDto>>> Execute(RequestDataFormDocumentsDto request);
    }
}
