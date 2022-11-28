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

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveWorkExperience
{

    public class SaveWorkExperienceService : ISaveWorkExperienceService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IWebHostEnvironment _env;
        public SaveWorkExperienceService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            _env = env;
        }

        public async Task<ResultDto<WorkExperienceDto>> Execute(WorkExperienceDto request)
        {
            #region Upload Image

            string fileNameOldPic_PictureOfEtcHistory = string.Empty, path_PictureOfEtcHistory = string.Empty;

            #endregion

            try
            {

                #region Validation



                #endregion

                #region Upload Image

                if (request.Result_Final_PictureOfEtcHistory != null && request.Result_Final_PictureOfEtcHistory.Length > 10)
                {
                    fileNameOldPic_PictureOfEtcHistory = request.PictureOfEtcHistory;
                    request.PictureOfEtcHistory = Guid.NewGuid().ToString().Replace("-", "") + ".png";
                    path_PictureOfEtcHistory = _env.ContentRootPath + VaribleForName.WorkExperienceFolder + request.PictureOfEtcHistory;

                    string strMessage = ServiceImage.SaveImageByByte_InExistNextDelete(request.Result_Final_PictureOfEtcHistory, path_PictureOfEtcHistory, string.Empty, "تصویر سایر مستندات");
                    if (!string.IsNullOrEmpty(strMessage))
                    {

                        return new ResultDto<WorkExperienceDto>()
                        {
                            IsSuccess = false,
                            Message = strMessage,
                            Data = null
                        };

                    }
                }

                #endregion

                EntityEntry<WorkExperience> q_Entity;
                if (request.SkilsId == 0)
                {
                    request.IsActive = (byte)Common.Enums.TablesGeneralIsActive.Active;
                    request.SaveDate = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);
                    q_Entity = _context.WorkExperience.Add(_mapper.Map<WorkExperience>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<WorkExperienceDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.WorkExperience).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.CustomerId),request.CustomerId
                        },
                        {
                            nameof(q_Entity.Entity.BoardOfDirectorsId),request.BoardOfDirectorsId
                        },
                        {
                            nameof(q_Entity.Entity.InsuranceHistory),request.InsuranceHistory
                        },
                        {
                            nameof(q_Entity.Entity.OfficialNewspaperHistory),request.OfficialNewspaperHistory
                        },
                        {
                            nameof(q_Entity.Entity.EtcHistory),request.EtcHistory
                        },
                        {
                            nameof(q_Entity.Entity.PictureOfEtcHistory),request.PictureOfEtcHistory
                        },

                    }, string.Format(nameof(q_Entity.Entity.SkilsId) + " = {0} ", request.SkilsId));

                    #region Upload Image

                    if (request.Result_Final_PictureOfEtcHistory != null && request.Result_Final_PictureOfEtcHistory.Length > 10)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.WorkExperienceFolder + fileNameOldPic_PictureOfEtcHistory);

                    path_PictureOfEtcHistory = string.Empty;

                    #endregion

                }

                return new ResultDto<WorkExperienceDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت تجارب کاری با موفقیت انجام شد",
                    Data = request
                };


            }
            catch (Exception ex)
            {
                #region Upload Image

                FileOperation.DeleteFile(path_PictureOfEtcHistory);

                #endregion

                throw ex;
            }
        }
    }
}
