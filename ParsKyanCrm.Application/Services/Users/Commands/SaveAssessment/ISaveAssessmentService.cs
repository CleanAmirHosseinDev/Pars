using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveAssessment
{
    public interface ISaveAssessmentService
    {
        Task<ResultDto> Execute(RequestSaveAssessmentDto request);
    }
}
