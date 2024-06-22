using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure.Consts;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveDataForm
{
    public class SaveDataFormService : ISaveDataFormService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public SaveDataFormService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private bool Check_Remote(DataFormsDto request)
        {
            try
            {
                string strCondition = string.Empty;

                if (request.FormId != 0)
                {
                    strCondition = "" + nameof(request.FormId) + " = " + request.FormId;
                }


                if (!string.IsNullOrEmpty(strCondition))
                {
                    var q = Ado_NetOperation.GetAll_Table(nameof(DataForms), "*", strCondition);
                    return q != null && q.Rows.Count > 0 ? true : false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResultDto<DataFormsDto>> Execute(DataFormsDto request)
        {
            try
            {
                EntityEntry<DataForms> q_Entity;
                if (Check_Remote(request) == false)
                {
                    q_Entity = _context.DataForms.Add(_mapper.Map<DataForms>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<DataFormsDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(nameof(DataForms), new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.FormId),request.FormId
                        },
                        {
                            nameof(q_Entity.Entity.FormTitle),request.FormTitle
                        },
                        {
                            nameof(q_Entity.Entity.CategoryId),request.CategoryId
                        },
                        {
                            nameof(q_Entity.Entity.IsTable),request.IsTable
                        }
                    }, nameof(q_Entity.Entity.FormId) + $" = {request.FormId}");
                }

                return new ResultDto<DataFormsDto>()
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

        //public async Task<ResultDto<DataFormsDto>> ExecutdeCopy(string Request)
        //{
        //try
        //{
        //    string fileNameOldPic_FileName1 = string.Empty, path_FileName1 = string.Empty;

        //    string[] values = Request.Split('-');
        //    int newReq = Convert.ToInt32(values[0]);
        //    int OldReq = Convert.ToInt32(values[1]);

        //    var con = await Infrastructure.DapperOperation.Run<DataFromAnswersDto>("select * from DataFromAnswers where RequestId=" + OldReq);

        //    foreach (var item in con)
        //    {

        //        DataFromAnswersDto request = new DataFromAnswersDto();

        //        request.FileName1 = item != null && !string.IsNullOrEmpty(item.FileName1) ? item.FileName1 : string.Empty;
        //        request.FormId = item.FormId;
        //        request.RequestId = newReq;
        //        request.DataFormQuestionId = item.DataFormQuestionId;
        //        request.Answer = item.Answer;
        //        #region Upload Image

        //        if (request.FileName1 != null)
        //        {
        //            fileNameOldPic_FileName1 = request.FileName1;
        //            request.FileName1 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_FileName1);
        //            path_FileName1 = _env.ContentRootPath + VaribleForName.CustomerFurtherInfoFolderWithwwwroot + request.FileName1;
        //            await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomerFurtherInfoFolderWithwwwroot + fileNameOldPic_FileName1, path_FileName1, "فایل یک");
        //        }
        //        #endregion

        //        EntityEntry<DataFromAnswers> q_Entity;
        //        if (Check_Remote(request) == false)
        //        {
        //            q_Entity = _context.DataFromAnswers.Add(_mapper.Map<DataFromAnswers>(request));
        //            await _context.SaveChangesAsync();
        //            request = _mapper.Map<DataFromAnswersDto>(q_Entity.Entity);
        //        }
        //    }

        //    return new ResultDto<DataFromAnswersDto>()
        //    {
        //        IsSuccess = true,
        //        Message = "ثبت فرم با موفقیت انجام شد",
        //        Data = null
        //    };


        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
        //}

        public Task<ResultDto<DataFormsDto>> ExecuteCopy(string Request)
        {
            throw new NotImplementedException();
        }
    }
}
