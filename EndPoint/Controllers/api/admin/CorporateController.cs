using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Common.Enums;
using ParsKyanCrm.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers.api.admin
{
    [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateDataForm })]
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
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateDataFormDocument })]
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
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateDataFormDocument_Save })]
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

        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateDataFormDocument_Save })]
        public async Task<ResultDto<DataFormDocumentsDto>> Save_DataFormDocument([FromBody] DataFormDocumentsDto request)
        {
            try
            {
                return await _userFacad.SaveDataFormDocumentService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("[action]/{id}/")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateDataFormDocument_Delete })]
        public ResultDto Delete_DataFormDocument(int id)
        {
            try
            {
                return _userFacad.DeleteDataFormDocumentService.Execute(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]")]
        [HttpPost]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateDataForm })]
        public async Task<ResultDto<IEnumerable<DataFormsDto>>> Get_DataForms([FromBody] RequestDataFormsDto request)
        {
            try
            {
                return await _userFacad.GetDataFormsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateDataForm_Save })]
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

        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateDataForm_Save })]
        public async Task<ResultDto<DataFormsDto>> Save_DataForm([FromBody] DataFormsDto request)
        {
            try
            {
                return await _userFacad.SaveDataFormService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]       
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateDataForm_Delete })]
        public ResultDto Delete_DataForm(int id)
        {
            try
            {
                return _userFacad.DeleteDataFormService.Execute(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

     

        [Route("[action]")]
        [HttpPost]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateDataFormQuestions })]
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

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateDataFormQuestions_Save })]
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

        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateDataFormQuestions_Save })]
        public async Task<ResultDto<DataFormQuestionsDto>> Save_DataFormQuestions([FromBody] DataFormQuestionsDto request)
        {
            try
            {
                return await _userFacad.SaveDataFormQuestionsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateDataFormQuestions_Delete })]
        public ResultDto Delete_DataFormQuestions(int id)
        {
            try
            {
                return _userFacad.DeleteDataFormQuestionsService.Execute(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]")]
        [HttpPost]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateDataFormQuestionsOptione })]
        public async Task<ResultDto<IEnumerable<DataFormQuestionsOptionDto>>> Get_DataFormQuestionsOptiones([FromBody] RequestDataFormQuestionsOptionDto request)
        {
            try
            {
                return await _userFacad.GetDataFormQuestionsOptionsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateDataFormQuestionsOptione_Save })]
        public async Task<DataFormQuestionsOptionDto> Get_DataFormQuestionsOptione(int? id = null)
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

        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateDataFormQuestionsOptione_Save })]
        public async Task<ResultDto<DataFormQuestionsOptionDto>> Save_DataFormQuestionsOptione([FromBody] DataFormQuestionsOptionDto request)
        {
            try
            {
                return await _userFacad.SaveDataFormQuestionsOptionService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateDataFormQuestionsOptione_Delete })]
        public ResultDto Delete_DataFormQuestionsOptione(int id)
        {
            try
            {
                return _userFacad.DeleteDataFormQuestionsOptionService.Execute(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]")]
        [HttpPost]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateQuestionLevel })]
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

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateQuestionLevel_Save })]
        public async Task<QuestionLevelDto> Get_QuestionLevel(int id)
        {
            try
            {
                var request = new RequestQuestionLevelDto() { QuestionLevelId = id };
                return await _userFacad.GetQuestionLevelService.Execute(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateQuestionLevel_Save })]
        public async Task<ResultDto<QuestionLevelDto>> Save_QuestionLevel([FromBody] QuestionLevelDto request)
        {
            try
            {
                return await _userFacad.SaveQuestionLevelService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateQuestionLevel_Delete })]
        public ResultDto Delete_QuestionLevel(int id)
        {
            try
            {
                return _userFacad.DeleteQuestionLevelService.Execute(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
