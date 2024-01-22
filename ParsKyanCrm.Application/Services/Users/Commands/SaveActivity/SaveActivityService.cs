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

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveActivity
{

    public class SaveActivityService : ISaveActivityService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public SaveActivityService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<ResultDto<ActivityDto>> Execute(ActivityDto request)
        {
            #region Upload Image

            string fileNameOldPic_Picture1 = string.Empty, path_Picture1 = string.Empty;

            string fileNameOldPic_Picture2 = string.Empty, path_Picture2 = string.Empty;

            #endregion

            try
            {

                #region Validation



                #endregion

                var qFind = await _context.Activity.FindAsync(request.ActivityId);
                request.Picture1 = qFind != null && !string.IsNullOrEmpty(qFind.Picture1) ? qFind.Picture1 : string.Empty;
                request.Picture2 = qFind != null && !string.IsNullOrEmpty(qFind.Picture2) ? qFind.Picture2 : string.Empty;

                #region Upload Image

                if (request.Result_Final_Picture1 != null && request.Result_Final_Picture1.Length > 10)
                {
                    fileNameOldPic_Picture1 = request.Picture1;
                    request.Picture1 = Guid.NewGuid().ToString().Replace("-", "") + ".png";
                    path_Picture1 = _env.ContentRootPath + VaribleForName.ActivityFolderWithwwwroot + request.Picture1;

                    ServiceFileUploader.SaveImageByByte_InExistNextDelete(request.Result_Final_Picture1, path_Picture1, string.Empty, "تصویر یک");
                }

                if (request.Result_Final_Picture2 != null && request.Result_Final_Picture2.Length > 10)
                {
                    fileNameOldPic_Picture2 = request.Picture2;
                    request.Picture2 = Guid.NewGuid().ToString().Replace("-", "") + ".png";
                    path_Picture2 = _env.ContentRootPath + VaribleForName.ActivityFolderWithwwwroot + request.Picture2;

                    ServiceFileUploader.SaveImageByByte_InExistNextDelete(request.Result_Final_Picture2, path_Picture2, string.Empty, "تصویر دو");
                }

                #endregion

                EntityEntry<Activity> q_Entity;
                if (request.ActivityId == 0)
                {
                    request.IsActive = (byte)Common.Enums.TablesGeneralIsActive.Active;
                    q_Entity = _context.Activity.Add(_mapper.Map<Activity>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<ActivityDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.Activity).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.Picture1),request.Picture1
                        },
                        {
                            nameof(q_Entity.Entity.ActivityTitle),request.ActivityTitle
                        },
                        {
                            nameof(q_Entity.Entity.ActivityComment),request.ActivityComment
                        },
                        {
                            nameof(q_Entity.Entity.Picture2),request.Picture2
                        },

                    }, string.Format(nameof(q_Entity.Entity.ActivityId) + " = {0} ", request.ActivityId));

                    #region Upload Image

                    if (request.Result_Final_Picture1 != null && request.Result_Final_Picture1.Length > 10)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.ActivityFolderWithwwwroot + fileNameOldPic_Picture1);

                    if (request.Result_Final_Picture2 != null && request.Result_Final_Picture2.Length > 10)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.ActivityFolderWithwwwroot + fileNameOldPic_Picture2);

                    path_Picture1 = string.Empty;
                    path_Picture2 = string.Empty;

                    #endregion

                }

                return new ResultDto<ActivityDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت فعالیتها با موفقیت انجام شد",
                    Data = request
                };


            }
            catch (Exception ex)
            {
                #region Upload Image

                FileOperation.DeleteFile(path_Picture1);
                FileOperation.DeleteFile(path_Picture2);

                #endregion

                return new ResultDto<ActivityDto>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

    }
}
