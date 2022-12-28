using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class NewsAndContent
    {
        public int ContentId { get; set; }
        public string Title { get; set; }
        public string ContentPic { get; set; }
        public int? KindOfContent { get; set; }
        public string Body { get; set; }
        public DateTime? DateSave { get; set; }
        public string Summary { get; set; }
        public string MeteDesc { get; set; }
        public int? IsActive { get; set; }
        public string Keywords { get; set; }
        public int? UserId { get; set; }
        public byte? IsConfirmByAdmin { get; set; }

        public virtual SystemSeting KindOfContentNavigation { get; set; }
        public virtual Users User { get; set; }

        public string DirectLink { get; set; }
    }
}
