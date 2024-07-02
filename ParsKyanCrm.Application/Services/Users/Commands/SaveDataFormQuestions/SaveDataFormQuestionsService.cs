using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Org.BouncyCastle.Asn1.Ocsp;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveDataFormQuestions
{

    public class SaveDataFormQuestionsService : ISaveDataFormQuestionsService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public SaveDataFormQuestionsService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        private bool Check_Remote(DataFormQuestionsDto request)
        {
            try
            {
                string strCondition = string.Empty;

                if (request.DataFormQuestionId == 0)
                {
                    strCondition = "" + nameof(request.DataFormQuestionId) + " = " + request.DataFormQuestionId;
                }
                if (!string.IsNullOrEmpty(strCondition))
                {
                    var q = Ado_NetOperation.GetAll_Table(nameof(DataFormQuestions), "*", strCondition + " AND " + "DataFormQuestionID" + " = " + request.DataFormQuestionId);
                    return q != null && q.Rows.Count > 0 ? true : false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResultDto<DataFormQuestionsDto>> Execute(DataFormQuestionsDto request)
        {
            try
            {
                EntityEntry<DataFormQuestions> q_Entity;
                if (Check_Remote(request) == false)
                {
                    q_Entity = _context.DataFormQuestions.Add(_mapper.Map<DataFormQuestions>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<DataFormQuestionsDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.DataFormQuestions).Name,
                        new Dictionary<string, object>()
                        {
                            {
                                nameof(q_Entity.Entity.DataFormId), request.DataFormId
                            },
                            {
                                nameof(q_Entity.Entity.DataFormType), request.DataFormType
                            },
                            {
                                nameof(q_Entity.Entity.QuestionText), request.QuestionText
                            },
                            {
                                nameof(q_Entity.Entity.QuestionName), request.QuestionName
                            },
                            {
                                nameof(q_Entity.Entity.QuestionType), request.QuestionType
                            },
                            {
                                nameof(q_Entity.Entity.QuestionOrder), request.QuestionOrder
                            },
                            {
                                nameof(q_Entity.Entity.Score), request.Score
                            },
                            {
                                nameof(q_Entity.Entity.HelpText), request.HelpText
                            },
                        }, $"DataFormQuestionId = {request.DataFormQuestionId}");
                }

                return new ResultDto<DataFormQuestionsDto>()
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

        public Task<ResultDto<DataFormQuestionsDto>> ExecuteCopy(string request)
        {
            throw new NotImplementedException();
        }

        // TODO "Check this out"
        //public async Task<ResultDto<DataFormQuestionsDto>> ExecuteCopy(string request)
        //{
        //    try
        //    {
        //        string[] values = request.Split('-');
        //        int newReq = Convert.ToInt32(values[0]);
        //        int OldReq = Convert.ToInt32(values[1]);

        //        #region Validation
        //        #endregion

        //        var con = await Infrastructure.DapperOperation.Run<DataFormQuestionsOptionDto>("select * from DataFormQuestionsOption where Id=" + OldReq);

        //        foreach (var item in con)
        //        {

        //            DataFormQuestionsOptionDto newrequest = new DataFormQuestionsOptionDto();

        //            newrequest.Id = item.Id;
        //            //newrequest.DataFormQuestionsId = newReq;
        //            newrequest.DataFormQuestionsId = item.DataFormQuestionsId;
        //            newrequest.Text = item.Text;
        //            newrequest.Ratio = item.Ratio;

        //            EntityEntry<DataFormQuestionsOption> q_Entity;
        //            if (Check_Remote(newrequest) == false)
        //            {
        //                q_Entity = _context.DataFormQuestionsOption.Add(_mapper.Map<DataFormQuestionsOption>(request));
        //                await _context.SaveChangesAsync();
        //                newrequest = _mapper.Map<DataFormQuestionsOptionDto>(q_Entity.Entity);
        //            }
        //        }

        //        return new ResultDto<DataFormQuestionsOptionDto>()
        //        {
        //            IsSuccess = true,
        //            Message = "ثبت فرم با موفقیت انجام شد",
        //            Data = null
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
