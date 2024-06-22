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
                    where (s.IsActive == 15)
                    select s
                );

                var res_Lists = await lists.ToListAsync();

                return new ResultDto<IEnumerable<DataFormsDto>>
                {
                    Data = _mapper.Map<IEnumerable<DataFormsDto>>(res_Lists),
                    IsSuccess = true,
                    Message = string.Empty,
                    Rows = res_Lists.LongCount(),
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
