using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.PersianNumber;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{

    public class RequestNewsAndContentDto : PageingParamerDto
    {

        public int? ContentId { get; set; }
        public int? KindOfContent { get; set; }
        public string DirectLink { get; set; }

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
        public string DatePersianYear {
            get {
                return DateSave.HasValue ? (PersianNumberHelper.EnglishToPersian(Infrastructure.DateTimeOperation.ToPersianDate(DateSave.Value).Substring(0, 4)) + "") : "----";
            }
        }
        public string DatePersianDayAndMonth {
            get {
                var s = DateSave.HasValue ? (PersianNumberHelper.EnglishToPersian(Infrastructure.DateTimeOperation.ToStringShamsiDate(DateSave.Value)) + "") : "---- --";
                if(s.Length > 5)
                    s = s.Substring(0, s.Length - 5);
                return s;

            }
        }
        public string DatePersianDayOfWeek {
            get {
                return DateSave.HasValue ? (Infrastructure.DateTimeOperation.ToStringShamsiDayOfWeek(DateSave.Value) + "") : "-----";
            }
        }
        public string DatePersianFullDate {
            get {
                return DatePersianDayOfWeek + " " + DatePersianDayAndMonth + " " + DatePersianYear;
            }
        }
        public string DatePersianShortDate {
            get {
                return DateSave.HasValue ? (PersianNumberHelper.EnglishToPersian(Infrastructure.DateTimeOperation.ToPersianDate(DateSave.Value)) + "") : "--/--/----";
            }
        }
        public string Summary { get; set; }
        public string MeteDesc { get; set; }
        public string Keywords { get; set; }
        public int? UserId { get; set; }
        public byte? IsConfirmByAdmin { get; set; }

        public SystemSetingDto KindOfContentNavigation { get; set; }
        public UsersDto User { get; set; }
        public string DirectLink { get; set; }

    }
}
