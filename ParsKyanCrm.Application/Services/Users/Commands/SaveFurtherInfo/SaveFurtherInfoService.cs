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

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveFurtherInfo
{

    public class SaveFurtherInfoService : ISaveFurtherInfoService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        private readonly IWebHostEnvironment _env;
        public SaveFurtherInfoService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;            
            _env = env;
        }

        public async Task<ResultDto<FurtherInfoDto>> Execute(FurtherInfoDto request)
        {
            #region Upload Image
            //

            string fileNameOldPic_LastAuditingTaxList = string.Empty, path_LastAuditingTaxList = string.Empty;
            string fileNameOldPic_LastChangeOfficialNewspaper = string.Empty, path_LastChangeOfficialNewspaper = string.Empty;
            string fileNameOldPic_StatuteDoc = string.Empty, path_StatuteDoc = string.Empty;
            string fileNameOldPic_OfficialNewspaper = string.Empty, path_OfficialNewspaper = string.Empty;
            string fileNameOldPic_StatementTaxList = string.Empty, path_StatementTaxList = string.Empty;

            #endregion
            try
            {
               
                #region Validation



                #endregion

                var con = await _context.FurtherInfo.FindAsync(request.FurtherInfoId);
                request.LastAuditingTaxList = con != null && !string.IsNullOrEmpty(con.LastAuditingTaxList) ? con.LastAuditingTaxList : string.Empty;
                request.LastChangeOfficialNewspaper = con != null && !string.IsNullOrEmpty(con.LastChangeOfficialNewspaper) ? con.LastChangeOfficialNewspaper : string.Empty;
                request.StatuteDoc = con != null && !string.IsNullOrEmpty(con.StatuteDoc) ? con.StatuteDoc : string.Empty;
                request.OfficialNewspaper = con != null && !string.IsNullOrEmpty(con.OfficialNewspaper) ? con.OfficialNewspaper : string.Empty;
                request.StatementTaxList = con != null && !string.IsNullOrEmpty(con.StatementTaxList) ? con.StatementTaxList : string.Empty;

                #region Upload Image

                if (request.Result_Final_LastAuditingTaxList != null)
                {
                    fileNameOldPic_LastAuditingTaxList = request.LastAuditingTaxList;
                    request.LastAuditingTaxList = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_LastAuditingTaxList.FileName);
                    path_LastAuditingTaxList = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.LastAuditingTaxList;
                    await ServiceFileUploader.SaveFile(request.Result_Final_LastAuditingTaxList, path_LastAuditingTaxList, "صورتهای مالی حسابرسی شده");
                }

                if (request.Result_Final_LastChangeOfficialNewspaper != null)
                {
                    fileNameOldPic_LastChangeOfficialNewspaper = request.LastChangeOfficialNewspaper;
                    request.LastChangeOfficialNewspaper = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_LastChangeOfficialNewspaper.FileName);
                    path_LastChangeOfficialNewspaper = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.LastChangeOfficialNewspaper;
                    await ServiceFileUploader.SaveFile(request.Result_Final_LastChangeOfficialNewspaper, path_LastChangeOfficialNewspaper, "آخرین تغییرات روزنامه رسمی");
                }

                if (request.Result_Final_StatuteDoc != null)
                {
                    fileNameOldPic_StatuteDoc = request.StatuteDoc;
                    request.StatuteDoc = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_StatuteDoc.FileName);
                    path_StatuteDoc = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.StatuteDoc;
                    await ServiceFileUploader.SaveFile(request.Result_Final_StatuteDoc, path_StatuteDoc, "اساسنامه");
                }

                if (request.Result_Final_OfficialNewspaper != null)
                {
                    fileNameOldPic_OfficialNewspaper = request.OfficialNewspaper;
                    request.OfficialNewspaper = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_OfficialNewspaper.FileName);
                    path_OfficialNewspaper = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.OfficialNewspaper;
                    await ServiceFileUploader.SaveFile(request.Result_Final_OfficialNewspaper, path_OfficialNewspaper, " روزنامه رسمی");
                }

                if (request.Result_Final_StatementTaxList != null)
                {
                    fileNameOldPic_StatementTaxList = request.StatementTaxList;
                    request.StatementTaxList = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_StatementTaxList.FileName);
                    path_StatementTaxList = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.StatementTaxList;
                    await ServiceFileUploader.SaveFile(request.Result_Final_StatementTaxList, path_StatementTaxList, " اظهارنامه مالیاتی");
                }

                #endregion                                

                EntityEntry<FurtherInfo> q_Entity;
                if (request.FurtherInfoId == 0)
                {
                   // request.SaveDate = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);
                    q_Entity = _context.FurtherInfo.Add(_mapper.Map<FurtherInfo>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<FurtherInfoDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.FurtherInfo).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.RequestId),request.RequestId
                        },
                        {
                            nameof(q_Entity.Entity.LastAuditingTaxList),request.LastAuditingTaxList
                        },
                        {
                            nameof(q_Entity.Entity.StatuteDoc),request.StatuteDoc
                        },
                        {
                            nameof(q_Entity.Entity.LastChangeOfficialNewspaper),request.LastChangeOfficialNewspaper
                        }
                        ,
                        {
                            nameof(q_Entity.Entity.StatementTaxList),request.StatementTaxList
                        },
                        {
                            "OfficialNewspaper",request.OfficialNewspaper
                        }
                    }, string.Format(nameof(q_Entity.Entity.FurtherInfoId) + " = {0} ", request.FurtherInfoId)) ;
                    #region Upload Image

                    if (request.Result_Final_OfficialNewspaper != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_OfficialNewspaper);

                    if (request.Result_Final_LastAuditingTaxList != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_LastAuditingTaxList);

                    if (request.Result_Final_LastChangeOfficialNewspaper != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_LastChangeOfficialNewspaper);

                    if (request.Result_Final_StatuteDoc != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_StatuteDoc);


                    if (request.Result_Final_StatementTaxList != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_StatementTaxList);

                    path_StatuteDoc = string.Empty;
                    path_LastChangeOfficialNewspaper = string.Empty;
                    path_LastAuditingTaxList = string.Empty;
                    path_OfficialNewspaper = string.Empty;
                    path_LastAuditingTaxList = string.Empty;
                    #endregion
                }

                return new ResultDto<FurtherInfoDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت نرخ نامه قرارداد با موفقیت انجام شد",
                    Data = request
                };


            }
            catch (Exception ex)
            {
                #region Upload Image

                FileOperation.DeleteFile(path_OfficialNewspaper);
                FileOperation.DeleteFile(path_StatuteDoc);
                FileOperation.DeleteFile(path_LastAuditingTaxList);
                FileOperation.DeleteFile(path_LastChangeOfficialNewspaper);
                FileOperation.DeleteFile(path_StatementTaxList);
                #endregion
                throw ex;
            }
        }

        public async Task<ResultDto<FurtherInfoDto>> ExecuteCopy(string Request)
        {

            string[] values = Request.Split('-');
            int newReq = Convert.ToInt32(values[0]);
            int OldReq = Convert.ToInt32(values[1]);

            

            #region Upload Image
            //

           
            #endregion
            try
            {

                var con = await Infrastructure.DapperOperation.Run<FurtherInfoDto>("select * from FurtherInfo where RequestId=" + OldReq);

                foreach (var item in con)
                {
                    FurtherInfoDto request = new FurtherInfoDto();

                    request.IsActive = item.IsActive;
                    request.RequestId = newReq;
                    
                    string fileNameOldPic_LastAuditingTaxList = string.Empty, path_LastAuditingTaxList = string.Empty;
                    string fileNameOldPic_LastChangeOfficialNewspaper = string.Empty, path_LastChangeOfficialNewspaper = string.Empty;
                    string fileNameOldPic_StatuteDoc = string.Empty, path_StatuteDoc = string.Empty;
                    string fileNameOldPic_OfficialNewspaper = string.Empty, path_OfficialNewspaper = string.Empty;
                    string fileNameOldPic_StatementTaxList = string.Empty, path_StatementTaxList = string.Empty;


                    request.LastAuditingTaxList = con != null && !string.IsNullOrEmpty(item.LastAuditingTaxList) ? item.LastAuditingTaxList : string.Empty;
                    request.LastChangeOfficialNewspaper = con != null && !string.IsNullOrEmpty(item.LastChangeOfficialNewspaper) ? item.LastChangeOfficialNewspaper : string.Empty;
                    request.StatuteDoc = con != null && !string.IsNullOrEmpty(item.StatuteDoc) ? item.StatuteDoc : string.Empty;
                    request.OfficialNewspaper = con != null && !string.IsNullOrEmpty(item.OfficialNewspaper) ? item.OfficialNewspaper : string.Empty;
                    request.StatementTaxList = con != null && !string.IsNullOrEmpty(item.StatementTaxList) ? item.StatementTaxList : string.Empty;

                    //#region Upload Image

                    //if (request.LastAuditingTaxList != null)
                    //{
                    //    fileNameOldPic_LastAuditingTaxList = request.LastAuditingTaxList;
                    //    request.LastAuditingTaxList = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_LastAuditingTaxList);
                    //    path_LastAuditingTaxList = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.LastAuditingTaxList;
                    //    await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_LastAuditingTaxList, path_LastAuditingTaxList, "صورتهای مالی حسابرسی شده");
                    //}

                    //if (request.LastChangeOfficialNewspaper != null)
                    //{
                    //    fileNameOldPic_LastChangeOfficialNewspaper = request.LastChangeOfficialNewspaper;
                    //    request.LastChangeOfficialNewspaper = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_LastChangeOfficialNewspaper);
                    //    path_LastChangeOfficialNewspaper = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.LastChangeOfficialNewspaper;
                    //    await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_LastChangeOfficialNewspaper, path_LastChangeOfficialNewspaper, "آخرین تغییرات روزنامه رسمی");
                    //}

                    //if (request.Result_Final_StatuteDoc != null)
                    //{
                    //    fileNameOldPic_StatuteDoc = request.StatuteDoc;
                    //    request.StatuteDoc = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_StatuteDoc);
                    //    path_StatuteDoc = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.StatuteDoc;
                    //    await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_StatuteDoc, path_StatuteDoc, "اساسنامه");
                    //}

                    //if (request.Result_Final_OfficialNewspaper != null)
                    //{
                    //    fileNameOldPic_OfficialNewspaper = request.OfficialNewspaper;
                    //    request.OfficialNewspaper = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_OfficialNewspaper);
                    //    path_OfficialNewspaper = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.OfficialNewspaper;
                    //    await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot+ fileNameOldPic_OfficialNewspaper, path_OfficialNewspaper, " روزنامه رسمی");
                    //}

                    //if (request.Result_Final_StatementTaxList != null)
                    //{
                    //    fileNameOldPic_StatementTaxList = request.StatementTaxList;
                    //    request.StatementTaxList = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_StatementTaxList);
                    //    path_StatementTaxList = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.StatementTaxList;
                    //    await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot+ fileNameOldPic_StatementTaxList, path_StatementTaxList, " اظهارنامه مالیاتی");
                    //}

                    //#endregion

                    EntityEntry<FurtherInfo> q_Entity;
                    if (request.FurtherInfoId == 0)
                    {
                        q_Entity = _context.FurtherInfo.Add(_mapper.Map<FurtherInfo>(request));
                        await _context.SaveChangesAsync();
                        request = _mapper.Map<FurtherInfoDto>(q_Entity.Entity);
                    }

                }

                return new ResultDto<FurtherInfoDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت نرخ نامه قرارداد با موفقیت انجام شد",
                    Data = null
                };


            }
            catch (Exception ex)
            {
              
                throw ex;
            }
        }
    }
}
