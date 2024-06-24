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

        public async Task<DataFormQuestionsOptionDto> Execute(int? id = null)
        {
            try
            {
                DataFormQuestionsOptionDto res = new DataFormQuestionsOptionDto();
                if (id != null && id != 0)
                {
                    var q_Find = await _context.DataFormQuestionsOption.FirstOrDefaultAsync(p => p.DataFormQuestionsId == id.Value);
                    res = _mapper.Map<DataFormQuestionsOptionDto>(q_Find);
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
