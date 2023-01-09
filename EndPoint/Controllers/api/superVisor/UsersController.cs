using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Application.Services.Users.Commands.UpdatePassUsers;
using ParsKyanCrm.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers.api.superVisor
{    
    public class UsersController : BaseController
    {

        private readonly ILogger<UsersController> _logger;
        private readonly IUserFacad _userFacad;
        public UsersController(ILogger<UsersController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        [HttpPost]
        [Route("[action]")]        
        public async Task<ResultDto> UpdatePass_Users([FromBody] RequestUpdatePassUsersDto request)
        {
            try
            {
                request.UserID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserID").Value);
                return await _userFacad.UpdatePassUsersService.Execute(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
