using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveDataFromAnswers
{

    public class SaveDataFromAnswersService : ISaveDataFromAnswersService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        private readonly IWebHostEnvironment _env;
        public SaveDataFromAnswersService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;            
            _env = env;
        }

        private bool Check_Remote(DataFromAnswersDto request)
        {
            try
            {
                string strCondition = string.Empty;
                
                if (request.DataFormQuestionId == 0)
                {
                    strCondition = " " + nameof(request.DataFormQuestionId) + " = " + request.DataFormQuestionId ;
                }
                         

                if (!string.IsNullOrEmpty(strCondition))
                {
                    var q = Ado_NetOperation.GetAll_Table(nameof(DataFromAnswers), "*", strCondition + " AND " + nameof(request.RequestId) + " = " + request.RequestId);
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
                string fileNameOldPic_FileName1 = string.Empty, path_FileName1 = string.Empty;

                var qFind = await _context.DataFromAnswers.FirstOrDefaultAsync(m=>m.RequestId==request.RequestId && m.DataFormQuestionId==request.DataFormQuestionId);
                 request.FileName1 = qFind != null && !string.IsNullOrEmpty(qFind.FileName1) ? qFind.FileName1 : string.Empty;

                #region Upload Image
                if (request.Result_Final_FileName1 != null)
                {
                    fileNameOldPic_FileName1 = request.FileName1;
                    request.FileName1 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_FileName1.FileName);
                    path_FileName1 = _env.ContentRootPath + VaribleForName.CustomerFurtherInfoFolderWithwwwroot + request.FileName1;
                    await ServiceFileUploader.SaveFile(request.Result_Final_FileName1, path_FileName1, "فایل یک");
                }
                #endregion

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
                            nameof(q_Entity.Entity.RequestId),request.RequestId
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
                        {
                            nameof(q_Entity.Entity.Description),request.Description
                        },
                        {
                            nameof(q_Entity.Entity.FileName1),request.FileName1
                        },
                    }, string.Format(nameof(q_Entity.Entity.DataFormQuestionId) + " = {0} and RequestId={1} ", request.DataFormQuestionId,request.RequestId));
                }
                #region Upload Image
                if (request.Result_Final_FileName1 != null)

                    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomerFurtherInfoFolderWithwwwroot + fileNameOldPic_FileName1);

                path_FileName1 = string.Empty;

                #endregion

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

        public async Task<ResultDto<DataFromAnswersDto>> ExecuteCopy(string Request)
        {
            try
            {
                string fileNameOldPic_FileName1 = string.Empty, path_FileName1 = string.Empty;

                string[] values = Request.Split('-');
                int newReq = Convert.ToInt32(values[0]);
                int OldReq = Convert.ToInt32(values[1]);

                #region Validation

                #endregion

                var con = await Infrastructure.DapperOperation.Run<DataFromAnswersDto>("select * from DataFromAnswers where RequestId=" + OldReq);

                  foreach (var item in con)
                {

                    DataFromAnswersDto request = new DataFromAnswersDto();

                    request.FileName1 = item != null && !string.IsNullOrEmpty(item.FileName1) ? item.FileName1 : string.Empty;
                    request.FormId = item.FormId;
                    request.RequestId = newReq;
                    request.DataFormQuestionId = item.DataFormQuestionId;
                    request.Answer = item.Answer;
                    request.Description = item.Description;
                    #region Upload Image

                    if (request.FileName1 != null)
                    {
                        fileNameOldPic_FileName1 = request.FileName1;
                        request.FileName1 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_FileName1);
                        path_FileName1 = _env.ContentRootPath + VaribleForName.CustomerFurtherInfoFolderWithwwwroot + request.FileName1;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomerFurtherInfoFolderWithwwwroot + fileNameOldPic_FileName1, path_FileName1, "فایل یک");
                    }
                    #endregion

                    EntityEntry<DataFromAnswers> q_Entity;
                    if (Check_Remote(request) == false)
                    {
                        q_Entity = _context.DataFromAnswers.Add(_mapper.Map<DataFromAnswers>(request));
                        await _context.SaveChangesAsync();
                        request = _mapper.Map<DataFromAnswersDto>(q_Entity.Entity);
                    }
                }

                return new ResultDto<DataFromAnswersDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت فرم با موفقیت انجام شد",
                    Data = null
                };


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
