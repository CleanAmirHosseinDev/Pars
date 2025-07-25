﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Application.Services.Users.Commands.UpdatePassUsers;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Common.Enums;
using ParsKyanCrm.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers.api.admin
{

    [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.Users })]
    public class UsersController : BaseController
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserFacad _userFacad;
        public UsersController(ILogger<UsersController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<UserRolesDto>>> Get_Userss([FromBody] RequestUserRolesDto request)
        {
            try
            {
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                return await _userFacad.GetUserssService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.Users_Save })]
        public async Task<UserRolesDto> Get_Users(int? id = null)
        {
            try
            {
                return await _userFacad.GetUsersService.Execute(new RequestUserRolesDto() { UserId = id, IsActive = (byte)TablesGeneralIsActive.Active });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.Users_Save })]
        public async Task<ResultDto<UserRolesDto>> Save_Users([FromBody] UserRolesDto request)
        {
            try
            {
                return await _userFacad.SaveUsersService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.Users_Save })]
        public async Task<ResultDto<IEnumerable<RolesDto>>> Get_Roles_Combo(RequestRolesDto request)
        {
            try
            {
                return (await _userFacad.GetRolessService.Execute(request));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.SetAccessLevels })]
        public async Task<List<NormalJsonClassDto>> GetAccessLevels(int id)
        {
            try
            {
                var q = await _userFacad.GetAccessLevelsService.Execute(id);

                return q;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.SetAccessLevels })]
        public ResultDto Save_AccessLevels([FromBody] UserRolesDto request)
        {
            try
            {
                return _userFacad.SaveAccessLevelsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.Users_Delete })]
        public ResultDto Delete_Users(int id)
        {
            try
            {
                return _userFacad.DeleteUsersService.Execute(id);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.Users_UpdatePass })]
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
