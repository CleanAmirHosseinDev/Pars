﻿using Microsoft.AspNetCore.Http;
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

    public class CustomersController : BaseController
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IUserFacad _userFacad;
        public CustomersController(ILogger<CustomersController> logger, IUserFacad userFacad)
        {
            _logger = logger;
            _userFacad = userFacad;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResultDto<IEnumerable<CustomersDto>>> Get_Customerss([FromBody] RequestCustomersDto request)
        {
            try
            {
                request.IsActive = (byte)TablesGeneralIsActive.Active;
                return await _userFacad.GetCustomerssService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("[action]/{id}/")]
        public async Task<CustomersDto> Get_Customers(int? id = null)
        {
            try
            {
                return await _userFacad.GetCustomersService.Execute(new RequestCustomersDto() { CustomerId =id });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

      

    }
}
