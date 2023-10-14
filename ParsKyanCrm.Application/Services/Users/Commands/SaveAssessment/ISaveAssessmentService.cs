using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveAssessment
{
    public interface ISaveAssessmentService
    {
        ResultDto Execute(RequestSaveAssessmentDto request);
    }
}
