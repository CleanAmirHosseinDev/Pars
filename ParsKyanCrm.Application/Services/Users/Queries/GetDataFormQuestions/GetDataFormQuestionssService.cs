using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFormQuestions
{

    public class GetDataFormQuestionsService : IGetDataFormQuestionsService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetDataFormQuestionsService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        public async Task<DataFormQuestionsDto> Execute(int? id = null)
        {
            try
            {
                DataFormQuestionsDto res = new DataFormQuestionsDto();

                if (id != null && id != 0)
                {
                    var q_Find = await _context.DataFormQuestions.FirstOrDefaultAsync(p => p.DataFormQuestionId == id.Value);
                    res = _mapper.Map<DataFormQuestionsDto>(q_Find);
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
