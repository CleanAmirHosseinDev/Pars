using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Application.Services.Users.Queries.GetServiceFeeAndCustomerByRequest;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Common.Enums;
using ParsKyanCrm.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers.api.admin
{
    public class RequestForRatingController : BaseController
    {

        private readonly ILogger<RequestForRatingController> _logger;
        private readonly IUserFacad _userFacad;
        public RequestForRatingController(ILogger<RequestForRatingController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<RequestForRatingDto>>> Get_RequestForRatings([FromBody] RequestRequestForRatingDto request)
        {
            try
            {
                
                request.LoginName = User.Claims.FirstOrDefault(c => c.Type == "LoginName").Value;
                request.UserID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserID").Value);
                return await _userFacad.GetRequestForRatingsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<RequestReferencesDto>>> Get_RequestReferencessService([FromBody] RequestRequestReferencesDto request)
        {
            try
            {
                return await _userFacad.GetRequestReferencessService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [Route("[action]/{id}/")]
        [HttpGet]
        public ResultDto Delete_RequestForRating(int id)
        {
            try
            {
                return _userFacad.DeleteRequestForRatingService.Execute(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
    }
}


