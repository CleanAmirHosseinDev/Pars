using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers.api.customer
{

    [Route("api/customer/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class BaseController : ControllerBase
    {
    }
}
