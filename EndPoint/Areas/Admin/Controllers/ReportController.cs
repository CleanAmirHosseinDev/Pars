using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Application.Services.Reports.Queries.TotalNumberCustomersApprovedContract;
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
        private readonly IReportFacad _reportFacad;
        public ReportController(ILogger<ReportController> logger, IReportFacad reportFacad)
        {
            _logger = logger;
            _reportFacad = reportFacad;
        }

        #region گزارش کلی عملکرد

        public IActionResult UserReport()
        {
            return View();
        }

        /// <summary>
        /// خواهشا داخل کنترلر کد نویسی نکنید فقط داخل سرویس
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> GetExcell(RequestRequestForRatingDto request)
        {
            try
            {
                return File(await _reportFacad.GeneralPerformanceReportService.Execute(request), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
            }
            catch (Exception ex)
            {
                return RedirectToAction("", "Default");
            }
        }

        #endregion

        #region باکس های صفحه اصلی فقط تعداد

        [HttpPost]
        public async Task<IActionResult> IndexBoxAdmin()
        {
            try
            {
                return Json(await _reportFacad.IndexBoxAdminService.Execute());
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        #endregion

        #region تعداد کل مشتریان که قرارداد را تایید کردند

        public IActionResult TotalNumberCustomersApprovedContract()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetTotalNumberCustomersApprovedContract([FromBody] RequestTotalNumberCustomersApprovedContractDto request)
        {
            try
            {
                return Json(await _reportFacad.TotalNumberCustomersApprovedContractService.Execute(request));
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        [HttpPost]
        public async Task<IActionResult> GetTotalNumberCustomersApprovedContract1(RequestTotalNumberCustomersApprovedContractDto request)
        {
            try
            {
                return File(await _reportFacad.TotalNumberCustomersApprovedContractService.Execute1(request), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        #endregion

        #region تعداد کل مشتریان بدون ثبت درخواست


        #endregion

        #region تعداد کل درخواست ارزیابی وزارت صمت


        #endregion


    }
}
