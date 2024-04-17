using ClosedXML.Excel;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Reports.Queries.TotalNumberCustomersWithoutRegistration
{

    public class TotalNumberCustomersWithoutRegistrationService : ITotalNumberCustomersWithoutRegistrationService
    {

        public async Task<ResultDto<IEnumerable<ResultTotalNumberCustomersWithoutRegistrationDto>>> Execute(RequestTotalNumberCustomersWithoutRegistrationDto request)
        {
            try
            {

                string strQuery = @$"

               select cus.CustomerID,FORMAT(cast(cus.SaveDate as date), 'yyyy/MM/dd', 'fa') as SaveDateStr,cus.CompanyName,cus.AgentName,cus.NationalCode,cus.AgentMobile from Customers as cus
                                                                                               where cus.IsActive = 15 and cus.CustomerID not in( select CustomerID from RequestForRating )
{(!string.IsNullOrEmpty(request.FromDateStr) && !string.IsNullOrEmpty(request.ToDateStr) ? " and cast(cus.SaveDate as date) between  " + request.FromDateStr1 + " and " + request.ToDateStr1 : string.Empty)}               
{(!string.IsNullOrEmpty(request.Search) ? " and ( cus.CompanyName like N'%" + request.Search + "%'" + " or cus.AgentName like N'%" + request.Search + "%' or cus.CustomerID like N'%" + request.Search + "%' or cus.NationalCode like N'%" + request.Search + "%' or cus.AgentMobile like N'%" + request.Search + "%' )" : string.Empty)}
        ORDER BY cus.CustomerID desc

";

                if (!request.IsExcel) strQuery += @$" OFFSET {(request.PageIndex == 1 ? 0 : (request.PageIndex - 1) * request.PageSize)} ROWS
FETCH NEXT {request.PageSize} ROWS ONLY ";                       

                var data = await DapperOperation.Run<ResultTotalNumberCustomersWithoutRegistrationDto>(strQuery);

                return new ResultDto<IEnumerable<ResultTotalNumberCustomersWithoutRegistrationDto>>()
                {
                    Data = data,
                    Rows = data.Count(),
                    IsSuccess = true
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<byte[]> Execute1(RequestTotalNumberCustomersWithoutRegistrationDto request)
        {
            try
            {
                request.IsExcel = true;
                var q = await Execute(request);

                DataTable dt = new DataTable("Grid");
                dt.Columns.AddRange(new DataColumn[7] {
                new DataColumn("ردیف"),
                new DataColumn("کد مشتری"),
                new DataColumn("تاریخ ثبت "),
                new DataColumn("نام شرکت"),
                new DataColumn("نام رابط	 "),
                new DataColumn("شناسه/کد ملی"),
                new DataColumn("موبایل رابط")
            });
                int rowcount = 1;
                foreach (var item in q.Data)
                {
                    dt.Rows.Add(
                          rowcount,
                          item.CustomerID,
                          item.SaveDateStr,
                          item.CompanyName,
                          item.AgentName,
                          item.NationalCode,
                          item.AgentMobile
                        );
                    rowcount++;
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return stream.ToArray();
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
