using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Common.Enums;
using ParsKyanCrm.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers.api.superVisor
{
   
    public class ContractController : BaseController
    {
        private readonly ILogger<ContractController> _logger;
        private readonly IUserFacad _userFacad;
        public ContractController(ILogger<ContractController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<ContractDto>>> Get_Contracts([FromBody] RequestContractDto request)
        {
            try
            {
                return await _userFacad.GetContractsService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("[action]/{id}/")]
        [HttpGet]
        public async Task<ResultDto<IEnumerable<ContractPagesDto>>> Get_ContractPagess(int? id = null)
        {
            try
            {
                return await _userFacad.GetContractPagessService.Execute(new RequestContractPagesDto() {  ContractId=id.Value });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
