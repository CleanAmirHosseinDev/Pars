using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Application.Services.Users.Queries.GetServiceFeeAndCustomerByRequest;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Infrastructure;
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
                if (request.LoginName=="11" || request.LoginName=="12")
                {
                    request.IsCorporate = 1;
                }
                else if (request.LoginName == "1" || request.LoginName == "5" || request.LoginName == "6" || request.LoginName == "8")
                {
                    request.IsCorporate = 2;

                }
                if (!request.ForTimeLine)
                {
                    request.UserID = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                }              
                return await _userFacad.GetRequestForRatingsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

       
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<RequestForRatingDto>>> Get_RequestForRatingsA([FromBody] RequestRequestForRatingDto request)
        {
            try
            {

                request.LoginName = null;
                request.CustomerId = null;
                //request.UserID = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                return await _userFacad.GetRequestForRatingsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ActionResult> Get_RequestForRatingsA1(RequestRequestForRatingDto request)
        {
            try
            {

                request.LoginName = null;
                request.CustomerId = null;
                //request.UserID = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                return File(await _userFacad.GetRequestForRatingsService.Execute1(request), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
               
            }
            catch (Exception ex)
            {
                return null;
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
        public ResultDto Delete_ContractAndFinancialDocuments(int id)
        {
            try
            {
                return _userFacad.DeleteContractAndFinancialDocumentsService.Execute(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        

        [Route("[action]/{id}/")]
        [HttpGet]
        public async Task<ResultDto<ContractAndFinancialDocumentsDto>> Get_ContractAndFinancialDocuments(int? id = null)
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
               // request.RequestID = 1347;
                // request.PriceContract = Convert.ToDecimal((request.PriceContractStr).ToString().Replace(",", ""));
                return await _userFacad.SaveContractAndFinancialDocumentsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResultDto<ContractAndFinancialDocumentsDto>> Save_Remaining([FromBody] ContractAndFinancialDocumentsDto request)
        {
            try
            {
                request.EditStatuse = 14;
                return await _userFacad.SaveContractAndFinancialDocumentsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<LevelStepSettingDto>>> Get_LevelStepSetings([FromBody] RequestLevelStepSettingDto request)
        {
            try
            {
                return await _userFacad.GetLevelStepSettingsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        public async Task<ResultDto<IEnumerable<UserRolesDto>>> Get_UsersByRole(int? id = null)
        {
            try
            {
                return await _userFacad.GetUserssService.Execute(new RequestUserRolesDto() { PageIndex = 0, PageSize = 0, RoleId = id });
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<CustomerRequestInformationsDto> Get_CustomerRequestInformationsDto([FromBody] RequestCustomerRequestInformationDto request)
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

        [HttpPost("[action]")]
        public async Task<ResultDto<CommentDto>> AddComment([FromBody] CommentDto comment)
        {
            try
            {
                return await _userFacad.AddRequestCommentService.Execute(comment);
            }
            catch (Exception ex)
            {
                throw; 
            }
        }
    }
}
