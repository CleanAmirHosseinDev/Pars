using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Application.Services.Securitys.Queries.AutenticatedCode;
using ParsKyanCrm.Application.Services.Securitys.Queries.Logins;
using ParsKyanCrm.Common.Dto;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ParsKyanCrm.Application.Services.WebService.CaptchaService;

namespace EndPoint.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecuritysController : ControllerBase
    {
        private readonly ILogger<SecuritysController> _logger;
        private readonly ISecurityFacad _securityFacad;
        private readonly IBasicInfoFacad _basicInfoFacad;

        public SecuritysController(ILogger<SecuritysController> logger, ISecurityFacad securityFacad, IBasicInfoFacad basicInfoFacad)
        {
            _basicInfoFacad = basicInfoFacad;
            _logger = logger;
            _securityFacad = securityFacad;
        }

        [HttpPost]
        [Route("[action]")]
        [CaptchaCheck]
        public async Task<ResultDto<ResultLoginDto>> Login(RequestLoginDto request)
        {
            var error = "";
            if (ModelState.IsValid)
            {
                try
                {
                    return await _securityFacad.LoginsService.Execute(request);
                }
                catch (Exception ex)
                {
                    error = "خطا در اجرای درخواست";
                }
            }
            else
            {
                error = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

            }
            return new ResultDto<ResultLoginDto>
            {
                Data = null,
                IsSuccess = false,
                Message = error
            };


        }

        [HttpPost]
        [Route("[action]")]
        [CaptchaCheck]
        public async Task<ResultDto<ResultLoginDto>> AutenticatedCode(RequestAutenticatedCodeDto request)
        {

            ModelState.Remove("CaptchaCodes");
            var error = "";
            if (ModelState.IsValid)
            {
                try
                {
                    return await _securityFacad.AutenticatedCodeService.Execute(request);
                }
                catch (Exception ex)
                {
                    error = "خطا در اجرای درخواست";
                }
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                foreach (var item in errors)
                {
                    error += item + " ";
                }

            }
            return new ResultDto<ResultLoginDto>
            {
                Data = null,
                IsSuccess = false,
                Message = error
            };
        }

        [HttpPost]
        [Route("[action]")]
        public ResultDto SignOutForExitToLogin(LoginLogDto request)
        {
            try
            {
                _basicInfoFacad.InsertLoginLogService.Execute(request, false);
                return new ResultDto { IsSuccess = true };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
