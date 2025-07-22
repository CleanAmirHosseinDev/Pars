using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ParsKyanCrm.Application.Services.Users.Commands
{
    public class AddRequestCommentService : IAddRequestCommentService
    {
        private readonly IDataBaseContext _context;

        public AddRequestCommentService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> Execute(int requestId, string comment)
        {
            var request = await _context.RequestForRatings.FindAsync(requestId);

            if (request == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "درخواست مورد نظر یافت نشد."
                };
            }

            request.Comment = comment;
            await _context.SaveChangesAsync();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "کامنت با موفقیت ثبت شد."
            };
        }
    }
}
