//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using ParsKyanCrm.Application.Dtos.Users;
//using ParsKyanCrm.Application.Patterns.FacadPattern;
//using ParsKyanCrm.Common.Dto;
//using ParsKyanCrm.Common.Enums;
//using ParsKyanCrm.Infrastructure.Filters;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace EndPoint.Controllers.api.admin
//{
//    [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateDataFormQuestions })]
//    public class CorporateController : BaseController
//    {
//        private readonly ILogger<CorporateController> _logger;
//        private readonly IUserFacad _userFacad;
//        public CorporateController(ILogger<CorporateController> logger, IUserFacad userFacad)
//        {
//            _logger = logger;
//            _userFacad = userFacad;
//        }

//        [Route("[action]")]
//        [HttpPost]
//        //public async Task<ResultDto<IEnumerable<DataFormQuestionsOptionDto>>> Get_DataFormQuestionsOptiones([FromBody] RequestDataFormQuestionsOptionDto request)
//        //{
//        //    try
//        //    {
//        //        return await _userFacad.GetDataFormQuestionsOptionsService.Execute(request);
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw;
//        //    }
//        //}

//        //[Route("[action]/{id}/")]
//        //[HttpGet]
//        //[UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.CorporateDataFormQuestions_Save })]
//        //public async Task<DataFormQuestionsOptionDto> Get_DataFormQuestionsOptione(int? id = null)
//        //{
//        //    try
//        //    {
//        //        return await _userFacad.GetDataFormQuestionsOptionService.Execute(id);
//        //    }
//        //    catch (Exception)
//        //    {
//        //        throw;
//        //    }
//        //}

//        [HttpPost]
//        [Route("[action]")]
//        [UserRoleAdminRolesFilter(Role = new[] { UserRoleAdminRoles.City_Save })]
//        public async Task<ResultDto<CityDto>> Save_City([FromBody] CityDto request)
//        {
//            try
//            {
//                return await _userFacad.SaveCityService.Execute(request);
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }

//        [HttpPost]
//        [Route("[action]")]        
//        public async Task<ResultDto<IEnumerable<StateDto>>> Get_States_Combo(RequestStateDto request)
//        {
//            try
//            {
//                return (await _userFacad.GetStatesService.Execute(request));
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }

//    }
//}
