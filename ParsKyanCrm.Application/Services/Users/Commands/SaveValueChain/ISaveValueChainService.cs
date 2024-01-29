using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveValueChain
{
    public interface ISaveValueChainService
    {
        Task<ResultDto<ValueChainDto>> Execute(ValueChainDto request);
        Task<ResultDto<ValueChainDto>> ExecuteCopy(string Request);
    }
}
