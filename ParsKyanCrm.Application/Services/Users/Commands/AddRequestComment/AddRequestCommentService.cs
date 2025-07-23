using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands
{
    public class AddRequestCommentService : IAddRequestCommentService
    {
        private readonly IDataBaseContext _context;

        public AddRequestCommentService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> Execute(CommentDto commentDto)
        {
            try
            {
                var maxUserNameLength = 50;
                var maxCommentLength = 500;

                var userName = await _context.Users
                    .Where(u => u.UserId == commentDto.UserId)
                    .Select(u => u.UserName)
                    .FirstOrDefaultAsync();

                if (userName == null)
                    return new ResultDto { IsSuccess = false, Message = "کاربر پیدا نشد." };

                var comment = new Comment
                {
                    RequestId = commentDto.RequestId,
                    UserId = commentDto.UserId,
                    CommentText = commentDto.CommentText.Length > maxCommentLength ? commentDto.CommentText.Substring(0, maxCommentLength) : commentDto.CommentText,
                    UserName = userName.Length > maxUserNameLength ? userName.Substring(0, maxUserNameLength) : userName,
                    CreatedAt = DateTime.Now
                };

                try
                {
                    _context.Comments.Add(comment);
                    await _context.SaveChangesAsync();

                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = "کامنت با موفقیت ثبت شد."
                    };
                }
                catch (DbUpdateException dbEx)
                {
                    var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;

                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "DbUpdateException: " + innerMessage
                    };
                }
                catch (Exception ex)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "Exception: " + ex.Message
                    };
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}