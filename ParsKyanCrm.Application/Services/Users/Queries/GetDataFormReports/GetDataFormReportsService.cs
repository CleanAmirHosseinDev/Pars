using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Application.Services.Users.Queries.GetDataFromReports;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFromReports
{
    public class GetDataFormReportsService : IGetDataFormReportsService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetDataFormReportsService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        public async Task<ResultDto<IEnumerable<DataFormReportDto>>> Execute(RequestDataFormReportDto request)
        {
            try
            {
                var lists = (from s in _context.DataFormReport
                    where (s.DataReportId == request.DataReportId || s.RequestId == request.RequestId && s.DataFormAnswerId == request.DataFormAnswerId)
                    select s);

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.Description.Contains(request.Search));

                switch (request.SortOrder)
                {
                    case "DataReportId_D":
                        lists = lists.OrderByDescending(s => s.DataReportId);
                        break;
                    case "DataReportId_A":
                        lists = lists.OrderBy(s => s.DataReportId);
                        break;
                    case "DataFormAnswerId_D":
                        lists = lists.OrderByDescending(s => s.DataFormAnswerId);
                        break;
                    case "DataFormAnswerId_A":
                        lists = lists.OrderBy(s => s.DataFormAnswerId);
                        break;
                    case "RequestId_D":
                        lists = lists.OrderByDescending(s => s.RequestId);
                        break;
                    case "RequestId_A":
                        lists = lists.OrderBy(s => s.RequestId);
                        break;
                    default:
                        lists = lists.OrderBy(s => s.DataReportId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<DataFormReportDto>>
                    {
                        Data = _mapper.Map<IEnumerable<DataFormReportDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };
                }
                else
                {

                    var list_Res_Pageing = await Pagination<Domain.Entities.DataFormReport>.CreateAsync(lists.AsNoTracking(), request);

                    return new ResultDto<IEnumerable<DataFormReportDto>>
                    {
                        Data = _mapper.Map<IEnumerable<DataFormReportDto>>(list_Res_Pageing),
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
