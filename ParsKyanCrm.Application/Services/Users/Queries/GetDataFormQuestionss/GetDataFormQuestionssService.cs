using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using RestSharp;
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
                    join d in _context.DataForms on s.DataFormId equals d.FormId
                    where ((s.DataFormId == request.DataFormId || request.DataFormId == null) && d.IsActive==15)
                    select s
                );
                if (request.DataFormType == 2 )
                {
                    lists = (
                        from s in _context.DataFormQuestions
                        where (s.DataFormType == 2)
                        select s
                    );
                    if (request.DataFormId != null)
                    {
                        lists = (
                            from s in _context.DataFormQuestions
                            where (s.DataFormType == 2 && s.DataFormId == request.DataFormId && s.IsActive == 15)
                            select s
                        );
                    }
                }
                
                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.QuestionName.Contains(request.Search) ||
                    p.QuestionText.Contains(request.Search) ||
                    p.QuestionType.Contains(request.Search)
                );

                switch (request.SortOrder)
                {
                    case "DataFormQuestionId_D":
                        lists = lists.OrderByDescending(s => s.DataFormQuestionId);
                        break;
                    case "DataFormQuestionId_A":
                        lists = lists.OrderBy(s => s.DataFormQuestionId);
                        break;
                    default:
                        lists = lists.OrderBy(s => s.QuestionOrder);
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

                    var list_Res_Pageing = await Pagination<Domain.Entities.DataFormQuestions>.CreateAsync(lists.AsNoTracking(), request);

                    return new ResultDto<IEnumerable<DataFormQuestionsDto>>
                    {
                        Data = _mapper.Map<IEnumerable<DataFormQuestionsDto>>(list_Res_Pageing),
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
