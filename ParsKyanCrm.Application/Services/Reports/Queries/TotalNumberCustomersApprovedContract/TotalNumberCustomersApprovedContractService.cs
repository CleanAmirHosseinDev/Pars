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

                select rfr.RequestNo,FORMAT(cast(rfr.DateOfRequest as date), 'yyyy/MM/dd', 'fa') as DateOfRequestStr,
				FORMAT(cast(rrs.SendTime  as date), 'yyyy/MM/dd', 'fa') as SendTimeStr,cus.CompanyName,cus.AgentName,cus.NationalCode,cus.AgentMobile,(select top 1 ISNULL(REPLACE(REPLACE(FORMAT(ContractAndFinancialDocuments.FinalPriceContract, 'C0', 'en-US'), '$', ''), '.', ','), 0) from ContractAndFinancialDocuments where ContractAndFinancialDocuments.IsActive = 15 and ContractAndFinancialDocuments.RequestID = rfr.RequestID order by ContractAndFinancialDocuments.FinancialID desc) as FinalPriceContract from Customers as cus
                inner join RequestForRating as rfr on rfr.CustomerID = cus.CustomerID
                inner join RequestReferences as rrs on rfr.RequestID=rrs.Requestid and rrs.LevelStepSettingIndexID=7
                where cus.IsActive = 15 
                {(!string.IsNullOrEmpty(request.FromDateStr) && !string.IsNullOrEmpty(request.ToDateStr) ? " and cast(rrs.SendTime as date) between  " + request.FromDateStr1 + " and " + request.ToDateStr1 : string.Empty)}               
                {(!string.IsNullOrEmpty(request.Search) ? " and ( cus.CompanyName like N'%" + request.Search + "%'" + " or cus.AgentName like N'%" + request.Search + "%' or rfr.RequestNo like N'%" + request.Search + "%' or cus.NationalCode like N'%" + request.Search + "%' or cus.AgentMobile like N'%" + request.Search + "%' )" : string.Empty)}
                ORDER BY cus.CustomerID desc

";
                if (!request.IsExcel) strQuery += @$" OFFSET {(request.PageIndex == 1 ? 0 : (request.PageIndex - 1) * request.PageSize)} ROWS
FETCH NEXT { request.PageSize} ROWS ONLY";

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
