using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Infrastructure
{
    public class Ado_NetOperation
    {

        public static string GetSortOrderWithPageing(PageingParamerDto request, string strObjAdo = "s", string id = "Id")
        {

            if (string.IsNullOrEmpty(request.SortOrder)) return string.Empty;
            var qSplit = request.SortOrder.Split("_");

            string strResSortOrder = (qSplit.Length > 0 ? " order by " + strObjAdo + "." + qSplit[0] + " " + (qSplit[1] == "A" ? "asc" : "desc") : " order by " + strObjAdo + "." + id + " desc ");

            string strRes = @$"{(request.PageIndex == 0 && request.PageSize == 0 ? strResSortOrder : @$"

{strResSortOrder}

OFFSET {(request.PageIndex == 1 ? 0 : (request.PageIndex - 1) * request.PageSize)} ROWS
FETCH NEXT {request.PageSize} ROWS ONLY

")}";

            return strRes;

        }

        public static SqlConnectionStringBuilder InstanceSqlConnectionStringBuilder()
        {
            try
            {
                return new SqlConnectionStringBuilder(VaribleForName.MainConnectionString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SqlUpdate(string table, Dictionary<string, object> values, string where)
        {
            try
            {

                var equals = new List<string>();
                var parameters = new List<SqlParameter>();

                var i = 0;

                foreach (var item in values)
                {

                    var pn = "@sp" + i.ToString();

                    equals.Add(string.Format("{0}={1}", item.Key, pn));

                    parameters.Add(new SqlParameter(pn, item.Value == null ? DBNull.Value : item.Value));

                    i++;
                }

                ExecuteSql(string.Format("update {0} set {1} where {2}", table, string.Join(", ", equals.ToArray()), where), values: parameters.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ExecuteSql(string sql, SqlConnectionStringBuilder sqlConnectionStringBuilder_Param = null, SqlParameter[] values = null)
        {
            try
            {

                var sqlConnectionStringBuilder = sqlConnectionStringBuilder_Param == null ? InstanceSqlConnectionStringBuilder() : sqlConnectionStringBuilder_Param;

                using (var sqlConnection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
                {
                    sqlConnection.Open();
                    using (var sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.Connection = sqlConnection;

                        if (values != null)
                        {
                            sqlCommand.Parameters.AddRange(values);
                        }

                        sqlCommand.CommandText = sql;
                        sqlCommand.ExecuteNonQuery();
                    }
                    sqlConnection.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetAll_Table(string tblName, string columns = "*", string condition = null)
        {
            try
            {
                return Select(GenerateSelectQuerySql(tblName, columns, condition));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GenerateSelectQuerySql(string tblName, string columns = "*", string condition = null)
        {
            try
            {
                string execWhere = !string.IsNullOrEmpty(condition) ? " where " : string.Empty;
                return string.Format("SELECT {0} FROM {1} {2} {3} ", columns, tblName, execWhere, condition);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable Select(string sqlQuery)
        {
            try
            {

                var sqlConnectionStringBuilder = InstanceSqlConnectionStringBuilder();

                using (var sqlConnection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
                {
                    using (var sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = sqlQuery;
                        using (SqlDataAdapter da = new SqlDataAdapter(sqlCommand))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sqlConnection.Open();
                                da.Fill(dt);
                                sqlConnection.Close();
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T GetItem<T>(DataRow dr)
        {
            try
            {
                Type temp = typeof(T);
                T obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in dr.Table.Columns)
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        if (pro.Name == column.ColumnName)
                            pro.SetValue(obj, (dr[column.ColumnName] == DBNull.Value ? null : dr[column.ColumnName]), null);
                        else continue;
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            try
            {
                List<T> data = new List<T>();
                if (dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        T item = GetItem<T>(row);
                        data.Add(item);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SqlDelete(string table, string where = "")
        {
            try
            {
                string execWhere = !string.IsNullOrEmpty(where) ? " where " : string.Empty;

                ExecuteSql(string.Format("DELETE FROM {0} {1} {2} ", table, execWhere, where));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SqlInsert(string table, DataTable dataTable)
        {
            try
            {

                string columns = string.Join(","
    , dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName));
                string values = string.Join(","
                    , dataTable.Columns.Cast<DataColumn>().Select(c => string.Format("@{0}", c.ColumnName)));
                String sqlCommandInsert = string.Format("INSERT INTO " + table + "({0}) VALUES ({1})", columns, values);

                var sqlConnectionStringBuilder = InstanceSqlConnectionStringBuilder();

                using (var con = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
                using (var cmd = new SqlCommand(sqlCommandInsert, con))
                {
                    con.Open();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        cmd.Parameters.Clear();
                        foreach (DataColumn col in dataTable.Columns)
                            cmd.Parameters.AddWithValue("@" + col.ColumnName, row[col]);
                        int inserted = cmd.ExecuteNonQuery();
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
