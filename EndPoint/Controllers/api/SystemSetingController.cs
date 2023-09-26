using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers.api
{
   
    public class SystemSetingController : BaseController
    {
        private readonly ILogger<ManagerOfParsKyanController> _logger;
        private readonly IUserFacad _userFacad;
        public SystemSetingController(ILogger<ManagerOfParsKyanController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<SystemSetingDto>>> Get_SystemSetings([FromBody] RequestSystemSetingDto request)
        {
            try
            {
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                return await _userFacad.GetSystemSetingsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
