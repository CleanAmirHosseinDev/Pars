using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Application.Services.Reports.Queries.CustomerDataFormReport;
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
        private readonly IReportFacad _reportFacad;
        public CorporateController(ILogger<CorporateController> logger, IUserFacad userFacad, IReportFacad reportFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
            _reportFacad = reportFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<ResultCustomerDataFormReportDto>> Get_CustomerDataFormReport([FromBody] RequestCustomerDataFormReportDto request)
        {
            try
            {
                return await _reportFacad.CustomerDataFormReportService.Execute(request);
            }
            catch (Exception ex)
            {
                return null;
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
        public async Task<DataFormReportDto> Get_DataFormReport([FromBody] RequestDataFormReportDto request)
        {
            try
            {
                return await _userFacad.GetDataFormReportService.ExecuteWhithParam(request);
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
