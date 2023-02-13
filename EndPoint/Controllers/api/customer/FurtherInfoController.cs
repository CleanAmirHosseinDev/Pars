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
        public async Task<ResultDto<FurtherInfoDto>> Save_FurtherInfo([FromForm] FurtherInfoDto request)
        {
            try
            {
               
                return await _userFacad.SaveFurtherInfoService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<ResultDto<CorporateGovernanceDto>> Save_CorporateGovernance([FromForm] CorporateGovernanceDto request)
        {
            try
            {

                return await _userFacad.SaveCorporateGovernanceService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<ResultDto<ValueChainDto>> Save_ValueChain([FromForm] ValueChainDto request)
        {
            try
            {

                return await _userFacad.SaveValueChainService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResultDto<PublicActivitiesDto>> Save_PublicActivities([FromForm] PublicActivitiesDto request)
        {
            try
            {

                return await _userFacad.SavePublicActivitiesService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion        


        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<DataFormQuestionsDto>>> Get_DataFormQuestionss([FromBody] RequestDataFormQuestionsDto request)
        {
            try
            {
                return await _userFacad.GetDataFormQuestionssService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<DataFormAnswerTablesDto>>> Get_DataFormAnswerTabless([FromBody] RequestDataFormAnswerTablesDto request)
        {
            try
            {
                
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                return await _userFacad.GetDataFormAnswerTablessService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [Route("[action]/{id}/")]
        [HttpGet]
       
        public async Task<DataFormAnswerTablesDto> Get_DataFormAnswerTables(int? id = null)
        {
            try
            {
                return await _userFacad.GetDataFormAnswerTablesService.Execute(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<DataFromAnswersDto>>> Get_DataFromAnswerss([FromBody] RequestDataFromAnswersDto request)
        {
            try
            {
                return await _userFacad.GetDataFromAnswerssService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<DataFormAnswerTablesDto>> Save_DataFormAnswerTabless([FromForm] DataFormAnswerTablesDto request)
        {
            try
            {
                return await _userFacad.SaveDataFormAnswerTablesService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<DataFromAnswersDto>> Save_DataFromAnswers([FromForm] DataFromAnswersDto request)
        {
            try
            {
                return await _userFacad.SaveDataFromAnswersService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]        
        public ResultDto Delete_DataFormAnswerTables(int id)
        {
            try
            {
                return _userFacad.DeleteDataFormAnswerTablesService.Execute(id);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [Route("[action]/{id}/")]
        [HttpGet]
        public async Task<ResultDto<FurtherInfoDto>> Get_FurtherInfo(int? id = null)
        {
            try
            {
                return await _userFacad.GetFurtherInfoService.Execute(new RequestFurtherInfoDto() { RequestId = id });
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [Route("[action]/{id}/")]
        [HttpGet]
        public async Task<ResultDto<CorporateGovernanceDto>> Get_CorporateGovernances(int? id = null)
        {
            try
            {
                return await _userFacad.GetCorporateGovernancesService.Execute(new RequestCorporateGovernanceDto() { RequestId = id });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        public async Task<ResultDto<ValueChainDto>> Get_ValueChain(int? id = null)
        {
            try
            {
                return await _userFacad.GetValueChainService.Execute(new RequestValueChainDto() { RequestId = id });
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [Route("[action]/{id}/")]
        [HttpGet]
        public async Task<ResultDto<PublicActivitiesDto>> Get_PublicActivities(int? id = null)
        {
            try
            {
                return await _userFacad.GetPublicActivitiesService.Execute(new RequestPublicActivitiesDto() { RequestId = id });
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
