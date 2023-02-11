using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveValueChain
{
   
    public class SaveValueChainService : ISaveValueChainService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IWebHostEnvironment _env;
        public SaveValueChainService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            _env = env;
        }

        public async Task<ResultDto<ValueChainDto>> Execute(ValueChainDto request)
        {
            #region Upload Image
            //

            //string fileNameOldPic_LastAuditingTaxList = string.Empty, path_LastAuditingTaxList = string.Empty;
            //string fileNameOldPic_LastChangeOfficialNewspaper = string.Empty, path_LastChangeOfficialNewspaper = string.Empty;
            //string fileNameOldPic_StatuteDoc = string.Empty, path_StatuteDoc = string.Empty;
            //string fileNameOldPic_OfficialNewspaper = string.Empty, path_OfficialNewspaper = string.Empty;

            #endregion
            try
            {

                #region Validation



                #endregion

                var con = await _context.ValueChain.FindAsync(request.ValueChainId);
                //request.LastAuditingTaxList = con != null && !string.IsNullOrEmpty(con.LastAuditingTaxList) ? con.LastAuditingTaxList : string.Empty;
                //request.LastChangeOfficialNewspaper = con != null && !string.IsNullOrEmpty(con.LastChangeOfficialNewspaper) ? con.LastChangeOfficialNewspaper : string.Empty;
                //request.StatuteDoc = con != null && !string.IsNullOrEmpty(con.StatuteDoc) ? con.StatuteDoc : string.Empty;
                //request.OfficialNewspaper = con != null && !string.IsNullOrEmpty(con.OfficialNewspaper) ? con.OfficialNewspaper : string.Empty;

                //#region Upload Image

                //if (request.Result_Final_LastAuditingTaxList != null)
                //{
                //    fileNameOldPic_LastAuditingTaxList = request.LastAuditingTaxList;
                //    request.LastAuditingTaxList = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_LastAuditingTaxList.FileName);
                //    path_LastAuditingTaxList = _env.ContentRootPath + VaribleForName.CustomersFolder + request.LastAuditingTaxList;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_LastAuditingTaxList, path_LastAuditingTaxList, "صورتهای مالی حسابرسی شده");
                //}

                //if (request.Result_Final_LastChangeOfficialNewspaper != null)
                //{
                //    fileNameOldPic_LastChangeOfficialNewspaper = request.LastChangeOfficialNewspaper;
                //    request.LastChangeOfficialNewspaper = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_LastChangeOfficialNewspaper.FileName);
                //    path_LastChangeOfficialNewspaper = _env.ContentRootPath + VaribleForName.CustomersFolder + request.LastChangeOfficialNewspaper;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_LastChangeOfficialNewspaper, path_LastChangeOfficialNewspaper, "آخرین تغییرات روزنامه رسمی");
                //}

                //if (request.Result_Final_StatuteDoc != null)
                //{
                //    fileNameOldPic_StatuteDoc = request.StatuteDoc;
                //    request.StatuteDoc = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_StatuteDoc.FileName);
                //    path_StatuteDoc = _env.ContentRootPath + VaribleForName.CustomersFolder + request.StatuteDoc;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_StatuteDoc, path_StatuteDoc, "اساسنامه");
                //}

                //if (request.Result_Final_OfficialNewspaper != null)
                //{
                //    fileNameOldPic_OfficialNewspaper = request.OfficialNewspaper;
                //    request.OfficialNewspaper = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_OfficialNewspaper.FileName);
                //    path_OfficialNewspaper = _env.ContentRootPath + VaribleForName.CustomersFolder + request.OfficialNewspaper;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_OfficialNewspaper, path_OfficialNewspaper, " روزنامه رسمی");
                //}

                //#endregion                                

                EntityEntry<ValueChain> q_Entity;
                if (request.ValueChainId == 0)
                {
                    // request.SaveDate = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);
                    q_Entity = _context.ValueChain.Add(_mapper.Map<ValueChain>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<ValueChainDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.ValueChain).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.RequestId),request.RequestId
                        },
                        //{
                        //    nameof(q_Entity.Entity.LastAuditingTaxList),request.LastAuditingTaxList
                        //},
                        //{
                        //    nameof(q_Entity.Entity.StatuteDoc),request.StatuteDoc
                        //},
                        //{
                        //    nameof(q_Entity.Entity.LastChangeOfficialNewspaper),request.LastChangeOfficialNewspaper
                        //}
                    }, string.Format(nameof(q_Entity.Entity.RequestId) + " = {0} ", request.RequestId));
                    //#region Upload Image

                    //if (request.Result_Final_OfficialNewspaper != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_OfficialNewspaper);

                    //if (request.Result_Final_LastAuditingTaxList != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_LastAuditingTaxList);

                    //if (request.Result_Final_LastChangeOfficialNewspaper != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_LastChangeOfficialNewspaper);

                    //if (request.Result_Final_StatuteDoc != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_StatuteDoc);

                    //path_StatuteDoc = string.Empty;
                    //path_LastChangeOfficialNewspaper = string.Empty;
                    //path_LastAuditingTaxList = string.Empty;
                    //path_OfficialNewspaper = string.Empty;

                    //#endregion
                }

                return new ResultDto<ValueChainDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت نرخ نامه قرارداد با موفقیت انجام شد",
                    Data = request
                };


            }
            catch (Exception ex)
            {
                #region Upload Image

                //FileOperation.DeleteFile(path_OfficialNewspaper);
                //FileOperation.DeleteFile(path_StatuteDoc);
                //FileOperation.DeleteFile(path_LastAuditingTaxList);
                //FileOperation.DeleteFile(path_LastChangeOfficialNewspaper);
                #endregion
                throw ex;
            }
        }
    }
}
