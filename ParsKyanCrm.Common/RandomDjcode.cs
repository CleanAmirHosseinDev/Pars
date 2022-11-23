using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Common
{
    public class RandomDjcode
    { 

        public static string randnu(int n)
        {
            try
            {
                /*string strRes = string.Empty;
                Random rnd = new Random();

                strRes += DateTime.Now.Millisecond.ToString() + (rnd.Next(1, 1000) + rnd.Next(1, 1000)) + DateTime.Now.Ticks.ToString();


                return new string(Enumerable.Repeat(strRes, n).Select(s => s[rnd.Next(s.Length)]).ToArray());*/
                string allowedChars = "123456789";
                char[] chars = new char[n];
                Random rd = new Random();

                for (int i = 0; i < n; i++)
                {
                    chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
                }
                return new string(chars);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
