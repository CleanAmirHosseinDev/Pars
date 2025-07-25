﻿using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetRequestForRatings
{
    public interface IGetRequestForRatingsService
    {
        Task<ResultDto<IEnumerable<RequestForRatingDto>>> Execute(RequestRequestForRatingDto request);
        Task<byte[]> Execute1(RequestRequestForRatingDto request);
        Task<ResultDto<IEnumerable<RequestForRatingDto>>> ExecuteHistory(RequestRequestForRatingDto request);
    }
}
