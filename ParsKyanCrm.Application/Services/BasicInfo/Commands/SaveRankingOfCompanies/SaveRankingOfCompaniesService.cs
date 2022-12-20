using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveRankingOfCompanies
{

    public class SaveRankingOfCompaniesService : ISaveRankingOfCompaniesService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public SaveRankingOfCompaniesService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<ResultDto<RankingOfCompaniesDto>> Execute(RankingOfCompaniesDto request)
        {
            #region Upload Image

            string fileNameOldPic_PressRelease = string.Empty, path_PressRelease = string.Empty;
            string fileNameOldPic_SummaryRanking = string.Empty, path_SummaryRanking = string.Empty;

            #endregion

            try
            {

                #region Validation



                #endregion

                var qFind = await _context.RankingOfCompanies.FindAsync(request.RankingId);
                request.PressRelease = qFind != null && !string.IsNullOrEmpty(qFind.PressRelease) ? qFind.PressRelease : string.Empty;
                request.SummaryRanking = qFind != null && !string.IsNullOrEmpty(qFind.SummaryRanking) ? qFind.SummaryRanking : string.Empty;

                #region Upload Image

                if (request.Result_Final_PressRelease != null)
                {
                    fileNameOldPic_PressRelease = request.PressRelease;
                    request.PressRelease = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_PressRelease.FileName);
                    path_PressRelease = _env.ContentRootPath + VaribleForName.RankingOfCompaniesFolder + request.PressRelease;
                    await ServiceFileUploader.SaveFile(request.Result_Final_PressRelease, path_PressRelease, "فایل بیانیه مطبوعاتی");
                }

                if (request.Result_Final_SummaryRanking != null)
                {
                    fileNameOldPic_SummaryRanking = request.SummaryRanking;
                    request.SummaryRanking = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_SummaryRanking.FileName);
                    path_SummaryRanking = _env.ContentRootPath + VaribleForName.RankingOfCompaniesFolder + request.SummaryRanking;
                    await ServiceFileUploader.SaveFile(request.Result_Final_SummaryRanking, path_SummaryRanking, "فایل خلاصه رتبه بندی");
                }

                #endregion

                EntityEntry<RankingOfCompanies> q_Entity;
                if (request.RankingId == 0)
                {
                    request.IsActive = (byte)Common.Enums.TablesGeneralIsActive.Active;
                    q_Entity = _context.RankingOfCompanies.Add(_mapper.Map<RankingOfCompanies>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<RankingOfCompaniesDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.RankingOfCompanies).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.PressRelease),request.PressRelease
                        },
                        {
                            nameof(q_Entity.Entity.ComanyId),request.ComanyId
                        },
                        {
                            nameof(q_Entity.Entity.PublishDate),request.PublishDate
                        },
                        {
                            nameof(q_Entity.Entity.LongTermRating),request.LongTermRating
                        },
                        {
                            nameof(q_Entity.Entity.ShortTermRating),request.ShortTermRating
                        },
                        {
                            "Vision",request.Vistion
                        },
                        {
                            nameof(q_Entity.Entity.SummaryRanking),request.SummaryRanking
                        },
                        {
                            nameof(q_Entity.Entity.UserId),request.UserId
                        },

                    }, string.Format(nameof(q_Entity.Entity.RankingId) + " = {0} ", request.RankingId));

                    #region Upload Image

                    if (request.Result_Final_PressRelease != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.RankingOfCompaniesFolder + fileNameOldPic_PressRelease);

                    if (request.Result_Final_SummaryRanking != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.RankingOfCompaniesFolder + fileNameOldPic_SummaryRanking);

                    path_PressRelease = string.Empty;
                    path_SummaryRanking = string.Empty;

                    #endregion

                }

                return new ResultDto<RankingOfCompaniesDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت امتیازات شرکت ها با موفقیت انجام شد",
                    Data = request
                };


            }
            catch (Exception ex)
            {
                #region Upload Image

                FileOperation.DeleteFile(path_PressRelease);
                FileOperation.DeleteFile(path_SummaryRanking);

                #endregion

                return new ResultDto<RankingOfCompaniesDto>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

    }
}
