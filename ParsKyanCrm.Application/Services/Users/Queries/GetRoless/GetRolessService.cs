﻿using AutoMapper;
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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetRoless
{

    public class GetRolessService : IGetRolessService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetRolessService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        public async Task<ResultDto<IEnumerable<RolesDto>>> Execute(RequestRolesDto request)
        {
            try
            {

                var lists = (from s in _context.Roles
                             select s);

                if (!string.IsNullOrEmpty(request.Search)) lists = lists.Where(p => p.RoleDesc.Contains(request.Search) ||
                p.RoleTitle.Contains(request.Search)
                );

                switch (request.SortOrder)
                {
                    case "RoleId_D":
                        lists = lists.OrderByDescending(s => s.RoleId);
                        break;
                    case "RoleId_A":
                        lists = lists.OrderBy(s => s.RoleId);
                        break;
                    default:
                        lists = lists.OrderByDescending(s => s.RoleId);
                        break;
                }

                if (request.PageIndex == 0 && request.PageSize == 0)
                {

                    var res_Lists = await lists.ToListAsync();

                    return new ResultDto<IEnumerable<RolesDto>>
                    {
                        Data = _mapper.Map<IEnumerable<RolesDto>>(res_Lists),
                        IsSuccess = true,
                        Message = string.Empty,
                        Rows = res_Lists.LongCount(),
                    };

                }
                else
                {

                    var list_Res_Pageing = await Pagination<Domain.Entities.Roles>.CreateAsync(lists.AsNoTracking(), request);

                    return new ResultDto<IEnumerable<RolesDto>>
                    {
                        Data = _mapper.Map<IEnumerable<RolesDto>>(list_Res_Pageing),
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
