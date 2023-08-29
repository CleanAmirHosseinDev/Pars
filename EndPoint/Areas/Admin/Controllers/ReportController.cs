using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Areas.Admin.Controllers
{
    public class ReportController : BaseController
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IUserFacad _userFacad;
        public ReportController(ILogger<ReportController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }
        public IActionResult UserReport()
        {
            return View();
        }
      

        public async Task<ActionResult> GetExcell()
        {
            try
            {
                RequestRequestForRatingDto request = new RequestRequestForRatingDto();               
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["ReciveUser"]))
                    request.ReciveUser =Convert.ToInt32(HttpContext.Request.Query["ReciveUser"]);

                if (!String.IsNullOrEmpty(HttpContext.Request.Query["TypeGroupCompanies"]))
                    request.TypeGroupCompanies = Convert.ToInt32(HttpContext.Request.Query["TypeGroupCompanies"]);

                if (!String.IsNullOrEmpty(HttpContext.Request.Query["FromDateStr"]))
                    request.FromDateStr = HttpContext.Request.Query["FromDateStr"];

                if (!String.IsNullOrEmpty(HttpContext.Request.Query["ToDateStr"]))
                    request.ToDateStr = HttpContext.Request.Query["ToDateStr"];



              
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
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");

                        }
                    }

            }
            catch (Exception ex)
            {
              return RedirectToAction("", "Default");
            }
        }



    }
}
