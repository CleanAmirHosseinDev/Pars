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
                    where (s.DataFormId == request.DataFormId || request.DataFormId == null)
                    select s
                );

                var new_list = lists;

                if (request.DataFormType == 2)
                {
                    new_list = (
                        from s in _context.DataFormQuestions
                        where (s.DataFormType == 2 && s.IsActive == 15)
                        select s
                    );
                    if (request.DataFormId != null)
                    {
                        new_list = (
                            from s in _context.DataFormQuestions
                            where (s.DataFormType == 2 && s.DataFormId == request.DataFormId && s.IsActive == 15)
                            select s
                        );
                    }
                }

                if (!string.IsNullOrEmpty(request.Search)) new_list = lists.Where(p => p.QuestionName.Contains(request.Search) ||
                    p.QuestionText.Contains(request.Search) ||
                    p.QuestionType.Contains(request.Search)
                );

                if (request.Version != null) new_list = lists.Where(p => p.Version == request.Version);


                if (request.PageIndex == 0 && request.PageSize == 0 && request.IsActive == 15 && request.DataFormType == 2)
                {
                    if (request.QuestionLevel != null)
                    {
                        new_list = (
                            from s in _context.DataFormQuestions
                            where (s.DataFormType == 2 && s.IsActive == 15 && s.QuestionLevel == request.QuestionLevel)
                            select s
                        );
                    }
                    else
                    {
                        new_list = from s in _context.DataFormQuestions
                                where (s.DataFormType == 2 && s.IsActive == 15)
                                select s;
                    }
                }
                if (request.PageIndex == 0 && request.PageSize == 0 && request.IsActive == 15 && request.DataFormType == 2 && request.DataFormId != null && request.Version == null && request.QuestionLevel != null)
                {
                    new_list = from s in _context.DataFormQuestions
                            where (s.DataFormType == 2 && s.IsActive == 15 && s.DataFormId == request.DataFormId && s.QuestionLevel == request.QuestionLevel)
                            select s;
                }

                switch (request.SortOrder)
                {
                    case "DataFormQuestionId_D":
                        new_list = new_list.OrderByDescending(s => s.DataFormQuestionId);
                        break;
                    case "DataFormQuestionId_A":
                        new_list = new_list.OrderBy(s => s.DataFormQuestionId);
                        break;
                    default:
                        new_list = new_list.OrderByDescending(s => s.DataFormQuestionId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await new_list.ToListAsync();

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

                    var list_Res_Pageing = await Pagination<Domain.Entities.DataFormQuestions>.CreateAsync(new_list.AsNoTracking(), request);

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
