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

        private bool Check_Remote(DataFromAnswersDto request)
        {
            try
            {
                string strCondition = string.Empty;
                
                if (request.DataFormQuestionId!=0)
                {
                    strCondition = " " + nameof(request.DataFormQuestionId) + " = " + request.DataFormQuestionId ;
                }
                         

                if (!string.IsNullOrEmpty(strCondition))
                {
                    var q = Ado_NetOperation.GetAll_Table(typeof(Domain.Entities.DataFromAnswers).Name, "*", strCondition + " AND " + nameof(request.CustomerId) + " = " + request.CustomerId);
                    return q != null && q.Rows.Count > 0 ? true : false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public async Task<ResultDto<DataFromAnswersDto>> Execute(DataFromAnswersDto request)
        {
            try
            {
                #region Upload Image

                string fileNameOldPic_FileName1 = string.Empty, path_FileName1 = string.Empty;

                #endregion

                #region Validation



                #endregion

                 var qFind = await _context.DataFromAnswers.FindAsync(request.AnswerId);
               // var qFind = await _context.DataFromAnswers.FirstOrDefault(m=>m.CustomerId==request.CustomerId&& m.DataFormQuestionId==request.DataFormQuestionId);

                request.FileName1 = qFind != null && !string.IsNullOrEmpty(qFind.FileName1) ? qFind.FileName1 : string.Empty;
                

                EntityEntry<DataFromAnswers> q_Entity;
                if (Check_Remote(request) == false)
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
                    }, string.Format(nameof(q_Entity.Entity.DataFormQuestionId) + " = {0} and CustomerId={1} ", request.DataFormQuestionId,request.CustomerId));
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
