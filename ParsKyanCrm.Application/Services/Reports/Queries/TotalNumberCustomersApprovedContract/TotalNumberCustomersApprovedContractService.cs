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

                string strQuery = @$"

WITH ResultCTE AS (
    SELECT 
        rfr.RequestNo,
        FORMAT(CAST(rfr.DateOfRequest AS date), 'yyyy/MM/dd', 'fa') AS DateOfRequestStr,
        FORMAT(CAST(rrs.SendTime AS date), 'yyyy/MM/dd', 'fa') AS SendTimeStr,
        cus.CompanyName,
        cus.AgentName,
        cus.NationalCode,
        cus.AgentMobile,
        ISNULL(REPLACE(REPLACE(FORMAT(cfd.FinalPriceContract, 'C0', 'en-US'), '$', ''), '.', ','), 0) AS FinalPriceContract,
        cus.CustomerID
    FROM Customers AS cus
    INNER JOIN RequestForRating AS rfr ON rfr.CustomerID = cus.CustomerID
    CROSS APPLY (
        SELECT TOP 1 SendTime
        FROM RequestReferences AS rrs2
        WHERE rrs2.RequestID = rfr.RequestID AND rrs2.LevelStepSettingIndexID = 7
        ORDER BY rrs2.SendTime DESC
    ) AS rrs
    OUTER APPLY (
        SELECT TOP 1 FinalPriceContract
        FROM ContractAndFinancialDocuments AS cfd2
        WHERE cfd2.IsActive = 15 AND cfd2.RequestID = rfr.RequestID
        ORDER BY cfd2.FinancialID DESC
    ) AS cfd
    WHERE cus.IsActive = 15
    {(!string.IsNullOrEmpty(request.FromDateStr) && !string.IsNullOrEmpty(request.ToDateStr) ? " AND CAST(rrs.SendTime AS date) BETWEEN " + request.FromDateStr1 + " AND " + request.ToDateStr1 : "")}
    {(!string.IsNullOrEmpty(request.Search) ? " AND (cus.CompanyName LIKE N'%" + request.Search + "%'" + " OR cus.AgentName LIKE N'%" + request.Search + "%' OR rfr.RequestNo LIKE N'%" + request.Search + "%' OR cus.NationalCode LIKE N'%" + request.Search + "%' OR cus.AgentMobile LIKE N'%" + request.Search + "%')" : "")}
)

SELECT *
FROM ResultCTE
ORDER BY CustomerID DESC
{(request.IsExcel ? "" : $" OFFSET {(request.PageIndex == 1 ? 0 : (request.PageIndex - 1) * request.PageSize)} ROWS FETCH NEXT {request.PageSize} ROWS ONLY")}
";

                var data = await DapperOperation.Run<ResultTotalNumberCustomersApprovedContractDto>(strQuery);

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
                request.IsExcel = true;
                var q = await Execute(request);

                DataTable dt = new DataTable("Grid");
                dt.Columns.AddRange(new DataColumn[8] {
                new DataColumn("ردیف"),
                new DataColumn("شماره درخواست"),
                new DataColumn("تاریخ ثبت درخواست"),
                new DataColumn("نام شرکت"),
                new DataColumn("نام رابط"),
                new DataColumn("شناسه/کد ملی"),
                new DataColumn("موبایل رابط"),
                new DataColumn("مبلغ قرارداد")
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
                          item.FinalPriceContract
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
