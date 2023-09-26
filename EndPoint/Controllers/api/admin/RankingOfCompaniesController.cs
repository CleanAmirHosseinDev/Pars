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
    [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.RankingOfCompanies })]
    public class RankingOfCompaniesController : BaseController
    {
        private readonly ILogger<RankingOfCompaniesController> _logger;
        private readonly IUserFacad _userFacad;
        public RankingOfCompaniesController(ILogger<RankingOfCompaniesController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<RankingOfCompaniesDto>>> Get_RankingOfCompaniess([FromBody] RequestRankingOfCompaniesDto request)
        {
            try
            {
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                return await _userFacad.GetRankingOfCompaniessService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.RankingOfCompanies_Save })]
        public async Task<RankingOfCompaniesDto> Get_RankingOfCompanies(int? id = null)
        {
            try
            {
                return await _userFacad.GetRankingOfCompaniesService.Execute(new RequestRankingOfCompaniesDto() { RankingId = id, IsActive = (byte)TablesGeneralIsActive.Active });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.RankingOfCompanies_Save })]
        public async Task<ResultDto<RankingOfCompaniesDto>> Save_RankingOfCompanies([FromForm] RankingOfCompaniesDto request)
        {
            try
            {
                request.UserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserID").Value);
                return await _userFacad.SaveRankingOfCompaniesService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.RankingOfCompanies_Delete })]
        public ResultDto Delete_RankingOfCompanies(int id)
        {
            try
            {
                return  _userFacad.DeleteRankingOfCompaniesService.Execute(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
