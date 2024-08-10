using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFromAnswerss
{
    public class GetDataFromAnswerssService : IGetDataFromAnswerssService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetDataFromAnswerssService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<IEnumerable<DataFromAnswersDto>>> Execute(RequestDataFromAnswersDto request)
        {
            try
            {
                var lists = (from s in _context.DataFromAnswers
                             where (s.FormId == request.FormId || request.FormId == null) &&
                             (s.RequestId == request.RequestId || request.RequestId == null)
                             select s);
                // Return Back All Question Except Document
                if (request.DataFormQuestionId == null && request.FormId == null &&
                    request.DataFormDocumentId == null && request.RequestId != null && request.IsActive == 15)
                {
                    lists = (from s in _context.DataFromAnswers
                             where s.DataFormDocumentId == null && s.RequestId == request.RequestId
                             select s);
                }
                // Return Back All Document
                else if (request.DataFormQuestionId == null && request.FormId == null)
                {
                    lists = (from s in _context.DataFromAnswers
                             where (s.FormId == 0 && s.RequestId == request.RequestId && s.DataFormQuestionId == 0)
                             select s);
                }

                // Filter By Search Query
                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.Answer.Contains(request.Search) || p.Description.Contains(request.Search));


                // Return Back List Of Document
                if (request.PageIndex == 0 && request.PageSize == 0 && request.FormId == 0 &&
                    request.DataFormQuestionId == 0)
                {
                    lists = (from s in _context.DataFromAnswers
                             where (s.FormId == 0) &&
                             (s.RequestId == request.RequestId) &&
                             (s.DataFormDocumentId != null) &&
                             (s.DataFormQuestionId == 0) && (s.IsActive == 15)
                             select s);
                }

                switch (request.SortOrder)
                {
                    case "AnswerId_D":
                        lists = lists.OrderByDescending(s => s.AnswerId);
                        break;
                    case "AnswerId_A":
                        lists = lists.OrderBy(s => s.AnswerId);
                        break;
                    default:
                        lists = lists.OrderBy(s => s.AnswerId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {
                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<DataFromAnswersDto>>
                    {
                        Data = _mapper.Map<IEnumerable<DataFromAnswersDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };
                }
                else
                {
                    var list_Res_Pageing = await Pagination<Domain.Entities.DataFromAnswers>.CreateAsync(lists.AsNoTracking(), request);

                    return new ResultDto<IEnumerable<DataFromAnswersDto>>
                    {
                        Data = _mapper.Map<IEnumerable<DataFromAnswersDto>>(list_Res_Pageing),
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
