using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.BasicInfo
{
    public class LoginLogDto
    {

        public string FullName { get; set; }

        public int LoginLogID { get; set; }

        public int Userid { get; set; }

        public DateTime? LoginDate { get; set; }
        public string LoginDateStr
        {
            get
            {
                return LoginDate.HasValue ? DateTimeOperation.ToPersianDate(LoginDate.Value) : string.Empty;
            }
        }

        public string Ip { get; set; }

        public DateTime? SignOutDate { get; set; }
        public string SignOutDateStr
        {
            get
            {
                return SignOutDate.HasValue ? DateTimeOperation.ToPersianDate(SignOutDate.Value) : string.Empty;
            }
        }

        public string AreaName { get; set; }
    }

    public class RequestLoginLogDto : PageingParamerDto
    {
        
        public string FromDateStr { get; set; }
        public string FromDateStr1
        {
            get
            {
                if (!string.IsNullOrEmpty(FromDateStr))
                {
                    DateTime dtFromDate = DateTimeOperation.ToMiladiDate(DateTimeOperation.InsertFieldDataTimeInTables(DateTimeOperation.ConvertStringToDateTime(FromDateStr)));
                    return "'" + dtFromDate.Year +
                              "/" + dtFromDate.Month +
                              "/" + dtFromDate.Day + "'";
                }
                return "''";
            }

        }
        
        public string ToDateStr { get; set; }
        public string ToDateStr1
        {
            get
            {
                if (!string.IsNullOrEmpty(ToDateStr))
                {
                    DateTime dtFromDate = DateTimeOperation.ToMiladiDate(DateTimeOperation.InsertFieldDataTimeInTables(DateTimeOperation.ConvertStringToDateTime(ToDateStr)));
                    return "'" + dtFromDate.Year +
                              "/" + dtFromDate.Month +
                              "/" + dtFromDate.Day + "'";
                }
                return "''";
            }
        }
    }

}
