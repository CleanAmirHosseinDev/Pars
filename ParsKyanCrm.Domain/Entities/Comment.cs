using ParsKyanCrm.Domain.Entities;
using System;

public class Comment
{
    public int Id { get; set; }
    public int RequestId { get; set; }
    public int UserId { get; set; }
    public string CommentText { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UserName { get; set; }

    public virtual Users User { get; set; }
}
