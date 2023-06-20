using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.InitReferral
{

    public class InitReferralService : IInitReferralService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IBaseUserFacad _baseUserFacad;
        public InitReferralService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IBaseUserFacad baseUserFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            _baseUserFacad = baseUserFacad;
        }

        public async Task<ResultDto<IEnumerable<LevelStepSettingDto>>> Execute(string loginName, int? id = null)
        {
            try
            {

                var q = await _baseUserFacad.GetRequestForRatingsService.Execute(new Dtos.Users.RequestRequestForRatingDto() { RequestId = id });

                if (!q.Data.Any())
                {

                    return new ResultDto<IEnumerable<LevelStepSettingDto>>
                    {
                        IsSuccess = false,
                        Message = string.Empty,
                        StatusCode = 404
                    };

                }

                if (q.Data.FirstOrDefault().DestLevelStepAccessRole != loginName)
                {

                    return new ResultDto<IEnumerable<LevelStepSettingDto>>
                    {
                        IsSuccess = false,
                        Message = string.Empty,
                    };

                }

                if (q.Rows == 1 && q.Data.FirstOrDefault().DestLevelStepIndex == "15")
                {

                    return new ResultDto<IEnumerable<LevelStepSettingDto>>
                    {
                        IsSuccess = false,
                        Message = "فرایند اتمام یافت",                        
                    };

                }

                var qLSI = await DapperOperation.Run<LevelStepSettingDto>(@$"select * from {typeof(LevelStepSetting).Name} where LevelStepIndex = " + q.Data.FirstOrDefault().DestLevelStepIndex);
                foreach (var item in qLSI)
                {

                    if (!string.IsNullOrEmpty(item.DestLevelStepIndex))
                    {
                        var qqxcmcx = await DapperOperation.Run<LevelStepSettingDto>(@$"select * from {typeof(LevelStepSetting).Name} where LevelStepIndex = " + item.DestLevelStepIndex);
                        item.LevelStepAccessRole = qqxcmcx.FirstOrDefault().LevelStepAccessRole;
                        item.LevelStepStatus = qqxcmcx.FirstOrDefault().LevelStepStatus;
                        item.SendUser = q.Data.FirstOrDefault().SendUser;
                    }

                }

                return new ResultDto<IEnumerable<LevelStepSettingDto>>
                {
                    IsSuccess = true,
                    Message = string.Empty,
                    Data = qLSI
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
