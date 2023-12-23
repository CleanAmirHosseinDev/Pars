using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Application.Services.Reports.Queries.NumberCodedFiles;
using ParsKyanCrm.Application.Services.Reports.Queries.TotalNumberApplicationsAssessmentMinistryPrivacy;
using ParsKyanCrm.Application.Services.Reports.Queries.TotalNumberCustomersApprovedContract;
using ParsKyanCrm.Application.Services.Reports.Queries.TotalNumberCustomersWithoutRegistration;
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
        
        public async Task<ActionResult> GetTotalNumberCustomersApprovedContract1(RequestTotalNumberCustomersApprovedContractDto request)
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



        public IActionResult TotalNumberCustomersWithoutRegistration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetTotalNumberCustomersWithoutRegistration([FromBody] RequestTotalNumberCustomersWithoutRegistrationDto request)
        {
            try
            {
                return Json(await _reportFacad.TotalNumberCustomersWithoutRegistrationService.Execute(request));
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        
        public async Task<ActionResult> GetTotalNumberCustomersWithoutRegistration1(RequestTotalNumberCustomersWithoutRegistrationDto request)
        {
            try
            {
                return File(await _reportFacad.TotalNumberCustomersWithoutRegistrationService.Execute1(request), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        #endregion

        #region تعداد کل درخواست ارزیابی وزارت صمت

        public IActionResult TotalNumberApplicationsAssessmentMinistryPrivacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetTotalNumberApplicationsAssessmentMinistryPrivacy([FromBody] RequestTotalNumberApplicationsAssessmentMinistryPrivacyDto request)
        {
            try
            {
                return Json(await _reportFacad.TotalNumberApplicationsAssessmentMinistryPrivacyService.Execute(request));
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<ActionResult> GetTotalNumberApplicationsAssessmentMinistryPrivacy1(RequestTotalNumberApplicationsAssessmentMinistryPrivacyDto request)
        {
            try
            {
                return File(await _reportFacad.TotalNumberApplicationsAssessmentMinistryPrivacyService.Execute1(request), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        #endregion

        #region تعداد پرونده های کدال شده

        public IActionResult NumberCodedFiles()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetNumberCodedFiles([FromBody] RequestNumberCodedFilesDto request)
        {
            try
            {
                return Json(await _reportFacad.NumberCodedFilesService.Execute(request));
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<ActionResult> GetNumberCodedFiles1(RequestNumberCodedFilesDto request)
        {
            try
            {
                return File(await _reportFacad.NumberCodedFilesService.Execute1(request), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        #endregion

    }
}