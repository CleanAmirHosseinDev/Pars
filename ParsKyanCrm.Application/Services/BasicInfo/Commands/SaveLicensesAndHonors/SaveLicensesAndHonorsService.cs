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

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveLicensesAndHonors
{

    public class SaveLicensesAndHonorsService : ISaveLicensesAndHonorsService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public SaveLicensesAndHonorsService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<ResultDto<LicensesAndHonorsDto>> Execute(LicensesAndHonorsDto request)
        {
            #region Upload Image

            string fileNameOldPic_Picture = string.Empty, path_Picture = string.Empty;

            #endregion

            try
            {

                #region Validation



                #endregion

                #region Upload Image

                if (request.Result_Final_Picture != null && request.Result_Final_Picture.Length > 10)
                {
                    fileNameOldPic_Picture = request.Picture;
                    request.Picture = Guid.NewGuid().ToString().Replace("-", "") + ".png";
                    path_Picture = _env.ContentRootPath + VaribleForName.LicensesAndHonorsFolder + request.Picture;

                    string strMessage = ServiceImage.SaveImageByByte_InExistNextDelete(request.Result_Final_Picture, path_Picture, string.Empty, "تصویر جایزه");
                    if (!string.IsNullOrEmpty(strMessage))
                    {

                        return new ResultDto<LicensesAndHonorsDto>()
                        {
                            IsSuccess = false,
                            Message = strMessage,
                            Data = null
                        };

                    }
                }

                #endregion

                EntityEntry<LicensesAndHonors> q_Entity;
                if (request.LicensesAndHonorsId == 0)
                {
                    request.SaveOrEditDate = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);
                    q_Entity = _context.LicensesAndHonors.Add(_mapper.Map<LicensesAndHonors>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<LicensesAndHonorsDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.LicensesAndHonors).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.Picture),request.Picture
                        },
                        {
                            nameof(q_Entity.Entity.Title),request.Title
                        },
                        {
                            nameof(q_Entity.Entity.Userid),request.Userid
                        },
                        {
                            nameof(q_Entity.Entity.SaveOrEditDate),DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now)
                        },

                    }, string.Format(nameof(q_Entity.Entity.LicensesAndHonorsId) + " = {0} ", request.LicensesAndHonorsId));

                    #region Upload Image

                    if (request.Result_Final_Picture != null && request.Result_Final_Picture.Length > 10)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.LicensesAndHonorsFolder + fileNameOldPic_Picture);


                    path_Picture = string.Empty;

                    #endregion

                }

                return new ResultDto<LicensesAndHonorsDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت جوایز و افتخارات با موفقیت انجام شد",
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
