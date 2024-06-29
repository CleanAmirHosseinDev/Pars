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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFormQuestionss
{

    public class GetDataFormQuestionssService : IGetDataFormQuestionssService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetDataFormQuestionssService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        public async Task<ResultDto<IEnumerable<DataFormQuestionsDto>>> Execute(RequestDataFormQuestionsDto request)
        {
            try
            {
                var lists = (
                    from s in _context.DataFormQuestions
                    where s.DataFormId > 25
                    select s).AsQueryable();
                    //.Include(p => p.DataForms)

                if (request.IsActive == 15) lists = lists.Where(p => p.IsActive == 15);

                if (request.IsActive == 14) lists = lists.Where(p => p.IsActive == 14);

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(
                    p => p.QuestionText.Contains(request.Search) || p.QuestionName.Contains(request.Search) || p.HelpText.Contains(request.Search)
                    );

                if (request.DataFormId != null)
                    lists = (
                        from s in _context.DataFormQuestions
                        where s.DataFormId == request.DataFormId
                        select s
                    );

                switch (request.SortOrder)
                {
                    case "Id_D":
                        lists = lists.OrderByDescending(s => s.DataFormId);
                        break;
                    case "Id_A":
                        lists = lists.OrderBy(s => s.DataFormId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.DataFormId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {
                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<DataFormQuestionsDto>>
                    {
                        Data = _mapper.Map<IEnumerable<DataFormQuestionsDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };
                }
                else
                {
                    var lists_Res_Pageing = (
                        await Pagination<DataFormQuestions>.CreateAsync(lists.AsNoTracking(), request)
                    );

                    return new ResultDto<IEnumerable<DataFormQuestionsDto>>
                    {
                        Data = _mapper.Map<IEnumerable<DataFormQuestionsDto>>(lists_Res_Pageing),
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
