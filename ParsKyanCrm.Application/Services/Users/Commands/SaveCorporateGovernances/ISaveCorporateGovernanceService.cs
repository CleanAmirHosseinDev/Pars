using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveCorporateGovernances
{
    public interface ISaveCorporateGovernanceService
    {
        Task<ResultDto<CorporateGovernanceDto>> Execute(CorporateGovernanceDto request);
    }
}
