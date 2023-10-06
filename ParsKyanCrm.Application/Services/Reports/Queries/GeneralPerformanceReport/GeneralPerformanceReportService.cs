using ClosedXML.Excel;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Reports.Queries.GeneralPerformanceReport
{

    public class GeneralPerformanceReportService : IGeneralPerformanceReportService
    {

        private readonly IUserFacad _userFacad;

        public GeneralPerformanceReportService(IUserFacad userFacad)
        {
            _userFacad = userFacad;
        }

        public async Task<byte[]> Execute(RequestRequestForRatingDto request)
        {
            try
            {
                request.CustomerId = null;
                request.IsExcelReport = true;
                request.ReciveUser = (request.ReciveUser.HasValue ? (request.ReciveUser.Value == 0 ? null : request.ReciveUser) : null);
                request.TypeGroupCompanies = (request.TypeGroupCompanies.HasValue ? (request.TypeGroupCompanies.Value == 0 ? null : request.TypeGroupCompanies) : null);
                //request.LoginName = User.Claims.FirstOrDefault(c => c.Type == "LoginName").Value;
                // request.UserID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserID").Value);
                var result = await _userFacad.GetRequestForRatingsService.Execute(request);

                DataTable dt = new DataTable("Grid");
                dt.Columns.AddRange(new DataColumn[9] {
                new DataColumn("ردیف"),
                new DataColumn("شماره درخواست"),
                new DataColumn("تاریخ ثبت درخواست "),
                new DataColumn("نام شرکت"),
                new DataColumn("نام رابط	 "),
                new DataColumn("شناسه/کد ملی"),
                new DataColumn("موبایل رابط	"),
                new DataColumn("ارزیاب"),
                new DataColumn("آخرین وضعیت درخواست")
            });
                int rowcount = 0;
                foreach (var item in result.Data)
                {
                    rowcount++;
                    dt.Rows.Add(
                          rowcount++,
                          item.RequestNo,
                          item.DateOfRequestStr,
                          item.CompanyName,
                          item.AgentName,
                          item.NationalCode,
                          item.AgentMobile,
                          item.EvaluationExpert,
                          item.DestLevelStepIndexButton
                        );
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
