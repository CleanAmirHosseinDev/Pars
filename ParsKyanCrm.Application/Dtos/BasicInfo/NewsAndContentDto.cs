using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.BasicInfo
{

    public class RequestNewsAndContentDto : PageingParamerDto
    {

        public int? ContentId { get; set; }

    }

    public class NewsAndContentDto : BaseEntityDto
    {

        public int ContentId { get; set; }
        
        public string Title { get; set; }


        public string ContentPic { get; set; }
        public string Result_Final_ContentPic { get; set; }
        public string ContentPicFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(ContentPic, VaribleForName.NewsAndContentFolder);
            }
        }

        public int? KindOfContent { get; set; }
        public string Body { get; set; }
        public DateTime? DateSave { get; set; }
        public string Summary { get; set; }
        public string MeteDesc { get; set; }
        public string Keywords { get; set; }
        public int? UserId { get; set; }
        public byte? IsConfirmByAdmin { get; set; }

        public SystemSetingDto KindOfContentNavigation { get; set; }
        public UsersDto User { get; set; }

    }
}
