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

namespace ParsKyanCrm.Application.Services.Reports.Queries.NumberCodedFiles
{

    public class NumberCodedFilesService : INumberCodedFilesService
    {
        public async Task<ResultDto<IEnumerable<ResultNumberCodedFilesDto>>> Execute(RequestNumberCodedFilesDto request)
        {
            try
            {

                string strQuery = @$"
                 select rfr.RequestNo,FORMAT(cast(rfr.DateOfRequest as date), 'yyyy/MM/dd', 'fa') as DateOfRequestStr,cus.CompanyName,cus.AgentName,cus.NationalCode,cus.AgentMobile, FORMAT(cast(rfr.CodalDate as date), 'yyyy/MM/dd', 'fa')CodalDate,
	   rfr.CodalNumber from RequestForRating as rfr
inner join Customers as cus on rfr.CustomerID = cus.CustomerID
where cus.IsActive = 15 and cus.IsProfileComplete = 1 and rfr.IsFinished = 1
               
{(!string.IsNullOrEmpty(request.FromDateStr) && !string.IsNullOrEmpty(request.ToDateStr) ? " and cast(rfr.CodalDate as date) between  " + request.FromDateStr1 + " and " + request.ToDateStr1 : string.Empty)}               
{(!string.IsNullOrEmpty(request.Search) ? " and ( cus.CompanyName like N'%" + request.Search + "%'" + " or cus.AgentName like N'%" + request.Search + "%' or rfr.RequestNo like N'%" + request.Search + "%' or cus.NationalCode like N'%" + request.Search + "%' or cus.AgentMobile like N'%" + request.Search + "%' )" : string.Empty)}
        ORDER BY cus.CustomerID desc

";
                if (!request.IsExcel) strQuery += @$" OFFSET {(request.PageIndex == 1 ? 0 : (request.PageIndex - 1) * request.PageSize)} ROWS
FETCH NEXT { request.PageSize} ROWS ONLY";

                var data = await DapperOperation.Run<ResultNumberCodedFilesDto>(strQuery);

                return new ResultDto<IEnumerable<ResultNumberCodedFilesDto>>()
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

        public async Task<byte[]> Execute1(RequestNumberCodedFilesDto request)
        {
            try
            {
                request.IsExcel = true;
                var q = await Execute(request);

                DataTable dt = new DataTable("Grid");
                dt.Columns.AddRange(new DataColumn[9] {
                new DataColumn("ردیف"),
                new DataColumn("شماره درخواست"),
                new DataColumn("تاریخ ثبت درخواست "),
                new DataColumn("نام شرکت"),
                new DataColumn("نام رابط"),
                new DataColumn("شناسه/کد ملی"),
                new DataColumn("موبایل رابط	"),
                new DataColumn("تاریخ کدال	"),
                new DataColumn("کد رهگیری	")
            });
                int rowcount = 1;
                foreach (var item in q.Data)
                {
                    dt.Rows.Add(
                          rowcount,
                          item.RequestNo,
                          item.DateOfRequestStr,
                          item.CompanyName,
                          item.AgentName,
                          item.NationalCode,
                          item.AgentMobile,
                          item.CodalDate,
                          item.CodalNumber

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
