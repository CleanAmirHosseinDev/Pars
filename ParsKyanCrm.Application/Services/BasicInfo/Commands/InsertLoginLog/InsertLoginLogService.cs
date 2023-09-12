using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.InsertLoginLog
{

    public class InsertLoginLogService : IInsertLoginLogService
    {
        public void Execute(LoginLogDto request, bool isLogin = true)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    dt.Columns.Add("Userid", typeof(int));
                    dt.Columns.Add("LoginDate", typeof(DateTime));
                    dt.Columns.Add("Ip", typeof(string));
                    dt.Columns.Add("SignOutDate", typeof(DateTime));
                    dt.Columns.Add("AreaName", typeof(string));
                    DataRow _ravi = dt.NewRow();

                    _ravi["Userid"] = request.Userid;
                    _ravi["Ip"] = Utility.GetUserHostAddress();

                    if (isLogin)
                    {
                        _ravi["LoginDate"] = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);
                        _ravi["SignOutDate"] = DBNull.Value;
                    }
                    else
                    {
                        _ravi["LoginDate"] = DBNull.Value;
                        _ravi["SignOutDate"] = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);
                    }

                    _ravi["AreaName"] = request.AreaName;


                    dt.Rows.Add(_ravi);
                    Ado_NetOperation.SqlInsert("LoginLog", dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
