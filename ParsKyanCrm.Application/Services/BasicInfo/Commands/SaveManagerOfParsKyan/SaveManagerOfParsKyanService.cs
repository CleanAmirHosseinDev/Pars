using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ParsKyanCrm.Application.Dtos.BasicInfo;
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

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveManagerOfParsKyan
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

        public async Task<ResultDto<ManagerOfParsKyanDto>> Execute(ManagerOfParsKyanDto request, IFormCollection formCollection)
        {
            #region Upload Image

            string fileNameOldPic_Picture = string.Empty, path_Picture = string.Empty;

            #endregion

            try
            {

                request.ManagersId = int.Parse(formCollection.FirstOrDefault(p=>p.Key == "ManagersId").Value);

                #region Validation



                #endregion

                var qFind = await _context.ManagerOfParsKyan.FindAsync(request.ManagersId);
                request.Picture = qFind != null && !string.IsNullOrEmpty(qFind.Picture) ? qFind.Picture : string.Empty;

                #region Upload Image

                if (request.Result_Final_Picture != null && request.Result_Final_Picture.Length > 10)
                {
                    fileNameOldPic_Picture = request.Picture;
                    request.Picture = Guid.NewGuid().ToString().Replace("-", "") + ".png";
                    path_Picture = _env.ContentRootPath + VaribleForName.ManagerOfParsKyanFolder + request.Picture;

                    string strMessage = ServiceFileUploader.SaveImageByByte_InExistNextDelete(request.Result_Final_Picture, path_Picture, string.Empty, "تصویر یک");
                    if (!string.IsNullOrEmpty(strMessage))
                    {

                        return new ResultDto<ManagerOfParsKyanDto>()
                        {
                            IsSuccess = false,
                            Message = strMessage,
                            Data = null
                        };

                    }
                }

                #endregion

                EntityEntry<ManagerOfParsKyan> q_Entity;
                if (request.ManagersId == 0)
                {
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

                    path_Picture = string.Empty;

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

                #endregion

                throw ex;
            }
        }

    }

}
