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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFormAnswerTabless
{

    public class GetDataFormAnswerTablessService : IGetDataFormAnswerTablessService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetDataFormAnswerTablessService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ResultDto<IEnumerable<DataFormAnswerTablesDto>>> Execute(RequestDataFormAnswerTablesDto request)
        {
            try
            {

                var lists = (from s in _context.DataFormAnswerTables
                             where (s.FormId == request.FormId || request.FormId == null) &&
                             (s.CustomerId == request.CustomerId || request.CustomerId == null)
                             select s);

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.Answer1.Contains(request.Search) ||
                p.Answer2.Contains(request.Search) ||
                p.Answer3.Contains(request.Search)
                );

                switch (request.SortOrder)
                {
                    case "AnswerTableId_D":
                        lists = lists.OrderByDescending(s => s.AnswerTableId);
                        break;
                    case "AnswerTableId_A":
                        lists = lists.OrderBy(s => s.AnswerTableId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.AnswerTableId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<DataFormAnswerTablesDto>>
                    {
                        Data = _mapper.Map<IEnumerable<DataFormAnswerTablesDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };

                }
                else
                {

                    var list_Res_Pageing = await Pagination<Domain.Entities.DataFormAnswerTables>.CreateAsync(lists.AsNoTracking(), request);

                    return new ResultDto<IEnumerable<DataFormAnswerTablesDto>>
                    {
                        Data = _mapper.Map<IEnumerable<DataFormAnswerTablesDto>>(list_Res_Pageing),
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
