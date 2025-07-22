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

        public static async Task<IEnumerable<T>> Run<T>(string sql, object parameters = null)
        {
            using (var connection = new SqlConnection(VaribleForName.MainConnectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<T>(sql, parameters);
            }
        }

        public static async Task<T> RunScalar<T>(string query, DynamicParameters parameters)
        {
            using var connection = new SqlConnection(VaribleForName.MainConnectionString);
            return await connection.ExecuteScalarAsync<T>(query, parameters);
        }
    }
}
