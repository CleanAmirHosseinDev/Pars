using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetLevelStepSettings
{

    public class GetLevelStepSettingsService : IGetLevelStepSettingsService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetLevelStepSettingsService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ResultDto<IEnumerable<LevelStepSettingDto>>> Execute(RequestLevelStepSettingDto request)
        {
            try
            {

                var lists = (from s in _context.LevelStepSetting
                             select s);

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.AccessRoleName.Contains(request.Search)
                || p.LevelStepStatus.Contains(request.Search) ||
                p.DestLevelStepIndexButton.Contains(request.Search)               
                );

                switch (request.SortOrder)
                {
                    case "LevelStepSettingIndexId_D":
                        lists = lists.OrderByDescending(s => s.LevelStepSettingIndexId);
                        break;
                    case "LevelStepSettingIndexId_A":
                        lists = lists.OrderBy(s => s.LevelStepSettingIndexId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.LevelStepSettingIndexId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = (await lists.ToListAsync()).Distinct(new GenericTypeCompare<LevelStepSetting>("LevelStepStatus"));

                    return new ResultDto<IEnumerable<LevelStepSettingDto>>
                    {
                        Data = _mapper.Map<IEnumerable<LevelStepSettingDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };

                }
                else
                {

                    var list_Res_Pageing = await Pagination<Domain.Entities.LevelStepSetting>.CreateAsync(lists.AsNoTracking(), request);

                    return new ResultDto<IEnumerable<LevelStepSettingDto>>
                    {
                        Data = _mapper.Map<IEnumerable<LevelStepSettingDto>>(list_Res_Pageing),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = list_Res_Pageing.Rows,
                    };

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
