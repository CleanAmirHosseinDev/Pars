using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFormQuestionsOptions
{

    public class GetDataFormQuestionsOptionsService : IGetDataFormQuestionsOptionsService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetDataFormQuestionsOptionsService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<IEnumerable<DataFormQuestionsOptionDto>>> Execute(RequestDataFormQuestionsOptionDto request)
        {
            try
            {
                var lists = (
                    from s in _context.DataFormQuestionsOption
                    select s).AsQueryable();

                if (request.IsActive == 15) lists = lists.Where(p => p.IsActive == 15);
                
                if (request.IsActive == 14) lists = lists.Where(p => p.IsActive == 14);

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.Text.Contains(request.Search));
                
                if (request.DataFormQuestionsId != null)
                    lists = (
                        from s in _context.DataFormQuestionsOption
                        where s.DataFormQuestionsId == request.DataFormQuestionsId
                        select s
                    );
                
                switch (request.SortOrder)
                {
                    case "Id_D":
                        lists = lists.OrderByDescending(s => s.Id);
                        break;
                    case "Id_A":
                        lists = lists.OrderBy(s => s.Id);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.Id);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {
                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<DataFormQuestionsOptionDto>>
                    {
                        Data = _mapper.Map<IEnumerable<DataFormQuestionsOptionDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };
                }
                else
                {
                    var lists_Res_Pageing = (
                        await Pagination<DataFormQuestionsOption>.CreateAsync(lists.AsNoTracking(), request)
                    );

                    return new ResultDto<IEnumerable<DataFormQuestionsOptionDto>>
                    {
                        Data = _mapper.Map<IEnumerable<DataFormQuestionsOptionDto>>(lists_Res_Pageing),
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
