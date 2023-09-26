
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParsKyanCrm.Common
{

    public static class Utility
    {        


        public static bool CheckMobile(string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str)) return false;

                if (str.Substring(0, 2) != "09") return false;

                if (str.Length != 11) return false;

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool CheckTel(string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str)) return false;

                if (str.Substring(0, 2) == "09") return false;

                if (str.Length != 11) return false;

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
