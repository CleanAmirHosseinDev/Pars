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

namespace EndPoint.Controllers.api.customer
{

    public class FurtherInfoController : BaseController
    {

        private readonly ILogger<FurtherInfoController> _logger;
        private readonly IUserFacad _userFacad;
        public FurtherInfoController(ILogger<FurtherInfoController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        #region BoardOfDirectors

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<BoardOfDirectorsDto>>> Get_BoardOfDirectorss([FromBody] RequestBoardOfDirectorsDto request)
        {
            try
            {
                request.CustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "CustomerID").Value);
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                return await _userFacad.GetBoardOfDirectorssService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        public async Task<BoardOfDirectorsDto> Get_BoardOfDirectors(int? id = null)
        {
            try
            {
                return await _userFacad.GetBoardOfDirectorsService.Execute(new RequestBoardOfDirectorsDto() { BoardOfDirectorsId = id, CustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "CustomerID").Value), IsActive = (byte)TablesGeneralIsActive.Active });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResultDto<BoardOfDirectorsDto>> Save_BoardOfDirectorsCustomers([FromBody] BoardOfDirectorsDto request)
        {
            try
            {
                request.CustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "CustomerID").Value);
                return await _userFacad.SaveBoardOfDirectorsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region WorkExperience


        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<WorkExperienceDto>>> Get_WorkExperiences([FromBody] RequestWorkExperienceDto request)
        {
            try
            {
                request.CustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "CustomerID").Value);
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                return await _userFacad.GetWorkExperiencesService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        public async Task<WorkExperienceDto> Get_WorkExperience(int? id = null)
        {
            try
            {
                return await _userFacad.GetWorkExperienceService.Execute(new RequestWorkExperienceDto() { SkilsId = id, CustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "CustomerID").Value), IsActive = (byte)TablesGeneralIsActive.Active });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResultDto<WorkExperienceDto>> Save_WorkExperienceCustomers([FromBody] WorkExperienceDto request)
        {
            try
            {
                request.CustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "CustomerID").Value);
                return await _userFacad.SaveWorkExperienceService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region EducationCourses


        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<EducationCoursesDto>>> Get_EducationCoursess([FromBody] RequestEducationCoursesDto request)
        {
            try
            {
                request.CustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "CustomerID").Value);
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                return await _userFacad.GetEducationCoursessService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        public async Task<EducationCoursesDto> Get_EducationCourses(int? id = null)
        {
            try
            {
                return await _userFacad.GetEducationCoursesService.Execute(new RequestEducationCoursesDto() { EducationCoursesId = id, CustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "CustomerID").Value), IsActive = (byte)TablesGeneralIsActive.Active });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResultDto<EducationCoursesDto>> Save_EducationCoursesCustomers([FromBody] EducationCoursesDto request)
        {
            try
            {
                request.CustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "CustomerID").Value);
                return await _userFacad.SaveEducationCoursesService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

    }
}
