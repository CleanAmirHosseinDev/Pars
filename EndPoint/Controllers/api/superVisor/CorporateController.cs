using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Common.Enums;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers.api.superVisor
{
    public class CorporateController : BaseController
    {
        private readonly ILogger<CorporateController> _logger;
        private readonly IUserFacad _userFacad;
        public CorporateController(ILogger<CorporateController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<DataFormsDto>>> Get_DataForms([FromBody] RequestDataFormsDto request)
        {
            try
            {
                return await _userFacad.GetDataFormsService.Execute(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

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
                throw ex;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        public async Task<DataFormQuestionsDto> Get_DataFormQuestions(int? id = null)
        {
            try
            {
                return await _userFacad.GetDataFormQuestionsService.Execute(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<DataFormQuestionsOptionDto>>> Get_Options([FromBody] RequestDataFormQuestionsOptionDto request)
        {
            try
            {
                return await _userFacad.GetDataFormQuestionsOptionsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        public async Task<DataFormQuestionsOptionDto> Get_Option(int? id = null)
        {
            try
            {
                return await _userFacad.GetDataFormQuestionsOptionService.Execute(id);
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
        public async Task<ResultDto<DataFromAnswersDto>> Save_DataFromAnswers([FromBody] DataFromAnswersDto request)
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

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<DataFormReportDto>> Get_DataFromReport([FromBody] DataFormReportDto request)
        {
            try
            {
                return null;
                //return await _userFacad.GetDataFormReportService.ExecuteWhithParam(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<DataFormReportDto>> Save_DataFromReport([FromBody] DataFormReportDto request)
        {
            try
            {
                return await _userFacad.SaveDataFormReportsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
