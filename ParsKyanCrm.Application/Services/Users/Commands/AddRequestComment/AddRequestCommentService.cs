using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands
{
    public class AddRequestCommentService : IAddRequestCommentService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public AddRequestCommentService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<ResultDto<CommentDto>> Execute(CommentDto commentDto)
        {
            try
            {
                EntityEntry<Comment> commentEntityEntry;

                var maxUserNameLength = 50;
                var maxCommentLength = 500;

                var userName = await _context.Users
                    .Where(u => u.UserId == commentDto.UserId)
                    .Select(u => u.UserName)
                    .FirstOrDefaultAsync();

                if (userName == null)
                {
                    return new ResultDto<CommentDto>
                    {
                        IsSuccess = false,
                        Message = "کاربر پیدا نشد."
                    };
                }

                commentDto.UserName = userName.Length > maxUserNameLength ? userName.Substring(0, maxUserNameLength) : userName;
                commentDto.CommentText = commentDto.CommentText.Length > maxCommentLength ? commentDto.CommentText.Substring(0, maxCommentLength) : commentDto.CommentText;
                commentDto.CreatedAt = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);

                var comment = _mapper.Map<Comment>(commentDto);
                commentEntityEntry = _context.Comments.Add(comment);
                await _context.SaveChangesAsync();

                commentDto = _mapper.Map<CommentDto>(commentEntityEntry.Entity);

                return new ResultDto<CommentDto>
                {
                    IsSuccess = true,
                    Message = "کامنت با موفقیت ثبت شد.",
                    Data = commentDto
                };
            }
            catch (DbUpdateException dbEx)
            {
                var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;

                return new ResultDto<CommentDto>
                {
                    IsSuccess = false,
                    Message = "DbUpdateException: " + innerMessage
                };
            }
            catch (Exception ex)
            {
                return new ResultDto<CommentDto>
                {
                    IsSuccess = false,
                    Message = "Exception: " + ex.Message
                };
            }
        }
    }
}