using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Common.Enums;
using ParsKyanCrm.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers.api.admin
{
    [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.AboutUs })]
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

        [HttpPost]
        [Route("[action]")]        
        public async Task<ResultDto<AboutUsDto>> Save_AboutUs([FromBody] AboutUsDto request)
        {
            try
            {
                request.Userid = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserID").Value);
                return await _userFacad.SaveAboutUsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
