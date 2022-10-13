using ParsKyanCrm.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Securitys.Queries.Logins
{
    public interface ILoginsService
    {
        Task<ResultDto<ResultLoginDto>> Execute(RequestLoginDto request);
    }
}
