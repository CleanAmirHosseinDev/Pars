
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Common
{
    public static class Utility
    {

        public static string GetUserHostAddress()
        {
            try
            {
                Microsoft.AspNetCore.Http.HttpContext context;
                //return context.Connection.RemoteIpAddress?.ToString();
                return "دینه";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
