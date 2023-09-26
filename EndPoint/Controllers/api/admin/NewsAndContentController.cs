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
    [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.NewsAndContent })]
    public class NewsAndContentController : BaseController
    {
        private readonly ILogger<NewsAndContentController> _logger;
        private readonly IUserFacad _userFacad;
        public NewsAndContentController(ILogger<NewsAndContentController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<NewsAndContentDto>>> Get_NewsAndContents([FromBody] RequestNewsAndContentDto request)
        {
            try
            {
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                return await _userFacad.GetNewsAndContentsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.NewsAndContent_Save })]
        public async Task<NewsAndContentDto> Get_NewsAndContent(int? id = null)
        {
            try
            {
                return await _userFacad.GetNewsAndContentService.Execute(new RequestNewsAndContentDto() { ContentId = id, IsActive = (byte)TablesGeneralIsActive.Active });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.NewsAndContent_Save })]
        public async Task<ResultDto<NewsAndContentDto>> Save_NewsAndContent([FromBody] NewsAndContentDto request)
        {
            try
            {
                request.UserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserID").Value);                
                return await _userFacad.SaveNewsAndContentService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.NewsAndContent_Delete })]
        public ResultDto Delete_NewsAndContent(int id)
        {
            try
            {
                return _userFacad.DeleteNewsAndContentService.Execute(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
