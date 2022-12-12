using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ParsKyanCrm.Infrastructure.Consts;

namespace ParsKyanCrm.Infrastructure
{
    public class DapperOperation
    {

        public static async Task<IEnumerable<T>> Run<T>(string strQuery)
        {
            try
            {

                using (IDbConnection con = new SqlConnection(VaribleForName.MainConnectionString))
                {
                    return await con.QueryAsync<T>(strQuery);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
