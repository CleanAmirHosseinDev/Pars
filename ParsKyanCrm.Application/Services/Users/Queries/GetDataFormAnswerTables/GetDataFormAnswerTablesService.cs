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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFormAnswerTables
{

    public class GetDataFormAnswerTablesService : IGetDataFormAnswerTablesService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetDataFormAnswerTablesService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        public async Task<DataFormAnswerTablesDto> Execute(int? id = null)
        {
          
                try
                {

                   DataFormAnswerTablesDto res = new DataFormAnswerTablesDto();
                   
                    if (id != null && id != 0)
                    {
                        var q_Find = await _context.DataFormAnswerTables.FindAsync(id);
                        res = _mapper.Map<DataFormAnswerTablesDto>(q_Find);
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
