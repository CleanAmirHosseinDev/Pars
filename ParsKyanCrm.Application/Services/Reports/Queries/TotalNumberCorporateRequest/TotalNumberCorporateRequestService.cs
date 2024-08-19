using ClosedXML.Excel;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Reports.Queries.TotalNumberCorporateRequest
{
    public class TotalNumberCorporateRequestService : ITotalNumberCorporateRequestService
    {
        public async Task<ResultDto<IEnumerable<ResultTotalNumberCorporateRequestDto>>> Execute(RequestTotalNumberCorporateRequestDto request)
        {
            try
            {

                string strQuery = @$"
select rfr.RequestNo,FORMAT(cast(rfr.DateOfRequest as date), 'yyyy/MM/dd', 'fa') as DateOfRequestStr,cus.CompanyName,cus.AgentName,cus.NationalCode,cus.AgentMobile,(select top 1 ISNULL(REPLACE(REPLACE(FORMAT(ContractAndFinancialDocuments.FinalPriceContract, 'C0', 'en-US'), '$', ''), '.', ','), 0) from ContractAndFinancialDocuments where ContractAndFinancialDocuments.IsActive = 15 and ContractAndFinancialDocuments.RequestID = rfr.RequestID order by ContractAndFinancialDocuments.FinancialID desc) as FinalPriceContract from Customers as cus
inner join RequestForRating as rfr on rfr.CustomerID = cus.CustomerID
where cus.IsActive = 15 and cus.IsProfileComplete = 1 and rfr.KindOfRequest = 254
               
{(!string.IsNullOrEmpty(request.FromDateStr) && !string.IsNullOrEmpty(request.ToDateStr) ? " and cast(rfr.DateOfRequest as date) between  " + request.FromDateStr1 + " and " + request.ToDateStr1 : string.Empty)}               
{(!string.IsNullOrEmpty(request.Search) ? " and ( cus.CompanyName like N'%" + request.Search + "%'" + " or cus.AgentName like N'%" + request.Search + "%' or rfr.RequestNo like N'%" + request.Search + "%' or cus.NationalCode like N'%" + request.Search + "%' or cus.AgentMobile like N'%" + request.Search + "%' )" : string.Empty)}
        ORDER BY cus.CustomerID desc

";
                if (!request.IsExcel) strQuery += @$" OFFSET {(request.PageIndex == 1 ? 0 : (request.PageIndex - 1) * request.PageSize)} ROWS
FETCH NEXT { request.PageSize} ROWS ONLY";

                var data = await DapperOperation.Run<ResultTotalNumberCorporateRequestDto>(strQuery);

                return new ResultDto<IEnumerable<ResultTotalNumberCorporateRequestDto>>()
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

        public async Task<byte[]> Execute1(RequestTotalNumberCorporateRequestDto request)
        {
            try
            {
                request.IsExcel = true;
                var q = await Execute(request);

                DataTable dt = new DataTable("Grid");
                dt.Columns.AddRange(new DataColumn[7] {
                new DataColumn("ردیف"),
                new DataColumn("شماره درخواست"),
                new DataColumn("تاریخ ثبت درخواست "),
                new DataColumn("نام شرکت"),
                new DataColumn("نام رابط"),
                new DataColumn("شناسه/کد ملی"),
                new DataColumn("موبایل رابط	")
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
