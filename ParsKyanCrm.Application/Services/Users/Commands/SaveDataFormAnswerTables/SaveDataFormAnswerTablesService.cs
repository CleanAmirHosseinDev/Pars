using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveDataFormAnswerTables
{

    public class SaveDataFormAnswerTablesService : ISaveDataFormAnswerTablesService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        private readonly IWebHostEnvironment _env;

        public SaveDataFormAnswerTablesService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;            
            _env = env;
        }

        public async Task<ResultDto<DataFormAnswerTablesDto>> Execute(DataFormAnswerTablesDto request)
        {
            #region Upload Image

            string fileNameOldPic_FileName1 = string.Empty, path_FileName1 = string.Empty;
            string fileNameOldPic_FileName2 = string.Empty, path_FileName2 = string.Empty;
            string fileNameOldPic_FileName3 = string.Empty, path_FileName3 = string.Empty;
            string fileNameOldPic_FileName4 = string.Empty, path_FileName4 = string.Empty;

            #endregion

            try
            {
                #region Validation



                #endregion

                var qFind = await _context.DataFormAnswerTables.FindAsync(request.AnswerTableId);
                request.FileName1 = qFind != null && !string.IsNullOrEmpty(qFind.FileName1) ? qFind.FileName1 : string.Empty;
                request.FileName2 = qFind != null && !string.IsNullOrEmpty(qFind.FileName2) ? qFind.FileName2 : string.Empty;
                request.FileName3 = qFind != null && !string.IsNullOrEmpty(qFind.FileName3) ? qFind.FileName3 : string.Empty;
                request.FileName4 = qFind != null && !string.IsNullOrEmpty(qFind.FileName4) ? qFind.FileName4 : string.Empty;

                #region Upload Image

                if (request.Result_Final_FileName1 != null)
                {
                    fileNameOldPic_FileName1 = request.FileName1;
                    request.FileName1 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_FileName1.FileName);
                    path_FileName1 = _env.ContentRootPath + VaribleForName.CustomerFurtherInfoFolderWithwwwroot + request.FileName1;
                    await ServiceFileUploader.SaveFile(request.Result_Final_FileName1, path_FileName1, "فایل یک");
                }

                if (request.Result_Final_FileName2 != null)
                {
                    fileNameOldPic_FileName2 = request.FileName2;
                    request.FileName2 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_FileName2.FileName);
                    path_FileName2 = _env.ContentRootPath + VaribleForName.CustomerFurtherInfoFolderWithwwwroot + request.FileName2;
                    await ServiceFileUploader.SaveFile(request.Result_Final_FileName2, path_FileName2, "فایل دو");
                }

                if (request.Result_Final_FileName3 != null)
                {
                    fileNameOldPic_FileName3 = request.FileName3;
                    request.FileName3 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_FileName3.FileName);
                    path_FileName3 = _env.ContentRootPath + VaribleForName.CustomerFurtherInfoFolderWithwwwroot + request.FileName3;
                    await ServiceFileUploader.SaveFile(request.Result_Final_FileName3, path_FileName3, "فایل سه");
                }

                if (request.Result_Final_FileName4 != null)
                {
                    fileNameOldPic_FileName4 = request.FileName4;
                    request.FileName4 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_FileName4.FileName);
                    path_FileName4 = _env.ContentRootPath + VaribleForName.CustomerFurtherInfoFolderWithwwwroot + request.FileName4;
                    await ServiceFileUploader.SaveFile(request.Result_Final_FileName4, path_FileName4, "فایل چهار");
                }

                #endregion

                EntityEntry<DataFormAnswerTables> q_Entity;
                if (request.AnswerTableId == 0)
                {
                    request.IsActive = (byte)Common.Enums.TablesGeneralIsActive.Active;
                    q_Entity = _context.DataFormAnswerTables.Add(_mapper.Map<DataFormAnswerTables>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<DataFormAnswerTablesDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.DataFormAnswerTables).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.FormId),request.FormId
                        },
                        {
                            nameof(q_Entity.Entity.RequestId),request.RequestId
                        },
                        {
                            nameof(q_Entity.Entity.Answer1),request.Answer1
                        },
                        {
                            nameof(q_Entity.Entity.Answer2),request.Answer2
                        },
                        {
                            nameof(q_Entity.Entity.Answer3),request.Answer3
                        },
                        {
                            nameof(q_Entity.Entity.Answer4),request.Answer4
                        },
                        {
                            nameof(q_Entity.Entity.Answer5),request.Answer5
                        },
                        {
                            nameof(q_Entity.Entity.Answer6),request.Answer6
                        },
                        {
                            nameof(q_Entity.Entity.Answer7),request.Answer7
                        },
                        {
                            nameof(q_Entity.Entity.Answer8),request.Answer8
                        },
                        {
                            nameof(q_Entity.Entity.Answer9),request.Answer9
                        },
                        {
                            nameof(q_Entity.Entity.Answer10),request.Answer10
                        },
                        {
                            nameof(q_Entity.Entity.FileName1),request.FileName1
                        },
                        {
                            nameof(q_Entity.Entity.FileName2),request.FileName2
                        },
                        {
                            nameof(q_Entity.Entity.FileName3),request.FileName3
                        },
                        {
                            nameof(q_Entity.Entity.FileName4),request.FileName4
                        }
                    }, string.Format(nameof(q_Entity.Entity.AnswerTableId) + " = {0} ", request.AnswerTableId));
                    #region Upload Image

                    if (request.Result_Final_FileName1 != null)
                        
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomerFurtherInfoFolderWithwwwroot + fileNameOldPic_FileName1);

                    if (request.Result_Final_FileName2 != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomerFurtherInfoFolderWithwwwroot + fileNameOldPic_FileName2);

                    if (request.Result_Final_FileName3 != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomerFurtherInfoFolderWithwwwroot + fileNameOldPic_FileName3);

                    if (request.Result_Final_FileName4 != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomerFurtherInfoFolderWithwwwroot + fileNameOldPic_FileName4);

                 
                    path_FileName1 = string.Empty;
                    path_FileName2 = string.Empty;
                    path_FileName3 = string.Empty;
                    path_FileName4 = string.Empty;


                    #endregion
                }

                return new ResultDto<DataFormAnswerTablesDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت فرم با موفقیت انجام شد",
                    Data = request
                };


            }
            catch (Exception ex)
            {
                #region Upload Image

                FileOperation.DeleteFile(path_FileName1);
                FileOperation.DeleteFile(path_FileName2);
                FileOperation.DeleteFile(path_FileName3);
                FileOperation.DeleteFile(path_FileName4);

                #endregion
                throw ex;
            }
        }
    }
}
