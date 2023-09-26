using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveManagerOfParsKyan
{

    public class SaveManagerOfParsKyanService : ISaveManagerOfParsKyanService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public SaveManagerOfParsKyanService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<ResultDto<ManagerOfParsKyanDto>> Execute(ManagerOfParsKyanDto request)
        {
            #region Upload Image

            string fileNameOldPic_Picture = string.Empty, path_Picture = string.Empty;
            string fileNameOldPic_ResumeFile = string.Empty, path_ResumeFile = string.Empty;

            #endregion

            try
            {                

                #region Validation



                #endregion

                var qFind = await _context.ManagerOfParsKyan.FindAsync(request.ManagersId);
                request.Picture = qFind != null && !string.IsNullOrEmpty(qFind.Picture) ? qFind.Picture : string.Empty;
                request.ResumeFile = qFind != null && !string.IsNullOrEmpty(qFind.ResumeFile) ? qFind.ResumeFile : string.Empty;

                #region Upload Image

                if (request.Result_Final_Picture != null && request.Result_Final_Picture.Length > 10)
                {
                    fileNameOldPic_Picture = request.Picture;
                    request.Picture = Guid.NewGuid().ToString().Replace("-", "") + ".png";
                    path_Picture = _env.ContentRootPath + VaribleForName.ManagerOfParsKyanFolder + request.Picture;

                    ServiceFileUploader.SaveImageByByte_InExistNextDelete(request.Result_Final_Picture, path_Picture, string.Empty, "تصویر یک");                    
                }

                if (request.Result_Final_ResumeFile != null)
                {
                    fileNameOldPic_ResumeFile = request.ResumeFile;
                    request.ResumeFile = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_ResumeFile.FileName);
                    path_ResumeFile = _env.ContentRootPath + VaribleForName.ManagerOfParsKyanFolder + request.ResumeFile;
                    await ServiceFileUploader.SaveFile(request.Result_Final_ResumeFile, path_ResumeFile, "فایل رزومه");                    
                }

                #endregion

                EntityEntry<ManagerOfParsKyan> q_Entity;
                if (request.ManagersId == 0)
                {
                    request.IsActive = (byte)Common.Enums.TablesGeneralIsActive.Active;
                    request.SaveAndEditDate = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);
                    q_Entity = _context.ManagerOfParsKyan.Add(_mapper.Map<ManagerOfParsKyan>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<ManagerOfParsKyanDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.ManagerOfParsKyan).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.Picture),request.Picture
                        },
                        {
                            nameof(q_Entity.Entity.NameOfManager),request.NameOfManager
                        },
                        {
                            nameof(q_Entity.Entity.PositionId),request.PositionId
                        },
                        {
                            nameof(q_Entity.Entity.TitleId),request.TitleId
                        },
                        {
                            nameof(q_Entity.Entity.EmailAddress),request.EmailAddress
                        },
                        {
                            nameof(q_Entity.Entity.TwitterId),request.TwitterId
                        },
                        {
                            nameof(q_Entity.Entity.ResumeSummary),request.ResumeSummary
                        },
                        {
                            nameof(q_Entity.Entity.ResumeFile),request.ResumeFile
                        },
                        {
                            nameof(q_Entity.Entity.Userid),request.Userid
                        },
                        {
                            nameof(q_Entity.Entity.SaveAndEditDate),DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now)
                        },

                    }, string.Format(nameof(q_Entity.Entity.ManagersId) + " = {0} ", request.ManagersId));

                    #region Upload Image

                    if (request.Result_Final_Picture != null && request.Result_Final_Picture.Length > 10)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.ManagerOfParsKyanFolder + fileNameOldPic_Picture);

                    if (request.Result_Final_ResumeFile != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.ManagerOfParsKyanFolder + fileNameOldPic_ResumeFile);

                    path_Picture = string.Empty;
                    path_ResumeFile = string.Empty;

                    #endregion

                }

                return new ResultDto<ManagerOfParsKyanDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت ساختار مدیریت با موفقیت انجام شد",
                    Data = request
                };


            }
            catch (Exception ex)
            {
                #region Upload Image

                FileOperation.DeleteFile(path_Picture);
                FileOperation.DeleteFile(path_ResumeFile);

                #endregion

                return new ResultDto<ManagerOfParsKyanDto>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

    }

}
