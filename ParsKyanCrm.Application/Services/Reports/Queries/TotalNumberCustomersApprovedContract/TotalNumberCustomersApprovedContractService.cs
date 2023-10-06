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

namespace ParsKyanCrm.Application.Services.Reports.Queries.TotalNumberCustomersApprovedContract
{

    public class TotalNumberCustomersApprovedContractService : ITotalNumberCustomersApprovedContractService
    {

        public async Task<ResultDto<IEnumerable<ResultTotalNumberCustomersApprovedContractDto>>> Execute(RequestTotalNumberCustomersApprovedContractDto request)
        {
            try
            {

                var data = await DapperOperation.Run<ResultTotalNumberCustomersApprovedContractDto>(@$"

                select rfr.RequestNo,FORMAT(GETDATE(), 'yyyy/MM/dd', 'fa') as DateOfRequestStr,cus.CompanyName,cus.AgentName,cus.NationalCode,cus.AgentMobile from Customers as cus
inner join RequestForRating as rfr on rfr.CustomerID = cus.CustomerID
where cus.IsActive = 15 and cus.IsProfileComplete = 1 and rfr.RequestID in (select ContractAndFinancialDocuments.RequestID from ContractAndFinancialDocuments where ContractAndFinancialDocuments.IsActive = 15)
{(!string.IsNullOrEmpty(request.FromDateStr) && !string.IsNullOrEmpty(request.ToDateStr) ? " and cast(rfr.DateOfRequest as date) between  " + request.FromDateStr1 + " and " + request.ToDateStr1 : string.Empty)}               
{(!string.IsNullOrEmpty(request.Search) ? " and ( cus.CompanyName like N'%" + request.Search + "%'" + " or cus.AgentName like N'%" + request.Search + "%' or rfr.RequestNo like N'%" + request.Search + "%' or cus.NationalCode like N'%" + request.Search + "%' or cus.AgentMobile like N'%" + request.Search + "%' )" : string.Empty)}
        ORDER BY cus.CustomerID desc

OFFSET {(request.PageIndex == 1 ? 0 : (request.PageIndex - 1) * request.PageSize)} ROWS
FETCH NEXT {request.PageSize} ROWS ONLY

");

                return new ResultDto<IEnumerable<ResultTotalNumberCustomersApprovedContractDto>>()
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

        public async Task<byte[]> Execute1(RequestTotalNumberCustomersApprovedContractDto request)
        {
            try
            {
                var q = await Execute(request);

                DataTable dt = new DataTable("Grid");
                dt.Columns.AddRange(new DataColumn[7] {
                new DataColumn("ردیف"),
                new DataColumn("شماره درخواست"),
                new DataColumn("تاریخ ثبت درخواست "),
                new DataColumn("نام شرکت"),
                new DataColumn("نام رابط	 "),
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
