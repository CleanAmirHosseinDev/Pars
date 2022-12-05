using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers.api
{
    public class ActivityController : BaseController
    {

        private readonly ILogger<ActivityController> _logger;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public ActivityController(ILogger<ActivityController> logger, IBasicInfoFacad basicInfoFacad)
        {
            _logger = logger;
            _basicInfoFacad = basicInfoFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<ActivityDto>>> Get_Activitys([FromBody] RequestActivityDto request)
        {
            try
            {
                return await _basicInfoFacad.GetActivitysService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [Route("[action]/{id}/")]
        [HttpGet]        
        public async Task<ActivityDto> Get_Activity(int? id = null)
        {
            try
            {
                return await _basicInfoFacad.GetActivityService.Execute(new RequestActivityDto() { ActivityId = id });
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
