using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Api.Controllers.api.customer
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


        [HttpPost]
        [Route("[action]")]
        public ResultDto Save_BasicInformationCustomers([FromBody] CustomersDto request)
        {
            try
            {
                request.CustomerID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "CustomerID").Value.Decrypt_Advanced_For_Number());
                return _userFacad.SaveBasicInformationCustomersService.Execute(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
