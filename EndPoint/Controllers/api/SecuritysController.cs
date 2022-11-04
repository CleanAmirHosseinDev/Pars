using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Application.Services.Securitys.Queries.AutenticatedCode;
using ParsKyanCrm.Application.Services.Securitys.Queries.Logins;
using ParsKyanCrm.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecuritysController : ControllerBase
    {
        private readonly ILogger<SecuritysController> _logger;
        private readonly ISecurityFacad _securityFacad;
        public SecuritysController(ILogger<SecuritysController> logger, ISecurityFacad securityFacad)
        {
            _logger = logger;
            _securityFacad = securityFacad;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResultDto<ResultLoginDto>> Login([FromBody] RequestLoginDto request)
        {
            try
            {
                return await _securityFacad.LoginsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResultDto<ResultLoginDto>> AutenticatedCode([FromBody] RequestAutenticatedCodeDto request)
        {
            try
            {
                return await _securityFacad.AutenticatedCodeService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
