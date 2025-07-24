using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands
{
    public interface IAddRequestCommentService
    {
        Task<ResultDto<CommentDto>> Execute(CommentDto commentDto);
    }
}
