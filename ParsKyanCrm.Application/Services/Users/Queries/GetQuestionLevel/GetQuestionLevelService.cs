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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetQuestionLevel
{

    public class GetQuestionLevelService : IGetQuestionLevelService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetQuestionLevelService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<QuestionLevelDto> Execute(RequestQuestionLevelDto request)
        {
            try
            {

                var res = await (from s in _context.QuestionLevel
                                 where (s.QuestionLevelId == request.QuestionLevelId)
                                 select s).FirstOrDefaultAsync();
                if (res != null)
                {
                    return new QuestionLevelDto()
                    {
                        QuestionLevelId = res.QuestionLevelId,
                        IsActive = 15,
                        LevelTitle = res.LevelTitle,
                        LevelDescription = res.LevelDescription,
                    };
                }
                else
                {
                    return new QuestionLevelDto()
                    {
                        QuestionLevelId = 0,
                        IsActive = 14,
                        LevelTitle = "",
                        LevelDescription = "",
                    };
                }
            }
            catch (Exception ex)
            {
                return new QuestionLevelDto()
                {
                    QuestionLevelId = 0,
                    IsActive = 14,
                    LevelTitle = "",
                    LevelDescription = "",
                };
            }
        }
    }
}
