using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers.api
{
    public class AboutUsController : BaseController
    {

        private readonly ILogger<AboutUsController> _logger;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public AboutUsController(ILogger<AboutUsController> logger, IBasicInfoFacad basicInfoFacad)
        {
            _logger = logger;
            _basicInfoFacad = basicInfoFacad;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<AboutUsDto> Get_AboutUs()
        {
            try
            {
                return await _basicInfoFacad.GetAboutUsService.Execute();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
