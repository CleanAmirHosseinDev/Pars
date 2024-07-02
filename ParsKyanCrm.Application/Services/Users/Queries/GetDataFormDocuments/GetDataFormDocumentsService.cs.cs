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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFormDocuments
{

    public class GetDataFormDocumentsService : IGetDataFormDocumentsService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetDataFormDocumentsService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }
        public async Task<ResultDto<IEnumerable<DataFormDocumentsDto>>> Execute(RequestDataFormDocumentsDto request)
        {
            try
            {
                var lists = (
                    from s in _context.DataFormDocument
                    select s).AsQueryable();

                if (request.IsActive == 15) lists = lists.Where(p => p.IsActive == 15);

                if (request.IsActive == 14) lists = lists.Where(p => p.IsActive == 14);

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(
                    p => p.Title.Contains(request.Search) || p.HelpText.Contains(request.Search));

                //if (request.CategoryId != 0)
                //    lists = (
                //        from s in _context.DataFormDocuments
                //        where s.CategoryId == request.CategoryId
                //        select s
                //    );

                switch (request.SortOrder)
                {
                    case "DataFormDocumentId_D":
                        lists = lists.OrderByDescending(s => s.DataFormDocumentId);
                        break;
                    case "DataFormDocumentId_A":
                        lists = lists.OrderBy(s => s.DataFormDocumentId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.DataFormDocumentId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {
                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<DataFormDocumentsDto>>
                    {
                        Data = _mapper.Map<IEnumerable<DataFormDocumentsDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };
                }
                else
                {
                    var lists_Res_Pageing = 
                        await Pagination<Domain.Entities.DataFormDocument>.CreateAsync(lists.AsNoTracking(), request);

                    return new ResultDto<IEnumerable<DataFormDocumentsDto>>
                    {
                        Data = _mapper.Map<IEnumerable<DataFormDocumentsDto>>(lists_Res_Pageing),
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
