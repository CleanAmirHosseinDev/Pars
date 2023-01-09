using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.BasicInfo;
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
    [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.LicensesAndHonors })]
    public class LicensesAndHonorsController : BaseController
    {
        private readonly ILogger<LicensesAndHonorsController> _logger;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public LicensesAndHonorsController(ILogger<LicensesAndHonorsController> logger, IBasicInfoFacad basicInfoFacad)
        {
            _logger = logger;
            _basicInfoFacad = basicInfoFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<LicensesAndHonorsDto>>> Get_LicensesAndHonorss([FromBody] RequestLicensesAndHonorsDto request)
        {
            try
            {
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                return await _basicInfoFacad.GetLicensesAndHonorssService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.LicensesAndHonors_Save })]
        public async Task<LicensesAndHonorsDto> Get_LicensesAndHonors(int? id = null)
        {
            try
            {
                return await _basicInfoFacad.GetLicensesAndHonorsService.Execute(new RequestLicensesAndHonorsDto() { LicensesAndHonorsId = id, IsActive = (byte)TablesGeneralIsActive.Active });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.LicensesAndHonors_Save })]
        public async Task<ResultDto<LicensesAndHonorsDto>> Save_LicensesAndHonors([FromBody] LicensesAndHonorsDto request)
        {
            try
            {
                request.Userid = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserID").Value);
                return await _basicInfoFacad.SaveLicensesAndHonorsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.LicensesAndHonors_Delete })]
        public ResultDto Delete_LicensesAndHonors(int id)
        {
            try
            {
                return _basicInfoFacad.DeleteLicensesAndHonorsService.Execute(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
