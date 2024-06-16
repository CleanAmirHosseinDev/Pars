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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveNewsAndContent
{

    public class SaveNewsAndContentService : ISaveNewsAndContentService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public SaveNewsAndContentService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<ResultDto<NewsAndContentDto>> Execute(NewsAndContentDto request)
        {
            #region Upload Image

            string fileNameOldPic_ContentPic = string.Empty, path_ContentPic = string.Empty;

            #endregion

            try
            {

                #region Validation



                #endregion

                var qFind = await _context.NewsAndContent.FindAsync(request.ContentId);
                request.ContentPic = qFind != null && !string.IsNullOrEmpty(qFind.ContentPic) ? qFind.ContentPic : string.Empty;

                #region Upload Image

                if (request.Result_Final_ContentPic != null && request.Result_Final_ContentPic.Length > 10)
                {
                    fileNameOldPic_ContentPic = request.ContentPic;
                    request.ContentPic = Guid.NewGuid().ToString().Replace("-", "") + ".png";
                    path_ContentPic = _env.ContentRootPath + VaribleForName.NewsAndContentFolderWithwwwroot + request.ContentPic;

                    ServiceFileUploader.SaveImageByByte_InExistNextDelete(request.Result_Final_ContentPic, path_ContentPic, string.Empty, "تصویر محتوا");
                }

                #endregion

                EntityEntry<NewsAndContent> q_Entity;
                if (request.ContentId == 0)
                {
                    request.IsActive = (byte)Common.Enums.TablesGeneralIsActive.Active;
                    //request.DateSave = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);
                    q_Entity = _context.NewsAndContent.Add(_mapper.Map<NewsAndContent>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<NewsAndContentDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.NewsAndContent).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.ContentPic),request.ContentPic
                        },
                        {
                            nameof(q_Entity.Entity.Title),request.Title
                        },                        
                        {
                            nameof(q_Entity.Entity.UserId),request.UserId
                        },
                        //{
                        //    nameof(q_Entity.Entity.DateSave),DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now)
                        //},
                        {
                            nameof(q_Entity.Entity.KindOfContent),request.KindOfContent
                        },
                        {
                            nameof(q_Entity.Entity.Body),request.Body
                        },
                        {
                            nameof(q_Entity.Entity.Summary),request.Summary
                        },
                        {
                            nameof(q_Entity.Entity.MeteDesc),request.MeteDesc
                        },
                        {
                            nameof(q_Entity.Entity.Keywords),request.Keywords
                        },{
                            nameof(q_Entity.Entity.DirectLink),request.DirectLink
                        },

                    }, string.Format(nameof(q_Entity.Entity.ContentId) + " = {0} ", request.ContentId));

                    #region Upload Image

                    if (request.Result_Final_ContentPic != null && request.Result_Final_ContentPic.Length > 10)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.NewsAndContentFolderWithwwwroot + fileNameOldPic_ContentPic);


                    path_ContentPic = string.Empty;

                    #endregion

                }

                return new ResultDto<NewsAndContentDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت اخبار و محتوا با موفقیت انجام شد",
                    Data = request
                };


            }
            catch (Exception ex)
            {
                #region Upload Image

                FileOperation.DeleteFile(path_ContentPic);

                #endregion

                return new ResultDto<NewsAndContentDto>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

    }
}
