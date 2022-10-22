using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Api.Controllers.api.customer
{
    
    public class SystemSetingController : BaseController
    {
        private readonly ILogger<SystemSetingController> _logger;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public SystemSetingController(ILogger<SystemSetingController> logger, IBasicInfoFacad basicInfoFacad)
        {
            _logger = logger;
            _basicInfoFacad = basicInfoFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<SystemSetingDto>>> Get_SystemSetings([FromBody] RequestSystemSetingDto request)
        {
            try
            {
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                return await _basicInfoFacad.GetSystemSetingsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
