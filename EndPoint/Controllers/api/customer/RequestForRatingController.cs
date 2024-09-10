using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers.api.customer
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
                request.CustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "CustomerID").Value);
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
                request.Request.CustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "CustomerID").Value);
                request.SendUser = null;
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
        public async Task<ResultDto<ContractAndFinancialDocumentsDto>>Get_ContractAndFinancialDocuments(int? id = null)
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
        public async Task<ResultDto<ContractAndFinancialDocumentsDto>> Save_ContractAndFinancialDocuments([FromForm] ContractAndFinancialDocumentsDto request)
        {
            try
            {
                
                return await _userFacad.SaveContractAndFinancialDocumentsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<ResultDto<ContractAndFinancialDocumentsDto>> Save_ContractAndFinancialDocumentsNoForm([FromBody] ContractAndFinancialDocumentsDto request)
        {
            try
            {
                request.IsCustomer = true;
                return await _userFacad.SaveContractAndFinancialDocumentsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<RequestForRatingDto>>> Get_RequestHistory([FromBody] RequestRequestForRatingDto request)
        {
            try
            {
                request.CustomerId = null;
                request.LoginName = User.Claims.FirstOrDefault(c => c.Type == "LoginName").Value;
                request.UserID = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                return await _userFacad.GetRequestForRatingsService.ExecuteHistory(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<ResultDto<CustomerRequestInformationsDto>> Save_SaveCustomerRequestInformation([FromForm] CustomerRequestInformationsDto request)
        {
            try
            {
                request.CustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "CustomerID").Value);
                return await _userFacad.SaveCustomerRequestInformationService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<CustomerRequestInformationsDto> Get_CustomerRequestInformations([FromBody] RequestCustomerRequestInformationDto request)
        {
            try
            {
                return await _userFacad.GetCustomerRequestInformationService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
