﻿
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetLoginLogs
{
    public interface IGetLoginLogsService
    {
        Task<ResultDto<IEnumerable<LoginLogDto>>> Execute(RequestLoginLogDto request);
    }
}
