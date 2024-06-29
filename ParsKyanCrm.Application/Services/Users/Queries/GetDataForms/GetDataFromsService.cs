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
using ParsKyanCrm.Domain.Entities;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataForms
{
    public class GetDataFormsService : IGetDataFormsService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetDataFormsService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        public async Task<ResultDto<IEnumerable<DataFormsDto>>> Execute(RequestDataFormsDto request)
        {
            try
            {

                var lists = (
                    from s in _context.DataForms
                     where (s.IsActive == 15 && s.CategoryId != null)
                     select s
                    ).AsQueryable();

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.FormTitle.Contains(request.Search));

                switch (request.SortOrder)
                {
                    case "FormId_D":
                        lists = lists.OrderByDescending(s => s.FormId);
                        break;
                    case "FormId_A":
                        lists = lists.OrderBy(s => s.FormId);
                        break;
                    case "FormTitle_D":
                        lists = lists.OrderByDescending(s => s.FormTitle);
                        break;
                    case "FormTitle_A":
                        lists = lists.OrderBy(s => s.FormTitle);
                        break;
                    case "CategoryId_D":
                        lists = lists.OrderByDescending(s => s.CategoryId);
                        break;
                    case "CategoryId_A":
                        lists = lists.OrderBy(s => s.CategoryId);
                        break;
                    default:
                        lists = lists = lists.OrderBy(s => s.FormTitle);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<DataFormsDto>>
                    {
                        Data = _mapper.Map<IEnumerable<DataFormsDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };
                }
                else
                {
                    var lists_Res_Pageing = (await Pagination<DataForms>.CreateAsync(lists.AsNoTracking(), request));

                    return new ResultDto<IEnumerable<DataFormsDto>>
                    {
                        Data = _mapper.Map<IEnumerable<DataFormsDto>>(lists_Res_Pageing),
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
