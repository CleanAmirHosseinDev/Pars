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
using Microsoft.AspNetCore.Hosting;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveDataFormReportCheck
{
    public class SaveDataFormReportCheckService : ISaveDataFormReportCheckService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public SaveDataFormReportCheckService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        private bool Check_Remote(DataFormReportCheckDto request)
        {
            try
            {
                string strCondition = string.Empty;

                if (request.QuestionId != 0 && request.FormId != 0)
                {
                    strCondition = "" + nameof(request.RequestId) + " = " + request.RequestId + " AND " + nameof(request.QuestionId) + $" = {request.QuestionId}" + " AND " + nameof(request.FormId) + " = " + request.FormId;
                }
                else if (request.DocumentId != 0)
                {
                    strCondition = "" + nameof(request.RequestId) + " = " + request.RequestId + " AND " + nameof(request.DocumentId) + $" = {request.DocumentId}";
                }
                if (!string.IsNullOrEmpty(strCondition))
                {
                    var q = Ado_NetOperation.GetAll_Table(nameof(DataFormReportCheck), "*", strCondition);
                    return q != null && q.Rows.Count > 0;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ResultDto<DataFormReportCheckDto>> Execute(DataFormReportCheckDto request)
        {
            try
            {
                string fileNameOldPic_FileName1 = string.Empty, path_FileName1 = string.Empty;

                var qFind = await _context.DataFromAnswers.FirstOrDefaultAsync(m => m.RequestId == request.RequestId && m.DataFormDocumentId == request.DocumentId);
                request.Document = qFind != null && !string.IsNullOrEmpty(qFind.FileName1) ? qFind.FileName1 : string.Empty;

                #region Upload Image
                if (request.DocumentFile != null)
                {
                    fileNameOldPic_FileName1 = request.Document;
                    request.Document = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.DocumentFile.FileName);
                    path_FileName1 = _env.ContentRootPath + VaribleForName.CustomerFurtherInfoFolderWithwwwroot + request.Document;
                    await ServiceFileUploader.SaveFile(request.DocumentFile, path_FileName1, "فایل یک");
                }
                #endregion

                int CheckId = 0;
                EntityEntry<DataFormReportCheck> q_Entity;
                if (Check_Remote(request) == false)
                {
                    request.IsActive = 15;
                    q_Entity = _context.DataFormReportCheck.Add(_mapper.Map<DataFormReportCheck>(request));
                    await _context.SaveChangesAsync();
                    CheckId = q_Entity.Entity.CheckId;
                    request = _mapper.Map<DataFormReportCheckDto>(q_Entity.Entity);
                }
                else
                {
                    if (request.QuestionId != 0 && request.FormId != 0)
                    {
                        Ado_NetOperation.SqlUpdate(nameof(DataFormReportCheck), new Dictionary<string, object>()
                        {
                            {
                                nameof(q_Entity.Entity.AnswerBeforEdit),request.AnswerBeforEdit
                            },
                            {
                                nameof(q_Entity.Entity.AnswerAfterEdit),request.AnswerAfterEdit
                            },
                            {
                                nameof(q_Entity.Entity.Document),request.Document
                            },
                            {
                                nameof(q_Entity.Entity.SuperVisorDescription),request.SuperVisorDescription
                            },
                            {
                                nameof(q_Entity.Entity.CostumerDescriptionBeforEdit),request.CostumerDescriptionBeforEdit
                            },
                            {
                                nameof(q_Entity.Entity.CostumerDescriptionAfterEdit),request.CostumerDescriptionAfterEdit
                            },
                        }, $"{nameof(request.QuestionId)} = {request.QuestionId} AND {nameof(request.FormId)} = {request.FormId} AND {nameof(request.RequestId)} = {request.RequestId}");
                    }
                    else if (request.DocumentId != 0)
                    {
                        Ado_NetOperation.SqlUpdate(nameof(DataFormReportCheck), new Dictionary<string, object>()
                            {
                                {
                                    nameof(q_Entity.Entity.AnswerBeforEdit),request.AnswerBeforEdit
                                },
                                {
                                    nameof(q_Entity.Entity.AnswerAfterEdit),request.AnswerAfterEdit
                                },
                                {
                                    nameof(q_Entity.Entity.Document),request.Document
                                },
                                {
                                    nameof(q_Entity.Entity.SuperVisorDescription),request.SuperVisorDescription
                                },
                                {
                                    nameof(q_Entity.Entity.CostumerDescriptionBeforEdit),request.CostumerDescriptionBeforEdit
                                },
                                {
                                    nameof(q_Entity.Entity.CostumerDescriptionAfterEdit),request.CostumerDescriptionAfterEdit
                                },
                            }, nameof(request.DocumentId) + " = " + request.DocumentId + " AND " + nameof(request.RequestId) + $" = {request.RequestId}");
                    }
                }

                #region Upload Image
                if (request.DocumentFile != null)
                    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomerFurtherInfoFolderWithwwwroot + fileNameOldPic_FileName1);
                path_FileName1 = string.Empty;
                #endregion

                return new ResultDto<DataFormReportCheckDto>()
                {
                    DataId = CheckId,
                    IsSuccess = true,
                    Message = "ثبت فرم با موفقیت انجام شد",
                    Data = request,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
