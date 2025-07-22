using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands
{
    public interface IAddRequestCommentService
    {
        Task<ResultDto> Execute(int requestId, string comment);
    }
}
