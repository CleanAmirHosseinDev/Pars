using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Application.Services.Reports.Queries.CustomerDataFormReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Areas.SuperVisor.Controllers
{
    public class CorporateController : BaseController
    {
        private readonly IReportFacad _reportFacad;
        public CorporateController(IReportFacad reportFacad)
        {
            _reportFacad = reportFacad;
        }

        public IActionResult Index(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }
        public IActionResult ShowScore(int? id = null)
        {
            ViewBag.id = id;
            return View();
        }

        public async Task<ActionResult> Excel_CustomerDataFormReport(int? id = null)
        {
            try
            {
                return File(await _reportFacad.CustomerDataFormReportService.ToExcel(new RequestCustomerDataFormReportDto() { RequestId = id }), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomerReport.xlsx");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
