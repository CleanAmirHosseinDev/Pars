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

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveEducationCourses
{

    public class SaveEducationCoursesService : ISaveEducationCoursesService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IWebHostEnvironment _env;
        public SaveEducationCoursesService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            _env = env;
        }

        public async Task<ResultDto<EducationCoursesDto>> Execute(EducationCoursesDto request)
        {
            #region Upload Image

            string fileNameOldPic_PictureOfEducationCourse = string.Empty, path_PictureOfEducationCourse = string.Empty;

            #endregion

            try
            {

                #region Validation



                #endregion

                #region Upload Image

                if (request.Result_Final_PictureOfEducationCourse != null && request.Result_Final_PictureOfEducationCourse.Length > 10)
                {
                    fileNameOldPic_PictureOfEducationCourse = request.PictureOfEducationCourse;
                    request.PictureOfEducationCourse = Guid.NewGuid().ToString().Replace("-", "") + ".png";
                    path_PictureOfEducationCourse = _env.ContentRootPath + VaribleForName.EducationCoursesFolder + request.PictureOfEducationCourse;

                    string strMessage = ServiceImage.SaveImageByByte_InExistNextDelete(request.Result_Final_PictureOfEducationCourse, path_PictureOfEducationCourse, string.Empty, "تصویر دوره آموزشی");
                    if (!string.IsNullOrEmpty(strMessage))
                    {

                        return new ResultDto<EducationCoursesDto>()
                        {
                            IsSuccess = false,
                            Message = strMessage,
                            Data = null
                        };

                    }
                }

                #endregion

                EntityEntry<EducationCourses> q_Entity;
                if (request.EducationCoursesId == 0)
                {
                    request.IsActive = (byte)Common.Enums.TablesGeneralIsActive.Active;
                    request.SaveDate = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);
                    q_Entity = _context.EducationCourses.Add(_mapper.Map<EducationCourses>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<EducationCoursesDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.EducationCourses).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.CustomerId),request.CustomerId
                        },
                        {
                            nameof(q_Entity.Entity.BoardOfDirectorsId),request.BoardOfDirectorsId
                        },                        
                        {
                            nameof(q_Entity.Entity.PictureOfEducationCourse),request.PictureOfEducationCourse
                        },
                        {
                            nameof(q_Entity.Entity.TitelCourses),request.TitelCourses
                        },
                        {
                            nameof(q_Entity.Entity.TimeOfCource),request.TimeOfCource
                        },
                        {
                            nameof(q_Entity.Entity.PlaceOfTraining),request.PlaceOfTraining
                        },
                        {
                            nameof(q_Entity.Entity.CourseOrganizer),request.CourseOrganizer
                        },

                    }, string.Format(nameof(q_Entity.Entity.EducationCoursesId) + " = {0} ", request.EducationCoursesId));

                    #region Upload Image

                    if (request.Result_Final_PictureOfEducationCourse != null && request.Result_Final_PictureOfEducationCourse.Length > 10)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.EducationCoursesFolder + fileNameOldPic_PictureOfEducationCourse);

                    path_PictureOfEducationCourse = string.Empty;

                    #endregion

                }

                return new ResultDto<EducationCoursesDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت دوره های آموزشی با موفقیت انجام شد",
                    Data = request
                };


            }
            catch (Exception ex)
            {
                #region Upload Image

                FileOperation.DeleteFile(path_PictureOfEducationCourse);

                #endregion

                throw ex;
            }
        }

    }
}
