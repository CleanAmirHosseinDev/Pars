using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class NewsAndContent
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime? DateNews { get; set; }
        public int? Isactive { get; set; }
        public int? Userid { get; set; }
        public int? KindOfContent { get; set; }
        public string Keywords { get; set; }
        public byte? IsConfirmByAdmin { get; set; }

        public virtual SystemSeting11 KindOfContentNavigation { get; set; }
        public virtual Users User { get; set; }
    }
}
