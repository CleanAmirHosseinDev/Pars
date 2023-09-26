using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Infrastructure
{
    public class Ipconfig
    {

        public async static Task<string> GetUserHostAddress()
        {
            try
            {
                var ips = await System.Net.Dns.GetHostAddressesAsync(System.Net.Dns.GetHostName());

                return VaribleForName.IsDebug == true ? "1::1" : ips[1].MapToIPv4().ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
