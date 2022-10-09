using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParsKyanCrm.Infrastructure
{
    public static class DateTimeOperation
    {
        public static string ToPersianDate(this DateTime dt, bool isTime = false)
        {
            try
            {
                if (!IsPersianDate(dt.ToString(FormatDate_yyyyMMdd)))
                {
                    PersianCalendar pc = new PersianCalendar();
                    int year = pc.GetYear(dt);
                    int month = pc.GetMonth(dt);
                    int day = pc.GetDayOfMonth(dt);
                    int hour = pc.GetHour(dt);
                    int min = pc.GetMinute(dt);

                    return year.ToString() + "/" + (month <= 9 ? "0" : string.Empty) + month.ToString() + "/" + (day <= 9 ? "0" : string.Empty) + day.ToString() + (isTime ? " " + hour.ToString() + ":" + min.ToString() : string.Empty);
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string _PersianNumDate(DateTime inputDate)
        {
            try
            {
                PersianCalendar oPersianC = new PersianCalendar();
                string Day, Month, Year, Date = "";

                if (inputDate != null)
                {
                    Year = oPersianC.GetYear(inputDate).ToString();
                    Month = oPersianC.GetMonth(inputDate).ToString();
                    if (Month.Length < 2)
                    {
                        Month = "0" + Month;
                    }
                    Day = oPersianC.GetDayOfMonth(inputDate).ToString();
                    if (Day.Length < 2)
                    {
                        Day = "0" + Day;
                    }

                    Date = Year + "/" + Month + "/" + Day;
                }
                else
                {
                }
                return Date;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string ConvertToMyDateTimeFormat(Nullable<DateTime> value, CultureInfo IFormateProvider)
        {
            try
            {
                if (value.HasValue)
                {
                    if (value.Value.TimeOfDay.Ticks > 0)
                    {
                        return value.Value.ToString(IFormateProvider);
                    }
                    else
                    {
                        return value.Value.ToString(IFormateProvider.DateTimeFormat.ShortDatePattern);
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DateTime PersianToGregorian0Time(string shamsi)
        {
            try
            {
                var Now = DateTime.Now;

                System.Globalization.PersianCalendar Mdate = new System.Globalization.PersianCalendar();
                DateTime MDate;
                string[] prdate = shamsi.Split('/');
                MDate = Mdate.ToDateTime(Convert.ToInt32((prdate[0])), Convert.ToInt32(prdate[1]), Convert.ToInt32(prdate[2]), 0, 0, 0, 0, System.Globalization.GregorianCalendar.ADEra);
                return MDate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime ToMiladiDate(this DateTime dt)
        {
            try
            {
                if (IsPersianDate(dt.ToString(FormatDate_yyyyMMdd)))
                {
                    PersianCalendar pc = new PersianCalendar();
                    return pc.ToDateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, 0);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static string ToStringShamsiDate(this DateTime dt)
        {
            try
            {
                PersianCalendar PC = new PersianCalendar();
                int intYear = PC.GetYear(dt);
                int intMonth = PC.GetMonth(dt);
                int intDayOfMonth = PC.GetDayOfMonth(dt);
                DayOfWeek enDayOfWeek = PC.GetDayOfWeek(dt);
                string strMonthName = "";
                string strDayName = "";
                switch (intMonth)
                {
                    case 1:
                        strMonthName = "فروردین";
                        break;
                    case 2:
                        strMonthName = "اردیبهشت";
                        break;
                    case 3:
                        strMonthName = "خرداد";
                        break;
                    case 4:
                        strMonthName = "تیر";
                        break;
                    case 5:
                        strMonthName = "مرداد";
                        break;
                    case 6:
                        strMonthName = "شهریور";
                        break;
                    case 7:
                        strMonthName = "مهر";
                        break;
                    case 8:
                        strMonthName = "آبان";
                        break;
                    case 9:
                        strMonthName = "آذر";
                        break;
                    case 10:
                        strMonthName = "دی";
                        break;
                    case 11:
                        strMonthName = "بهمن";
                        break;
                    case 12:
                        strMonthName = "اسفند";
                        break;
                    default:
                        strMonthName = "";
                        break;
                }

                //switch (enDayOfWeek)
                //{
                //    case DayOfWeek.Friday:
                //        strDayName = "جمعه";
                //        break;
                //    case DayOfWeek.Monday:
                //        strDayName = "دوشنبه";
                //        break;
                //    case DayOfWeek.Saturday:
                //        strDayName = "شنبه";
                //        break;
                //    case DayOfWeek.Sunday:
                //        strDayName = "یکشنبه";
                //        break;
                //    case DayOfWeek.Thursday:
                //        strDayName = "پنجشنبه";
                //        break;
                //    case DayOfWeek.Tuesday:
                //        strDayName = "سه شنبه";
                //        break;
                //    case DayOfWeek.Wednesday:
                //        strDayName = "چهارشنبه";
                //        break;
                //    default:
                //        strDayName = "";
                //        break;
                //}

                return (string.Format("{0} {1} {2} {3}", strDayName, intDayOfMonth, strMonthName, intYear));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsPersianDate(this string str)
        {
            try
            {
                if (Regex.IsMatch(str, @"^(13\d{2}|[1-9]\d)/(1[012]|0?[1-9])/([12]\d|3[01]|0?[1-9])$") || Regex.IsMatch(str, @"^(14\d{2}|[1-9]\d)/(1[012]|0?[1-9])/([12]\d|3[01]|0?[1-9])$"))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsPersianDateTime(this string str)
        {
            try
            {
                return Regex.IsMatch(str, @"^(13\d{2}|[1-9]\d)/(1[012]|0?[1-9])/([12]\d|3[01]|0?[1-9]) ([01][0-9]|2[0-3]):([0-5]?[0-9])$");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsTime(this string str)
        {
            try
            {
                return Regex.IsMatch(str, @"^([01][0-9]|2[0-3]):([0-5]?[0-9])$");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsTimeSpan12(this string str)
        {
            try
            {
                return Regex.IsMatch(str, @"^(1[012]|[1-9]):([0-5]?[0-9]) (AM|am|PM|pm)$");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsTimeSpan12P(this string str)
        {
            try
            {
                return Regex.IsMatch(str, @"^(1[012]|[1-9]):([0-5]?[0-9]) (ق ظ|ق.ظ|ب ظ|ب.ظ)$");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsTimeSpan24hhm(this string str)
        {
            try
            {
                return Regex.IsMatch(str, @"^([01][0-9]|2[0-3]):([0-5]?[0-9])$");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsTimeSpan24hm(this string str)
        {
            try
            {
                return Regex.IsMatch(str, @"^(2[0-3]|[01]?\d):([0-5]?[0-9])$");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string FormatDate_yyyyMMdd
        {
            get
            {
                return "yyyy/MM/dd";
            }
        }

        public static string FormatTime_HHmmss
        {

            get
            {
                return "HH:mm:ss";
            }

        }

        public static string FormatDateTime_yyyyMMdd_HHmmss
        {
            get
            {
                return FormatDate_yyyyMMdd + " " + FormatTime_HHmmss;
            }
        }

        public static DateTime InsertFieldDataTimeInTables(DateTime item)
        {
            try
            {
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime ConvertStringToDateTime(string dateTime)
        {
            try
            {

                DateTime resEnd = DateTime.Now;

                if (string.IsNullOrEmpty(dateTime)) return resEnd;

                string[] dateTimeRes = null;
                if (dateTime.Contains(" ")) dateTimeRes = dateTime.Split(' ');

                string date;
                string time;

                if (dateTimeRes != null)
                {
                    date = dateTimeRes[0];
                    time = dateTimeRes[1];
                }
                else
                {
                    date = dateTime.Contains("/") ? dateTime : string.Empty;
                    time = dateTime.Contains(":") ? dateTime : string.Empty;
                }

                string[] dateEnd = null;
                string[] timeEnd = null;
                if (!string.IsNullOrEmpty(date)) dateEnd = date.Split('/');
                if (!string.IsNullOrEmpty(time)) timeEnd = time.Split(':');

                if (IsPersianDate(
                    (dateEnd != null ? int.Parse(dateEnd[0]) : resEnd.Year) + "/" +
                    (dateEnd != null ? int.Parse(dateEnd[1]) : resEnd.Month) + "/" +
                    (dateEnd != null ? int.Parse(dateEnd[2]) : resEnd.Day)))
                {
                    PersianCalendar p = new PersianCalendar();
                    return p.ToDateTime(
                        (dateEnd != null ? int.Parse(dateEnd[0]) : resEnd.Year),
                        (dateEnd != null ? int.Parse(dateEnd[1]) : resEnd.Month),
                        (dateEnd != null ? int.Parse(dateEnd[2]) : resEnd.Day),
                        (timeEnd != null ? int.Parse(timeEnd[0]) : 0),
                        (timeEnd != null ? int.Parse(timeEnd[1]) : 0),
                        0,
                        0
                        );
                }
                else
                {
                    return new DateTime(
                        (dateEnd != null ? int.Parse(dateEnd[0]) : resEnd.Year),
                        (dateEnd != null ? int.Parse(dateEnd[1]) : resEnd.Month),
                        (dateEnd != null ? int.Parse(dateEnd[2]) : resEnd.Day),
                        (timeEnd != null ? int.Parse(timeEnd[0]) : 0),
                        (timeEnd != null ? int.Parse(timeEnd[1]) : 0),
                        0,
                        0
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int Calculate_Age(DateTime dt)
        {
            try
            {
                var today = DateTime.Today;
                var a = (today.Year * 100 + today.Month) * 100 + today.Day;
                var b = (dt.Year * 100 + dt.Month) * 100 + dt.Day;
                return (a - b) / 10000;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
