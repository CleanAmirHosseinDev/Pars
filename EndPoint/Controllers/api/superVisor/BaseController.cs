using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers.api.superVisor
{
    [Route("api/superVisor/[controller]")]
    [ApiController]
    [Authorize(Roles = "ResponsibleEvaluation,ContractsExpert,Evaluator,KodalExpert,Support,ChairmanEvaluationCommittee,FinancialManager,CorporateAdmin,CorporateUser")]
    public class BaseController : ControllerBase
    {
    }
}
