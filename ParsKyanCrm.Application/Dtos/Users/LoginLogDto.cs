using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{
    public class LoginLogDto
    {

        public string FullName { get; set; }

        public int LoginLogID { get; set; }

        public int Userid { get; set; }

        public DateTime? LoginDate { get; set; }
        public string LoginTimeStr
        {
            get
            {
                return LoginDate.HasValue ? LoginDate.Value.ToShortTimeString() : string.Empty;
            }
        }
        public string LoginDateStr
        {
            get
            {
                return LoginDate.HasValue ? DateTimeOperation.ToPersianDate(LoginDate.Value) : string.Empty;
            }
        }

        public string Ip { get; set; }

        public DateTime? SignOutDate { get; set; }
        public string SignOutTimeStr
        {
            get
            {
                return SignOutDate.HasValue ? SignOutDate.Value.ToShortTimeString() : string.Empty;
            }
        }
        public string SignOutDateStr
        {
            get
            {
                return SignOutDate.HasValue ? DateTimeOperation.ToPersianDate(SignOutDate.Value) : string.Empty;
            }
        }

        public string AreaName { get; set; }
        public int TotalRows { get; set; }
    }

    public class RequestLoginLogDto : PageingParamerDto
    {
        public string FromDateStr { get; set; }
        public string ToDateStr { get; set; }

        public DateTime? FromDateStr1 =>
            string.IsNullOrWhiteSpace(FromDateStr) ? (DateTime?)null :
            ConvertShamsiToMiladi(FromDateStr);

        public DateTime? ToDateStr1 =>
            string.IsNullOrWhiteSpace(ToDateStr) ? (DateTime?)null :
            ConvertShamsiToMiladi(ToDateStr);

        private DateTime ConvertShamsiToMiladi(string persianDate)
        {
            var pc = new System.Globalization.PersianCalendar();
            var parts = persianDate.Split('/');
            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int day = int.Parse(parts[2]);

            return pc.ToDateTime(year, month, day, 0, 0, 0, 0);
        }
    }
}
