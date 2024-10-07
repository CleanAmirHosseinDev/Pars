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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetQuestionLevels
{

    public class GetQuestionLevelsService : IGetQuestionLevelsService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetQuestionLevelsService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        public async Task<ResultDto<IEnumerable<QuestionLevelDto>>> Execute(RequestQuestionLevelDto request)
        {
            try
            {
                var lists = (
                    from s in _context.QuestionLevel
                    select s).AsQueryable();

                var new_list = lists;

                if (request.IsActive == 15) new_list = new_list.Where(p => p.IsActive == 15);

                else if (request.IsActive == 14) new_list = new_list.Where(p => p.IsActive == 14);

                if (!string.IsNullOrEmpty(request.Search)) new_list = new_list.Where(p => p.LevelDescription.Contains(request.Search) || p.LevelTitle.Contains(request.Search));

                switch (request.SortOrder)
                {
                    case "QuestionLevelId_D":
                        new_list = new_list.OrderByDescending(s => s.QuestionLevelId);
                        break;
                    case "QuestionLevelId_A":
                        new_list = new_list.OrderBy(s => s.QuestionLevelId);
                        break;
                    case "LevelTitle_D":
                        new_list = new_list.OrderByDescending(s => s.LevelTitle);
                        break;
                    case "LevelTitle_A":
                        new_list = new_list.OrderBy(s => s.LevelTitle);
                        break;
                    default:
                        new_list = new_list.OrderByDescending(s => s.QuestionLevelId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {
                    var res_Lists = await new_list.ToListAsync();

                    return new ResultDto<IEnumerable<QuestionLevelDto>>
                    {
                        Data = _mapper.Map<IEnumerable<QuestionLevelDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };
                }
                else
                {
                    var lists_Res_Pageing =
                        await Pagination<Domain.Entities.QuestionLevel>.CreateAsync(new_list.AsNoTracking(), request);

                    return new ResultDto<IEnumerable<QuestionLevelDto>>
                    {
                        Data = _mapper.Map<IEnumerable<QuestionLevelDto>>(lists_Res_Pageing),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = lists_Res_Pageing.Rows,
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
