
using Org.BouncyCastle.Asn1.Ocsp;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFormQuestionsOptions
{
    public interface IGetDataFormQuestionsOptionsService
    {
        Task<ResultDto<IEnumerable<DataFormQuestionsOptionDto>>> Execute(RequestDataFormQuestionsOptionDto request);
    }
}
