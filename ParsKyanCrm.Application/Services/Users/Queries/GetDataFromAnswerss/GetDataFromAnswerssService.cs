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
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetDataFromAnswerssService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ResultDto<IEnumerable<DataFromAnswersDto>>> Execute(RequestDataFromAnswersDto request)
        {
            try
            {

                var lists = (from s in _context.DataFromAnswers
                             where (s.FormId == request.FormId || request.FormId == null) &&
                             (s.CustomerId == request.CustomerId || request.CustomerId == null)
                             select s);

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.Answer.Contains(request.Search));

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
