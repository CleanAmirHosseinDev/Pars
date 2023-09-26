using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.Users;
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
        private readonly IUserFacad _userFacad;
        public AboutUsController(ILogger<AboutUsController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<AboutUsDto> Get_AboutUs()
        {
            try
            {
                return await _userFacad.GetAboutUsService.Execute();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
