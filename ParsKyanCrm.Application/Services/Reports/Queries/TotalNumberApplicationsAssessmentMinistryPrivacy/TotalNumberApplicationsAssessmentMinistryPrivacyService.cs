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

namespace ParsKyanCrm.Application.Services.Reports.Queries.TotalNumberApplicationsAssessmentMinistryPrivacy
{

    public class TotalNumberApplicationsAssessmentMinistryPrivacyService : ITotalNumberApplicationsAssessmentMinistryPrivacyService
    {
        public async Task<ResultDto<IEnumerable<ResultTotalNumberApplicationsAssessmentMinistryPrivacyDto>>> Execute(RequestTotalNumberApplicationsAssessmentMinistryPrivacyDto request)
        {
            try
            {

                string strQuery = @$"
SELECT 
    rfr.RequestNo,
    FORMAT(CAST(rfr.DateOfRequest AS date), 'yyyy/MM/dd', 'fa') AS DateOfRequestStr,
    cus.CompanyName,
    cus.AgentName,
    cus.NationalCode,
    cus.AgentMobile,
    ISNULL((
        SELECT TOP 1 
            REPLACE(REPLACE(FORMAT(cfd.FinalPriceContract, 'C0', 'en-US'), '$', ''), '.', ',')
        FROM ContractAndFinancialDocuments AS cfd
        WHERE cfd.IsActive = 15 AND cfd.RequestID = rfr.RequestID
        ORDER BY cfd.FinancialID DESC
    ), '0') AS FinalPriceContract
FROM Customers AS cus
INNER JOIN RequestForRating AS rfr ON rfr.CustomerID = cus.CustomerID
WHERE cus.IsActive = 15 
  AND cus.IsProfileComplete = 1 
  AND rfr.KindOfRequest = 66
{(!string.IsNullOrEmpty(request.FromDateStr) && !string.IsNullOrEmpty(request.ToDateStr) ? " AND CAST(rfr.DateOfRequest AS date) BETWEEN " + request.FromDateStr1 + " AND " + request.ToDateStr1 : "")}
{(!string.IsNullOrEmpty(request.Search) ? " AND ( cus.CompanyName LIKE N'%" + request.Search + "%'" + " OR cus.AgentName LIKE N'%" + request.Search + "%' OR rfr.RequestNo LIKE N'%" + request.Search + "%' OR cus.NationalCode LIKE N'%" + request.Search + "%' OR cus.AgentMobile LIKE N'%" + request.Search + "%')" : "")}
ORDER BY cus.CustomerID DESC
";

                if (!request.IsExcel) strQuery += @$" OFFSET {(request.PageIndex == 1 ? 0 : (request.PageIndex - 1) * request.PageSize)} ROWS
FETCH NEXT { request.PageSize} ROWS ONLY";

                var data = await DapperOperation.Run<ResultTotalNumberApplicationsAssessmentMinistryPrivacyDto>(strQuery);

                return new ResultDto<IEnumerable<ResultTotalNumberApplicationsAssessmentMinistryPrivacyDto>>()
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

        public async Task<byte[]> Execute1(RequestTotalNumberApplicationsAssessmentMinistryPrivacyDto request)
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
