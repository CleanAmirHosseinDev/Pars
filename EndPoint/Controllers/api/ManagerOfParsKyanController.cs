using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers.api
{
    public class ManagerOfParsKyanController : BaseController
    {

        private readonly ILogger<ManagerOfParsKyanController> _logger;
        private readonly IUserFacad _userFacad;
        public ManagerOfParsKyanController(ILogger<ManagerOfParsKyanController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<ManagerOfParsKyanDto>>> Get_ManagerOfParsKyans([FromBody] RequestManagerOfParsKyanDto request)
        {
            try
            {
                return await _userFacad.GetManagerOfParsKyansService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        public async Task<ManagerOfParsKyanDto> Get_ManagerOfParsKyan(int? id = null)
        {
            try
            {
                return await _userFacad.GetManagerOfParsKyanService.Execute(new RequestManagerOfParsKyanDto() { ManagersId = id });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
