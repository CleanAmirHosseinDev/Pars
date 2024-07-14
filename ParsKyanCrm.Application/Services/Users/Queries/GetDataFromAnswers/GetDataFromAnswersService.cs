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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFromAnswers
{
    public class GetDataFromAnswersService : IGetDataFromAnswersService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetDataFromAnswersService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DataFromAnswersDto> Execute(int? id = null)
        {
            try
            {
                DataFromAnswersDto res = new DataFromAnswersDto();

                if (id != null && id != 0)
                {
                    var q_Find = await _context.DataFromAnswers.FirstOrDefaultAsync(p => p.AnswerId == id);
                    res = _mapper.Map<DataFromAnswersDto>(q_Find);
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
