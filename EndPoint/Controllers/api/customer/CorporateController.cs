﻿using Microsoft.AspNetCore.Http;
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

namespace EndPoint.Controllers.api.customer
{

    public class CorporateController : BaseController
    {
        private readonly ILogger<FurtherInfoController> _logger;
        private readonly IUserFacad _userFacad;
        public CorporateController(ILogger<FurtherInfoController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<DataFormDocumentsDto>>> Get_DataFormDocuments([FromBody] RequestDataFormDocumentsDto request)
        {
            try
            {
                return await _userFacad.GetDataFormDocumentsService.Execute(request);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<DataFormDocumentsDto> Get_DataFormDocument(int id)
        {
            try
            {
                return await _userFacad.GetDataFormDocumentService.Execute(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        public async Task<DataFormsDto> Get_DataForm(int id)
        {
            try
            {
                return await _userFacad.GetDataFormService.Execute(id);
            }
            catch (Exception)
            {
                throw;
            }
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

        [Route("[action]/{id}/")]
        [HttpGet]
        public async Task<DataFromAnswersDto> Get_DataFromAnswers(int? id = null)
        {
            try
            {
                return await _userFacad.GetDataFromAnswersService.Execute(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<DataFromAnswersDto>>> Get_DataFromAnswersDocuments([FromBody] RequestDataFromAnswersDto request)
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

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<DataFromAnswersDto>> Save_DataFromAnswersUpload([FromForm] DataFromAnswersDto request)
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
        public async Task<ResultDto<IEnumerable<DataFormReportDto>>> Get_DataFormReports([FromBody] RequestDataFormReportDto request)
        {
            try
            {
                return await _userFacad.GetDataFormReportsService.Execute(request);
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

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<DataFormReportCheckDto>> Save_DataFormReportCheck([FromBody] DataFormReportCheckDto request)
        {
            try
            {
                return await _userFacad.SaveDataFormReportCheckService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<DataFormReportCheckDto>>> Get_DataFormReportChecks([FromBody] RequestDataFormReportCheckDto request)
        {
            try
            {
                return await _userFacad.GetDataFormReportChecksService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<DataFormReportCheckDto> Get_DataFormReportCheck([FromBody] RequestDataFormReportCheckDto request)
        {
            try
            {
                return await _userFacad.GetDataFormReportCheckService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<QuestionLevelDto>>> Get_QuestionLevels([FromBody] RequestQuestionLevelDto request)
        {
            try
            {
                return await _userFacad.GetQuestionLevelsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
