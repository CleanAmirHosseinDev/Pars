using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Infrastructure.Consts
{
    public class VaribleForName
    {

        //موقع ورژن زدن این عدد باید یکی اضافه شود
        public static string SCVersion
        {
            get
            {
                return "v=" + "1";
            }
        }

        public static bool IsDebug
        {
            get
            {
                return true;
            }
        }

        public static string MainConnectionString
        {
            get
            {
                return IsDebug ? "data source=77.238.123.197;initial catalog=ParsKyanCrmDB;user id=vam30;password=10155;MultipleActiveResultSets=True" : "data source=172.16.19.9;initial catalog=ParsKyanCrmDB;user id=ParsKyanCrm;password=ParsKyanCrm@10155;MultipleActiveResultSets=True";
            }
        }

        public static string Secret
        {
            get
            {
                return "3315169990345098_543267319_12345678900987654321_Mobin2352@gmail.com_Mobin2352@outlook.com_Ab123456Ba654321_ParsKyanCrm_Mohandes_Sarfrazi_Mobin_Morid_Ahmadi.!@#$%^&*()09876543211234567890";
            }
        }

        public static string No_Photo
        {
            get
            {
                return "no-photo.png";
            }
        }
    }
}
