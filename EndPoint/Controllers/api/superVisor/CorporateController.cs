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

namespace EndPoint.Controllers.api.superVisor
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
        #region دریافت ذخیره حذف سوالات یا اپشن ها
        // نمایش همه سوالات مرتبط با ایدی دریافتی
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
        // نمایش همه گزینه های سوالاتی که دارای گزینه هستند
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
        // ذخیره آپشن برای فرم های دارای سلک باکس
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<DataFormQuestionsOptionDto>> Save_DataFormQuestionsOptionDto([FromForm] DataFormQuestionsOptionDto request)
        {
            try
            {
                return await _userFacad.SaveDataFormQuestionsOptionService.Execute(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // حذف یک آپشن از سلک باکس
        [Route("[action]/{id}/")]
        [HttpGet]
        public ResultDto Delete_DataFormQuestionsOption(int id)
        {
            try
            {
                return _userFacad.DeleteDataFormQuestionsOptionService.Execute(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


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
                throw ex;
            }
        }
    }
}
