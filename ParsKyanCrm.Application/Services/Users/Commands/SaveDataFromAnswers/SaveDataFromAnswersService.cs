using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveDataFromAnswers
{

    public class SaveDataFromAnswersService : ISaveDataFromAnswersService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IWebHostEnvironment _env;
        public SaveDataFromAnswersService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            _env = env;
        }

        public async Task<ResultDto<DataFromAnswersDto>> Execute(DataFromAnswersDto request)
        {
            try
            {
                #region Validation



                #endregion

                EntityEntry<DataFromAnswers> q_Entity;
                if (request.AnswerId == 0)
                {                    
                    q_Entity = _context.DataFromAnswers.Add(_mapper.Map<DataFromAnswers>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<DataFromAnswersDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.DataFromAnswers).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.CustomerId),request.CustomerId
                        },
                        {
                            nameof(q_Entity.Entity.FormId),request.FormId
                        },
                        {
                            nameof(q_Entity.Entity.DataFormQuestionId),request.DataFormQuestionId
                        },
                        {
                            nameof(q_Entity.Entity.Answer),request.Answer
                        },
                    }, string.Format(nameof(q_Entity.Entity.AnswerId) + " = {0} ", request.AnswerId));
                }

                return new ResultDto<DataFromAnswersDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت فرم با موفقیت انجام شد",
                    Data = request
                };


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
