using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Application.Services.Users.Queries.GetServiceFeeAndCustomerByRequest;
using ParsKyanCrm.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers.api.superVisor
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
                request.CustomerId = null;              
                request.LoginName = User.Claims.FirstOrDefault(c => c.Type == "LoginName").Value;
                return await _userFacad.GetRequestForRatingsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResultDto> Save_Request([FromBody] RequestReferencesDto request)
        {
            try
            {
                request.SendUser = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserID").Value);
                return await _userFacad.SaveRequestForRatingService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        public async Task<LevelStepSettingDto> Get_LevelStepSetting(int? id = null)
        {
            try
            {
                return await _userFacad.GetLevelStepSettingService.Execute(new RequestLevelStepSettingDto() { LevelStepSettingIndexId = id });
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [Route("[action]/{id}/")]
        [HttpGet]
        public async Task<ResultDto<IEnumerable<LevelStepSettingDto>>> InitReferral(int? id = null)
        {
            try
            {
               int m=  int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserID").Value);
                return await _userFacad.InitReferralService.Execute(User.Claims.FirstOrDefault(c => c.Type == "LoginName").Value, id);
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
        public async Task<ResultGetServiceFeeAndCustomerByRequestDto> Get_ServiceFeeAndCustomerByRequest(int id)
        {
            try
            {
                return await _userFacad.GetServiceFeeAndCustomerByRequestService.Execute(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [Route("[action]/{id}/")]
        [HttpGet]
        public async Task<ContractAndFinancialDocumentsDto> Get_ContractAndFinancialDocuments(int? id = null)
        {
            try
            {
                return await _userFacad.GetContractAndFinancialDocumentsService.Execute(new RequestContractAndFinancialDocumentsDto() { RequestID = id });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResultDto<ContractAndFinancialDocumentsDto>> Save_ContractAndFinancialDocuments([FromBody] ContractAndFinancialDocumentsDto request)
        {
            try
            {
                request.PriceContract = Convert.ToDecimal((request.PriceContractStr).ToString().Replace(",", ""));
                return await _userFacad.SaveContractAndFinancialDocumentsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
