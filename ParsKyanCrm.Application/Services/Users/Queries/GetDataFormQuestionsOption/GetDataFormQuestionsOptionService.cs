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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFormQuestionsOption
{

    public class GetDataFormQuestionsOptionService : IGetDataFormQuestionsOptionService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetDataFormQuestionsOptionService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<IEnumerable<DataFormQuestionsOptionDto>>> Execute(RequestDataFormQuestionsOptionDto request)
        {
            try
            {
                List<DataFormQuestionsOptionDto> res = new List<DataFormQuestionsOptionDto>();
                var lists = (from s in _context.DataFormQuestionsOption
                             where (s.DataFormQuestionsId == request.DataFormQuestionsId)
                             select s);

                res = _mapper.Map<List<DataFormQuestionsOptionDto>>(await lists.ToListAsync());

                return new ResultDto<IEnumerable<DataFormQuestionsOptionDto>>
                {
                    Data = res,
                    IsSuccess = true,
                    Message = null,
                    Rows = lists.Count()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
