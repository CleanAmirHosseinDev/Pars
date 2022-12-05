using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetLicensesAndHonorss
{

    public class GetLicensesAndHonorssService : IGetLicensesAndHonorssService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetLicensesAndHonorssService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<IEnumerable<LicensesAndHonorsDto>>> Execute(RequestLicensesAndHonorsDto request)
        {
            try
            {

                var lists = (from s in _context.LicensesAndHonors                             
                             select s).Include(p => p.User).AsQueryable();

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.Titel.Contains(request.Search));

                switch (request.SortOrder)
                {
                    case "LicensesAndHonorsId_D":
                        lists = lists.OrderByDescending(s => s.LicensesAndHonorsId);
                        break;
                    case "LicensesAndHonorsId_A":
                        lists = lists.OrderBy(s => s.LicensesAndHonorsId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.LicensesAndHonorsId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<LicensesAndHonorsDto>>
                    {
                        Data = _mapper.Map<IEnumerable<LicensesAndHonorsDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };

                }
                else
                {

                    var lists_Res_Pageing = (await Pagination<LicensesAndHonors>.CreateAsync(lists.AsNoTracking(), request));

                    return new ResultDto<IEnumerable<LicensesAndHonorsDto>>
                    {
                        Data = _mapper.Map<IEnumerable<LicensesAndHonorsDto>>(lists_Res_Pageing),
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
