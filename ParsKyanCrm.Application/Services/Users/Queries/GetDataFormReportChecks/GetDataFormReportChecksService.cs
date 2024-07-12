using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Application.Services.Users.Queries.GetDataFormReportChecks;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFormReportChecks
{
    public class GetDataFormReportChecksService : IGetDataFormReportChecksService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetDataFormReportChecksService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        public async Task<ResultDto<IEnumerable<DataFormReportCheckDto>>> Execute(RequestDataFormReportCheckDto request)
        {
            try
            {
                var lists = (
                    from s in _context.DataFormReportCheck
                    where (
                        s.IsActive == 15 &&
                        s.CheckId == request.CheckId ||
                        s.QuestionId == request.QuestionId && s.FormId == request.FormId ||
                        s.RequestId == request.RequestId && s.DocumentId == request.DocumentId ||
                        s.AnswerId == request.AnswerId ||
                        s.CategoryId == request.CategoryId && s.QuestionId == request.QuestionId
                    )
                    select s
                );

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(
                    p => p.FormCode.Contains(request.Search) || p.AnswerAfterEdit.Contains(request.Search) || p.AnswerBeforEdit.Contains(request.Search) ||
                         p.CostumerDescriptionAfterEdit.Contains(request.Search) || p.CostumerDescriptionBeforEdit.Contains(request.Search) ||
                         p.SuperVisorDescription.Contains(request.Search)
                );

                switch (request.SortOrder)
                {
                    case "CheckId_D":
                        lists = lists.OrderByDescending(s => s.CheckId);
                        break;
                    case "CheckId_A":
                        lists = lists.OrderBy(s => s.CheckId);
                        break;
                    case "QuestionId_D":
                        lists = lists.OrderByDescending(s => s.QuestionId);
                        break;
                    case "QuestionId_A":
                        lists = lists.OrderBy(s => s.QuestionId);
                        break;
                    case "FormId_D":
                        lists = lists.OrderByDescending(s => s.FormId);
                        break;
                    case "FormId_A":
                        lists = lists.OrderBy(s => s.FormId);
                        break;
                    case "RequestId_D":
                        lists = lists.OrderByDescending(s => s.RequestId);
                        break;
                    case "RequestId_A":
                        lists = lists.OrderBy(s => s.RequestId);
                        break;
                    case "DocumentId_D":
                        lists = lists.OrderByDescending(s => s.DocumentId);
                        break;
                    case "DocumentId_A":
                        lists = lists.OrderBy(s => s.DocumentId);
                        break;
                    case "AnswerId_D":
                        lists = lists.OrderByDescending(s => s.AnswerId);
                        break;
                    case "AnswerId_A":
                        lists = lists.OrderBy(s => s.AnswerId);
                        break;
                    case "CategoryId_D":
                        lists = lists.OrderByDescending(s => s.CategoryId);
                        break;
                    case "CategoryId_A":
                        lists = lists.OrderBy(s => s.CategoryId);
                        break;
                    case "FormCode_D":
                        lists = lists.OrderByDescending(s => s.FormCode);
                        break;
                    case "FormCode_A":
                        lists = lists.OrderBy(s => s.FormCode);
                        break;
                    default:
                        lists = lists.OrderBy(s => s.CheckId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<DataFormReportCheckDto>>
                    {
                        Data = _mapper.Map<IEnumerable<DataFormReportCheckDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };
                }
                else
                {
                    var list_Res_Pageing = await Pagination<Domain.Entities.DataFormReportCheck>.CreateAsync(lists.AsNoTracking(), request);

                    return new ResultDto<IEnumerable<DataFormReportCheckDto>>
                    {
                        Data = _mapper.Map<IEnumerable<DataFormReportCheckDto>>(list_Res_Pageing),
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
