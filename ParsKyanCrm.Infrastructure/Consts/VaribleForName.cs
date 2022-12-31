using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Infrastructure.Consts
{
    public class VaribleForName
    {

        /// <summary>
        /// true ==> Debug
        /// false ==> Server Main
        /// null ==> Server Test
        /// </summary>
        public static bool? IsDebug
        {
            get
            {
                return false;
            }
        }

        public static string MainConnectionString
        {
            get
            {
                if (IsDebug == null)
                    return "";
                else if (IsDebug == true)
                    return "data source=77.238.123.197;initial catalog=ParsKyanCrmDB;user id=pars;password=Pars@10155;MultipleActiveResultSets=True";
                else
                    return "data source=172.16.21.6;initial catalog=ParsKyanCrmDB;user id=pars;password=Pars@10155;MultipleActiveResultSets=True";

            }
        }

        public static string Secret
        {
            get
            {
                return "3315169990345098_543267319_12345678900987654321_Mobin2352@gmail.com_Mobin2352@outlook.com_Ab123456Ba654321_ParsKyanCrm_Mohandes_Sarfrazi_Mobin_Morid_Ahmadi.!@#$%^&*()09876543211234567890";
            }
        }

        public static string BoardOfDirectorsFolder
        {
            get
            {
                return "/FileUpload/BoardOfDirectors/";
            }
        }

        public static string ActivityFolder
        {
            get
            {
                return "/FileUpload/Activity/";
            }
        }

        public static string LicensesAndHonorsFolder
        {
            get
            {
                return "/FileUpload/LicensesAndHonors/";
            }
        }

        public static string ManagerOfParsKyanFolder
        {
            get
            {
                return "/FileUpload/ManagerOfParsKyan/";
            }
        }

        public static string RankingOfCompaniesFolder
        {
            get
            {
                return "/FileUpload/RankingOfCompanies/";
            }
        }

        public static string NewsAndContentFolder
        {
            get
            {
                return "/FileUpload/NewsAndContent/";
            }
        }

        public static string CustomersFolder
        {
            get
            {
                return "/FileUpload/Customers/";
            }
        }

        public static string No_Photo
        {
            get
            {
                return "no-photo.png";
            }
        }

        #region شروع فرایند

        public static string DestLevelStepIndex
        {
            get
            {
                return "1";
            }
        }
        public static string LevelStepAccessRole
        {
            get
            {
                return "10";
            }
        }
        public static string LevelStepStatus
        {
            get
            {
                return " ارسال درخواست اولیه توسط مشتری  و در انتظار بررسی";
            }
        }
        public static string DestLevelStepIndexButton
        {
            get
            {
                return "ارجاع به مسئول امور ارزیابی جهت بررسی مشخصات اولیه مشتری";
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////

        public static string DestLevelStepIndex1
        {
            get
            {
                return "2";
            }
        }
        public static string LevelStepAccessRole1
        {
            get
            {
                return "1";
            }
        }
        public static string LevelStepStatus1
        {
            get
            {
                return "در حال بررسی مشخصات اولیه مشتری توسط مسئول امور ارزیابی";
            }
        }
        public static string SmsContent1
        {
            get
            {
                return "یک درخواست جدید در سامانه ثبت گردید";
            }
        }
        public static bool? SmsType1
        {
            get
            {
                return true;
            }
        }

        public static string DestLevelStepIndexButton1
        {
            get
            {
                return null;
            }
        }

        #endregion

    }
}
