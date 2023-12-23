using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Reports.Queries.NumberCodedFiles
{
    public interface INumberCodedFilesService
    {
        Task<ResultDto<IEnumerable<ResultNumberCodedFilesDto>>> Execute(RequestNumberCodedFilesDto request);
        Task<byte[]> Execute1(RequestNumberCodedFilesDto request);
    }
}
