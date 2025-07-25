﻿using AutoMapper;
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

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveBoardOfDirectors
{

    public class SaveBoardOfDirectorsService : ISaveBoardOfDirectorsService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        private readonly IWebHostEnvironment _env;
        public SaveBoardOfDirectorsService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;            
            _env = env;
        }

        public async Task<ResultDto<BoardOfDirectorsDto>> Execute(BoardOfDirectorsDto request)
        {
            #region Upload Image

            string fileNameOldPic_AcademicDegreePicture = string.Empty, path_AcademicDegreePicture = string.Empty;

            string fileNameOldPic_PictureOfEducationCourse = string.Empty, path_PictureOfEducationCourse = string.Empty;

            #endregion

            try
            {

                #region Validation



                #endregion

                var qFind = await _context.BoardOfDirectors.FindAsync(request.BoardOfDirectorsId);
                request.AcademicDegreePicture = qFind != null && !string.IsNullOrEmpty(qFind.AcademicDegreePicture) ? qFind.AcademicDegreePicture : string.Empty;
                request.PictureOfEducationCourse = qFind != null && !string.IsNullOrEmpty(qFind.PictureOfEducationCourse) ? qFind.PictureOfEducationCourse : string.Empty;

                #region Upload Image

                if (request.Result_Final_AcademicDegreePicture != null && request.Result_Final_AcademicDegreePicture.Length > 10)
                {
                    fileNameOldPic_AcademicDegreePicture = request.AcademicDegreePicture;
                    request.AcademicDegreePicture = Guid.NewGuid().ToString().Replace("-", "") + ".png";
                    path_AcademicDegreePicture = _env.ContentRootPath + VaribleForName.BoardOfDirectorsFolderWithwwwroot + request.AcademicDegreePicture;

                    ServiceFileUploader.SaveImageByByte_InExistNextDelete(request.Result_Final_AcademicDegreePicture, path_AcademicDegreePicture, string.Empty, "تصویر مدرک تحصیلی");
                }

                if (request.Result_Final_PictureOfEducationCourse != null && request.Result_Final_PictureOfEducationCourse.Length > 10)
                {
                    fileNameOldPic_PictureOfEducationCourse = request.PictureOfEducationCourse;
                    request.PictureOfEducationCourse = Guid.NewGuid().ToString().Replace("-", "") + ".png";
                    path_PictureOfEducationCourse = _env.ContentRootPath + VaribleForName.BoardOfDirectorsFolderWithwwwroot + request.PictureOfEducationCourse;

                    ServiceFileUploader.SaveImageByByte_InExistNextDelete(request.Result_Final_PictureOfEducationCourse, path_PictureOfEducationCourse, string.Empty, "تصویر دوره آموزشی");
                    
                }

                #endregion

                EntityEntry<BoardOfDirectors> q_Entity;
                if (request.BoardOfDirectorsId == 0)
                {
                    request.IsActive = (byte)Common.Enums.TablesGeneralIsActive.Active;
                    request.SaveDate = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);
                    q_Entity = _context.BoardOfDirectors.Add(_mapper.Map<BoardOfDirectors>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<BoardOfDirectorsDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.BoardOfDirectors).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.CustomerId),request.CustomerId
                        },
                        {
                            nameof(q_Entity.Entity.MemberName),request.MemberName
                        },
                        {
                            nameof(q_Entity.Entity.MemberPostId),request.MemberPostId
                        },
                        {
                            nameof(q_Entity.Entity.MemberEductionId),request.MemberEductionId
                        },
                        {
                            nameof(q_Entity.Entity.UniversityId),request.UniversityId
                        },
                        {
                            nameof(q_Entity.Entity.AcademicDegreePicture),request.AcademicDegreePicture
                        },
                        {
                            nameof(q_Entity.Entity.IsMemeberOfBoard),request.IsMemeberOfBoard
                        },
                        {
                            nameof(q_Entity.Entity.OwnershipPercentage),request.OwnershipPercentage
                        },
                        {
                            nameof(q_Entity.Entity.OwnershipCount),request.OwnershipCount
                        },
                        {
                            nameof(q_Entity.Entity.InsuranceHistory),request.InsuranceHistory
                        },
                        {
                            nameof(q_Entity.Entity.ManagersExperience),request.ManagersExperience
                        },
                        {
                            nameof(q_Entity.Entity.TitelCourses),request.TitleCourses
                        },
                        {
                            nameof(q_Entity.Entity.TimeOfCource),request.TimeOfCource
                        },
                        {
                            nameof(q_Entity.Entity.PlaceOfTraining),request.PlaceOfTraining
                        },
                        {
                            nameof(q_Entity.Entity.PictureOfEducationCourse),request.PictureOfEducationCourse
                        },
                    }, string.Format(nameof(q_Entity.Entity.BoardOfDirectorsId) + " = {0} ", request.BoardOfDirectorsId));

                    #region Upload Image

                    if (request.Result_Final_AcademicDegreePicture != null && request.Result_Final_AcademicDegreePicture.Length > 10)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.BoardOfDirectorsFolderWithwwwroot + fileNameOldPic_AcademicDegreePicture);

                    if (request.Result_Final_PictureOfEducationCourse != null && request.Result_Final_PictureOfEducationCourse.Length > 10)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.BoardOfDirectorsFolderWithwwwroot + fileNameOldPic_PictureOfEducationCourse);

                    path_AcademicDegreePicture = string.Empty;
                    path_PictureOfEducationCourse = string.Empty;

                    #endregion

                }

                return new ResultDto<BoardOfDirectorsDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت هیات مدیره با موفقیت انجام شد",
                    Data = request
                };


            }
            catch (Exception ex)
            {
                #region Upload Image

                FileOperation.DeleteFile(path_AcademicDegreePicture);
                FileOperation.DeleteFile(path_PictureOfEducationCourse);

                #endregion

                return new ResultDto<BoardOfDirectorsDto>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

    }
}
