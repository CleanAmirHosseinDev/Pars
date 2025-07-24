
using ParsKyanCrm.Common;
using System;

namespace ParsKyanCrm.Application.Dtos.Users
{
    public class CommentDto : BaseEntityDto 
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; }

        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}