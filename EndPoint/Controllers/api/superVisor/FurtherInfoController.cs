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

    public class FurtherInfoController : BaseController
    {

        private readonly ILogger<FurtherInfoController> _logger;
        private readonly IUserFacad _userFacad;
        public FurtherInfoController(ILogger<FurtherInfoController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
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

    }
}
