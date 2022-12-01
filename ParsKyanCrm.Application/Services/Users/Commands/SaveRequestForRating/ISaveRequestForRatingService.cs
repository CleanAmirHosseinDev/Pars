using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveRequestForRating
{
    public interface ISaveRequestForRatingService
    {
        Task<ResultDto> Execute(RequestReferencesDto request);
    }
}
