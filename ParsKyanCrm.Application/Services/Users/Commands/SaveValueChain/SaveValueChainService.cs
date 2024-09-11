using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Application.Services.Users.Commands.SaveValueChain;
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

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveValueChainService
{
   
    public class SaveValueChainService : ISaveValueChainService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        private readonly IWebHostEnvironment _env;
        public SaveValueChainService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;            
            _env = env;
        }

        public async Task<ResultDto<ValueChainDto>> Execute(ValueChainDto request)
        {
            #region Upload Image
            //

            string fileNameOldPic_Vc1 = string.Empty,  path_Vc1 = string.Empty;
            string fileNameOldPic_Vc2 = string.Empty,  path_Vc2 = string.Empty;
            string fileNameOldPic_Vc3 = string.Empty,  path_Vc3= string.Empty;
            string fileNameOldPic_Vc4 = string.Empty,  path_Vc4 = string.Empty;
            string fileNameOldPic_Vc5 = string.Empty,  path_Vc5 = string.Empty;
            string fileNameOldPic_Vc6 = string.Empty,  path_Vc6 = string.Empty;
            string fileNameOldPic_Vc7 = string.Empty,  path_Vc7 = string.Empty;
            string fileNameOldPic_Vc8 = string.Empty,  path_Vc8 = string.Empty;
            string fileNameOldPic_Vc9 = string.Empty,  path_Vc9 = string.Empty;
            string fileNameOldPic_Vc10 = string.Empty, path_Vc10 = string.Empty;
            string fileNameOldPic_Vc11 = string.Empty, path_Vc11 = string.Empty;
            string fileNameOldPic_Vc12 = string.Empty, path_Vc12 = string.Empty;
            string fileNameOldPic_Vc13 = string.Empty, path_Vc13 = string.Empty;
            string fileNameOldPic_Vc14 = string.Empty, path_Vc14 = string.Empty;
            string fileNameOldPic_Vc15 = string.Empty, path_Vc15 = string.Empty;
            string fileNameOldPic_Vc16 = string.Empty, path_Vc16 = string.Empty;
            string fileNameOldPic_Vc17 = string.Empty, path_Vc17 = string.Empty;
            string fileNameOldPic_Vc18 = string.Empty, path_Vc18 = string.Empty;
            string fileNameOldPic_Vc19 = string.Empty, path_Vc19 = string.Empty;
            string fileNameOldPic_Vc20 = string.Empty, path_Vc20 = string.Empty;
            string fileNameOldPic_Vc21 = string.Empty, path_Vc21 = string.Empty;
            string fileNameOldPic_Vc22 = string.Empty, path_Vc22 = string.Empty;
            string fileNameOldPic_Vc23= string.Empty,  path_Vc23 = string.Empty;
            string fileNameOldPic_Vc24 = string.Empty, path_Vc24 = string.Empty;
            string fileNameOldPic_Vc25 = string.Empty, path_Vc25 = string.Empty;

            #endregion
            try
            {

                #region Validation



                #endregion

                var con = await _context.ValueChain.FindAsync(request.ValueChainId);
                request.vc1 = con != null &&  !string.IsNullOrEmpty(con.vc1 ) ? con.vc1  : string.Empty;
                request.vc2 = con != null &&  !string.IsNullOrEmpty(con.vc2 ) ? con.vc2  : string.Empty;
                request.vc3 = con != null &&  !string.IsNullOrEmpty(con.vc3 ) ? con.vc3  : string.Empty;
                request.vc4 = con != null &&  !string.IsNullOrEmpty(con.vc4 ) ? con.vc4  : string.Empty;
                request.vc5 = con != null &&  !string.IsNullOrEmpty(con.vc5 ) ? con.vc5  : string.Empty;
                request.vc6 = con != null &&  !string.IsNullOrEmpty(con.vc6 ) ? con.vc6  : string.Empty;
                request.vc7 = con != null &&  !string.IsNullOrEmpty(con.vc7 ) ? con.vc7  : string.Empty;
                request.vc8 = con != null &&  !string.IsNullOrEmpty(con.vc8 ) ? con.vc8  : string.Empty;
                request.vc9 = con != null &&  !string.IsNullOrEmpty(con.vc9 ) ? con.vc9  : string.Empty;
                request.vc10 = con != null && !string.IsNullOrEmpty(con.vc10) ? con.vc10 : string.Empty;
                request.vc11 = con != null && !string.IsNullOrEmpty(con.vc11) ? con.vc11 : string.Empty;
                request.vc12 = con != null && !string.IsNullOrEmpty(con.vc12) ? con.vc12 : string.Empty;
                request.vc13 = con != null && !string.IsNullOrEmpty(con.vc13) ? con.vc13 : string.Empty;
                request.vc14 = con != null && !string.IsNullOrEmpty(con.vc14) ? con.vc14 : string.Empty;
                request.vc15 = con != null && !string.IsNullOrEmpty(con.vc15) ? con.vc15 : string.Empty;
                request.vc16 = con != null && !string.IsNullOrEmpty(con.vc16) ? con.vc16 : string.Empty;
                request.vc17 = con != null && !string.IsNullOrEmpty(con.vc17) ? con.vc17 : string.Empty;
                request.vc18 = con != null && !string.IsNullOrEmpty(con.vc18) ? con.vc18 : string.Empty;
                request.vc19 = con != null && !string.IsNullOrEmpty(con.vc19) ? con.vc19 : string.Empty;
                request.vc20 = con != null && !string.IsNullOrEmpty(con.vc20) ? con.vc20 : string.Empty;
                request.vc21 = con != null && !string.IsNullOrEmpty(con.vc21) ? con.vc21 : string.Empty;
                request.vc22 = con != null && !string.IsNullOrEmpty(con.vc22) ? con.vc22 : string.Empty;
                request.vc23 = con != null && !string.IsNullOrEmpty(con.vc23) ? con.vc23 : string.Empty;
                request.vc24 = con != null && !string.IsNullOrEmpty(con.vc24) ? con.vc24 : string.Empty;
                request.vc25 = con != null && !string.IsNullOrEmpty(con.vc25) ? con.vc25 : string.Empty;

                //#region Upload Image

                //if (request.Result_Final_vc1 != null)
                //{
                //    fileNameOldPic_Vc1 = request.vc1;
                //    request.vc1 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc1.FileName);
                //    path_Vc1 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc1;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc1, path_Vc1, "انبار / سند مالکیت انبار");
                //}
                //if (request.Result_Final_vc2 != null)
                //{
                //    fileNameOldPic_Vc2 = request.vc2;
                //    request.vc2= Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc2.FileName);
                //    path_Vc2 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc2;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc2, path_Vc2, "عکس/سند تجهیزات انبارش و بارگیری");
                //}
                //if (request.Result_Final_vc3 != null)
                //{
                //    fileNameOldPic_Vc3 = request.vc3;
                //    request.vc3 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc3.FileName);
                //    path_Vc3 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc3;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc3, path_Vc3, "عکس/سند مالکیت تجهیزات حمل و نقل به/ از  انبار");
                //}
                //if (request.Result_Final_vc4 != null)
                //{
                //    fileNameOldPic_Vc4 = request.vc4;
                //    request.vc4 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc4.FileName);
                //    path_Vc4 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc4;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc4, path_Vc4, "عکس / قراداد نرم افزار انبارداری");
                //}
                //if (request.Result_Final_vc5 != null)
                //{
                //    fileNameOldPic_Vc5 = request.vc5;
                //    request.vc5 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc5.FileName);
                //    path_Vc5 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc5;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc5, path_Vc5, "دفتر مرکزی(اجاره نامه یا سند مالکیت");
                //}
                //if (request.Result_Final_vc6 != null)
                //{
                //    fileNameOldPic_Vc6 = request.vc6;
                //    request.vc6 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc6.FileName);
                //    path_Vc6 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc6;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc6, path_Vc6, "دفتر خارج از کشور (اجاره نامه یا سند مالکیت");
                //}
                //if (request.Result_Final_vc7 != null)
                //{
                //    fileNameOldPic_Vc7 = request.vc7;
                //    request.vc7 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc7.FileName);
                //    path_Vc7 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc7;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc7, path_Vc7, "یک نمونه فیش حقوقی پرسنل نشان دهنده واریز پاداش");
                //}
                //if (request.Result_Final_vc8 != null)
                //{
                //    fileNameOldPic_Vc8 = request.vc8;
                //    request.vc8 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc8.FileName);
                //    path_Vc8 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc8;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc8, path_Vc8, "عکس یا قرارداد نرم افزار های یکپارچه مدیریتی");
                //}
                //if (request.Result_Final_vc9 != null)
                //{
                //    fileNameOldPic_Vc9 = request.vc9;
                //    request.vc9 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc9.FileName);
                //    path_Vc9= _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc9;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc9, path_Vc9, "عکس از آزمایشگاه  (در صورت برون سپاری قرارداد مربوطه");
                //}
                //if (request.Result_Final_vc10 != null)
                //{
                //    fileNameOldPic_Vc10 = request.vc10;
                //    request.vc10 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc10.FileName);
                //    path_Vc10 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc10;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc10, path_Vc10, "تصویر مجوز دانش بنیان");
                //}
                //if (request.Result_Final_vc11 != null)
                //{
                //    fileNameOldPic_Vc11 = request.vc11;
                //    request.vc11 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc11.FileName);
                //    path_Vc11 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc11;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc11, path_Vc11, "سند مالکیت یا قراداد یا غیره برای ناوگان حمل و نقل");
                //}
                //if (request.Result_Final_vc12 != null)
                //{
                //    fileNameOldPic_Vc12 = request.vc12;
                //    request.vc12 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc12.FileName);
                //    path_Vc12 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc12;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc12, path_Vc12, "درصورت وجود یک مدل جهت ردیابی محصول با بارکد یا کد رجیستری");
                //}
                //if (request.Result_Final_vc13 != null)
                //{
                //    fileNameOldPic_Vc13 = request.vc13;
                //    request.vc13= Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc13.FileName);
                //    path_Vc13 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc13;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc13, path_Vc13, "نمایندگی شرکت  ");
                //}
                //if (request.Result_Final_vc14 != null)
                //{
                //    fileNameOldPic_Vc14 = request.vc14;
                //    request.vc14 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc14.FileName);
                //    path_Vc14 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc14;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc14, path_Vc14, " شرکت نمایندگان خودد را ارزیابی کرده");
                //}
                //if (request.Result_Final_vc15 != null)
                //{
                //    fileNameOldPic_Vc15 = request.vc15;
                //    request.vc15 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc15.FileName);
                //    path_Vc15 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc15;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc15, path_Vc15, " تصویر یا آدرس سایت و فعالیت در شبکه های اجتماعی");
                //}
                //if (request.Result_Final_vc16 != null)
                //{
                //    fileNameOldPic_Vc16 = request.vc16;
                //    request.vc16 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc16.FileName);
                //    path_Vc16 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc16;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc16, path_Vc16, "اجاره نامه / سند مالکیت فروشگاه");
                //}
                //if (request.Result_Final_vc17 != null)
                //{
                //    fileNameOldPic_Vc17 = request.vc17;
                //    request.vc17 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc17.FileName);
                //    path_Vc17= _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc17;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc17, path_Vc17, "تصویر یا آدرس سایت فروش آنلاین");
                //}
                //if (request.Result_Final_vc18 != null)
                //{
                //    fileNameOldPic_Vc18 = request.vc18;
                //    request.vc18 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc18.FileName);
                //    path_Vc18 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc18;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc18, path_Vc18, "گزارش های بازاریابی حضوری");
                //}
                //if (request.Result_Final_vc19 != null)
                //{
                //    fileNameOldPic_Vc19 = request.vc19;
                //    request.vc19 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc19.FileName);
                //    path_Vc19 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc19;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc19, path_Vc19, " مجوز حضور در نمایشگاه ها");
                //}
                //if (request.Result_Final_vc20 != null)
                //{
                //    fileNameOldPic_Vc20 = request.vc20;
                //    request.vc20 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc20.FileName);
                //    path_Vc20 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc20;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc20, path_Vc20, "قرارداد های فروش به سازمان ها");
                //}
                //if (request.Result_Final_vc21 != null)
                //{
                //    fileNameOldPic_Vc21 = request.vc21;
                //    request.vc21 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc21.FileName);
                //    path_Vc21 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc21;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc21, path_Vc21, "نمونه تبلیغات محیطی");
                //}
                //if (request.Result_Final_vc22 != null)
                //{
                //    fileNameOldPic_Vc22 = request.vc22;
                //    request.vc22 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc22.FileName);
                //    path_Vc22 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc22;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc22, path_Vc22, "کارت گارانتی");
                //}
                //if (request.Result_Final_vc23 != null)
                //{
                //    fileNameOldPic_Vc23 = request.vc23;
                //    request.vc23 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc23.FileName);
                //    path_Vc23 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc23;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc23, path_Vc23, "عکس از واحد خدمات پس از فروش یا سایر مستندات");
                //}
                //if (request.Result_Final_vc24 != null)
                //{
                //    fileNameOldPic_Vc24 = request.vc24;
                //    request.vc24 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc24.FileName);
                //    path_Vc24 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc24;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc24, path_Vc24, "گزارش و گواهی بررسی و ارزیابی خدمات پس از فروش");
                //}
                //if (request.Result_Final_vc25 != null)
                //{
                //    fileNameOldPic_Vc25 = request.vc25;
                //    request.vc25 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_vc25.FileName);
                //    path_Vc25 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc25;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_vc25, path_Vc25, "عکس از تجهیزات  واحد خدمات پس از فروش");
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
                        {
                            nameof(q_Entity.Entity.vc1),request.vc1
                        },
                        {
                            nameof(q_Entity.Entity.vc2),request.vc2
                        },
                        {
                            nameof(q_Entity.Entity.vc3),request.vc3
                        },
                        {
                            nameof(q_Entity.Entity.vc4),request.vc4
                        },
                        {
                            nameof(q_Entity.Entity.vc5),request.vc5
                        },
                        {
                            nameof(q_Entity.Entity.vc6),request.vc6
                        },
                        {
                            nameof(q_Entity.Entity.vc7),request.vc7
                        },
                        {
                            nameof(q_Entity.Entity.vc8),request.vc8
                        },
                        {
                            nameof(q_Entity.Entity.vc9),request.vc9
                        },
                        {
                            nameof(q_Entity.Entity.vc10),request.vc10
                        },
                        {
                            nameof(q_Entity.Entity.vc11),request.vc11
                        },
                        {
                            nameof(q_Entity.Entity.vc12),request.vc12
                        },
                        {
                            nameof(q_Entity.Entity.vc13),request.vc13
                        },
                        {
                            nameof(q_Entity.Entity.vc14),request.vc14
                        },
                        {
                            nameof(q_Entity.Entity.vc15),request.vc15
                        },
                        {
                            nameof(q_Entity.Entity.vc16),request.vc16
                        },
                        {
                            nameof(q_Entity.Entity.vc17),request.vc17
                        },
                        {
                            nameof(q_Entity.Entity.vc18),request.vc18
                        },
                        {
                            nameof(q_Entity.Entity.vc19),request.vc19
                        },
                        {
                            nameof(q_Entity.Entity.vc20),request.vc20
                        }
                        ,
                        {
                            nameof(q_Entity.Entity.vc21),request.vc21
                        }
                        ,
                        {
                            nameof(q_Entity.Entity.vc22),request.vc22
                        }
                        ,
                        {
                            nameof(q_Entity.Entity.vc23),request.vc23
                        }
                        ,
                        {
                            nameof(q_Entity.Entity.vc24),request.vc24
                        },
                        {
                            nameof(q_Entity.Entity.vc25),request.vc25
                        }
                    }, string.Format(nameof(q_Entity.Entity.RequestId) + " = {0} ", request.RequestId));
                    //#region Upload Image

                    //if (request.Result_Final_vc1 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc1);

                    //if (request.Result_Final_vc2 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc2);

                    //if (request.Result_Final_vc3 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc3);

                    //if (request.Result_Final_vc4 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc4);

                    //if (request.Result_Final_vc5 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc5);

                    //if (request.Result_Final_vc6 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc6);

                    //if (request.Result_Final_vc7 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc7);

                    //if (request.Result_Final_vc8 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc8);

                    //if (request.Result_Final_vc9 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc9);

                    //if (request.Result_Final_vc10 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc10);

                    //if (request.Result_Final_vc11 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc11);

                    //if (request.Result_Final_vc12 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc12);

                    //if (request.Result_Final_vc13 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc13);

                    //if (request.Result_Final_vc14 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc14);

                    //if (request.Result_Final_vc15 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc15);

                    //if (request.Result_Final_vc16 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc16);

                    //if (request.Result_Final_vc17 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc17);

                    //if (request.Result_Final_vc18 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc18);

                    //if (request.Result_Final_vc19 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc19);

                    //if (request.Result_Final_vc20 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc20);

                    //if (request.Result_Final_vc21 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc21);

                    //if (request.Result_Final_vc22 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc22);

                    //if (request.Result_Final_vc23 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc23);

                    //if (request.Result_Final_vc24 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc24);

                    //if (request.Result_Final_vc25 != null)
                    //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc25);

                    //path_Vc1 = string.Empty;
                    //path_Vc2 = string.Empty;
                    //path_Vc3 = string.Empty;
                    //path_Vc4 = string.Empty;
                    //path_Vc5 = string.Empty;
                    //path_Vc6 = string.Empty;
                    //path_Vc7 = string.Empty;
                    //path_Vc8 = string.Empty;
                    //path_Vc9 = string.Empty;
                    //path_Vc10 = string.Empty;
                    //path_Vc11 = string.Empty;
                    //path_Vc12 = string.Empty;
                    //path_Vc13 = string.Empty;
                    //path_Vc14 = string.Empty;
                    //path_Vc15 = string.Empty;
                    //path_Vc16 = string.Empty;
                    //path_Vc17 = string.Empty;
                    //path_Vc18 = string.Empty;
                    //path_Vc19 = string.Empty;
                    //path_Vc20 = string.Empty;
                    //path_Vc21 = string.Empty;
                    //path_Vc22 = string.Empty;
                    //path_Vc23 = string.Empty;
                    //path_Vc24 = string.Empty;
                    //path_Vc25 = string.Empty;
                   

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

                FileOperation.DeleteFile(path_Vc1 );
                FileOperation.DeleteFile(path_Vc2 );
                FileOperation.DeleteFile(path_Vc3 );
                FileOperation.DeleteFile(path_Vc4 );
                FileOperation.DeleteFile(path_Vc5 );
                FileOperation.DeleteFile(path_Vc6 );
                FileOperation.DeleteFile(path_Vc7 );
                FileOperation.DeleteFile(path_Vc8 );
                FileOperation.DeleteFile(path_Vc9 );
                FileOperation.DeleteFile(path_Vc10);
                FileOperation.DeleteFile(path_Vc11);
                FileOperation.DeleteFile(path_Vc12);
                FileOperation.DeleteFile(path_Vc13);
                FileOperation.DeleteFile(path_Vc14);
                FileOperation.DeleteFile(path_Vc15);
                FileOperation.DeleteFile(path_Vc16);
                FileOperation.DeleteFile(path_Vc17);
                FileOperation.DeleteFile(path_Vc18);
                FileOperation.DeleteFile(path_Vc19);
                FileOperation.DeleteFile(path_Vc20);
                FileOperation.DeleteFile(path_Vc21);
                FileOperation.DeleteFile(path_Vc22);
                FileOperation.DeleteFile(path_Vc23);
                FileOperation.DeleteFile(path_Vc24);
                FileOperation.DeleteFile(path_Vc25);              
                #endregion
                throw ex;
            }
        }

        public async Task<ResultDto<ValueChainDto>> ExecuteCopy(string Request)
        {
            string[] values = Request.Split('-');
            int newReq = Convert.ToInt32(values[0]);
            int OldReq = Convert.ToInt32(values[1]);

            try
            {
                var con = await Infrastructure.DapperOperation.Run<ValueChainDto>("select * from ValueChain where RequestId=" + OldReq);

                foreach (var item in con)
                {

                    #region Upload Image
                    //

                    string fileNameOldPic_Vc1 = string.Empty, path_Vc1 = string.Empty;
                    string fileNameOldPic_Vc2 = string.Empty, path_Vc2 = string.Empty;
                    string fileNameOldPic_Vc3 = string.Empty, path_Vc3 = string.Empty;
                    string fileNameOldPic_Vc4 = string.Empty, path_Vc4 = string.Empty;
                    string fileNameOldPic_Vc5 = string.Empty, path_Vc5 = string.Empty;
                    string fileNameOldPic_Vc6 = string.Empty, path_Vc6 = string.Empty;
                    string fileNameOldPic_Vc7 = string.Empty, path_Vc7 = string.Empty;
                    string fileNameOldPic_Vc8 = string.Empty, path_Vc8 = string.Empty;
                    string fileNameOldPic_Vc9 = string.Empty, path_Vc9 = string.Empty;
                    string fileNameOldPic_Vc10 = string.Empty, path_Vc10 = string.Empty;
                    string fileNameOldPic_Vc11 = string.Empty, path_Vc11 = string.Empty;
                    string fileNameOldPic_Vc12 = string.Empty, path_Vc12 = string.Empty;
                    string fileNameOldPic_Vc13 = string.Empty, path_Vc13 = string.Empty;
                    string fileNameOldPic_Vc14 = string.Empty, path_Vc14 = string.Empty;
                    string fileNameOldPic_Vc15 = string.Empty, path_Vc15 = string.Empty;
                    string fileNameOldPic_Vc16 = string.Empty, path_Vc16 = string.Empty;
                    string fileNameOldPic_Vc17 = string.Empty, path_Vc17 = string.Empty;
                    string fileNameOldPic_Vc18 = string.Empty, path_Vc18 = string.Empty;
                    string fileNameOldPic_Vc19 = string.Empty, path_Vc19 = string.Empty;
                    string fileNameOldPic_Vc20 = string.Empty, path_Vc20 = string.Empty;
                    string fileNameOldPic_Vc21 = string.Empty, path_Vc21 = string.Empty;
                    string fileNameOldPic_Vc22 = string.Empty, path_Vc22 = string.Empty;
                    string fileNameOldPic_Vc23 = string.Empty, path_Vc23 = string.Empty;
                    string fileNameOldPic_Vc24 = string.Empty, path_Vc24 = string.Empty;
                    string fileNameOldPic_Vc25 = string.Empty, path_Vc25 = string.Empty;

                    #endregion

                    ValueChainDto request = new ValueChainDto();
                    request.RequestId = newReq;

                    request.vc1 = item != null && !string.IsNullOrEmpty(item.vc1) ? item.vc1 : string.Empty;
                    request.vc2 = item != null && !string.IsNullOrEmpty(item.vc2) ? item.vc2 : string.Empty;
                    request.vc3 = item != null && !string.IsNullOrEmpty(item.vc3) ? item.vc3 : string.Empty;
                    request.vc4 = item != null && !string.IsNullOrEmpty(item.vc4) ? item.vc4 : string.Empty;
                    request.vc5 = item != null && !string.IsNullOrEmpty(item.vc5) ? item.vc5 : string.Empty;
                    request.vc6 = item != null && !string.IsNullOrEmpty(item.vc6) ? item.vc6 : string.Empty;
                    request.vc7 = item != null && !string.IsNullOrEmpty(item.vc7) ? item.vc7 : string.Empty;
                    request.vc8 = item != null && !string.IsNullOrEmpty(item.vc8) ? item.vc8 : string.Empty;
                    request.vc9 =  item != null && !string.IsNullOrEmpty(item.vc9) ?  item.vc9 : string.Empty;
                    request.vc10 = item != null && !string.IsNullOrEmpty(item.vc10) ? item.vc10 : string.Empty;
                    request.vc11 = item != null && !string.IsNullOrEmpty(item.vc11) ? item.vc11 : string.Empty;
                    request.vc12 = item != null && !string.IsNullOrEmpty(item.vc12) ? item.vc12 : string.Empty;
                    request.vc13 = item != null && !string.IsNullOrEmpty(item.vc13) ? item.vc13 : string.Empty;
                    request.vc14 = item != null && !string.IsNullOrEmpty(item.vc14) ? item.vc14 : string.Empty;
                    request.vc15 = item != null && !string.IsNullOrEmpty(item.vc15) ? item.vc15 : string.Empty;
                    request.vc16 = item != null && !string.IsNullOrEmpty(item.vc16) ? item.vc16 : string.Empty;
                    request.vc17 = item != null && !string.IsNullOrEmpty(item.vc17) ? item.vc17 : string.Empty;
                    request.vc18 = item != null && !string.IsNullOrEmpty(item.vc18) ? item.vc18 : string.Empty;
                    request.vc19 = item != null && !string.IsNullOrEmpty(item.vc19) ? item.vc19 : string.Empty;
                    request.vc20 = item != null && !string.IsNullOrEmpty(item.vc20) ? item.vc20 : string.Empty;
                    request.vc21 = item != null && !string.IsNullOrEmpty(item.vc21) ? item.vc21 : string.Empty;
                    request.vc22 = item != null && !string.IsNullOrEmpty(item.vc22) ? item.vc22 : string.Empty;
                    request.vc23 = item != null && !string.IsNullOrEmpty(item.vc23) ? item.vc23 : string.Empty;
                    request.vc24 = item != null && !string.IsNullOrEmpty(item.vc24) ? item.vc24 : string.Empty;
                    request.vc25 = item != null && !string.IsNullOrEmpty(item.vc25) ? item.vc25 : string.Empty;

                    //#region Upload Image

                    if (request.vc1 != null)
                    {
                        fileNameOldPic_Vc1 = request.vc1;
                        request.vc1 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc1);
                        path_Vc1 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc1;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot+ fileNameOldPic_Vc1, path_Vc1, "انبار / سند مالکیت انبار");
                    }
                    if (request.vc2 != null)
                    {
                        fileNameOldPic_Vc2 = request.vc2;
                        request.vc2 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc2);
                        path_Vc2 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc2;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc2, path_Vc2, "عکس/سند تجهیزات انبارش و بارگیری");
                    }
                   
                    if (request.vc3 != null)
                    {
                        fileNameOldPic_Vc3 = request.vc3;
                        request.vc3 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc3);
                        path_Vc3 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc3;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot+ fileNameOldPic_Vc3, path_Vc3, "عکس/سند مالکیت تجهیزات حمل و نقل به/ از  انبار");
                    }
                    if (request.vc4 != null)
                    {
                        fileNameOldPic_Vc4 = request.vc4;
                        request.vc4 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc4);
                        path_Vc4 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc4;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot+ fileNameOldPic_Vc4, path_Vc4, "عکس / قراداد نرم افزار انبارداری");
                    }
                    if (request.vc5 != null)
                    {
                        fileNameOldPic_Vc5 = request.vc5;
                        request.vc5 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc5);
                        path_Vc5 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc5;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc5, path_Vc5, "دفتر مرکزی(اجاره نامه یا سند مالکیت");
                    }
                    if (request.vc6 != null)
                    {
                        fileNameOldPic_Vc6 = request.vc6;
                        request.vc6 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc6);
                        path_Vc6 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc6;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc6, path_Vc6, "دفتر خارج از کشور (اجاره نامه یا سند مالکیت");
                    }
                    if (request.vc7 != null)
                    {
                        fileNameOldPic_Vc7 = request.vc7;
                        request.vc7 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc7);
                        path_Vc7 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc7;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc7, path_Vc7, "یک نمونه فیش حقوقی پرسنل نشان دهنده واریز پاداش");
                    }
                    if (request.vc8 != null)
                    {
                        fileNameOldPic_Vc8 = request.vc8;
                        request.vc8 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc8);
                        path_Vc8 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc8;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot+ fileNameOldPic_Vc8, path_Vc8, "عکس یا قرارداد نرم افزار های یکپارچه مدیریتی");
                    }
                    if (request.vc9 != null)
                    {
                        fileNameOldPic_Vc9 = request.vc9;
                        request.vc9 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc9);
                        path_Vc9 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc9;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc9, path_Vc9, "عکس از آزمایشگاه  (در صورت برون سپاری قرارداد مربوطه");
                    }
                    if (request.vc10 != null)
                    {
                        fileNameOldPic_Vc10 = request.vc10;
                        request.vc10 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc10);
                        path_Vc10 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc10;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot+ fileNameOldPic_Vc10, path_Vc10, "تصویر مجوز دانش بنیان");
                    }
                    if (request.vc11 != null)
                    {
                        fileNameOldPic_Vc11 = request.vc11;
                        request.vc11 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc11);
                        path_Vc11 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc11;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot+ fileNameOldPic_Vc11, path_Vc11, "سند مالکیت یا قراداد یا غیره برای ناوگان حمل و نقل");
                    }
                    if (request.vc12 != null)
                    {
                        fileNameOldPic_Vc12 = request.vc12;
                        request.vc12 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc12);
                        path_Vc12 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc12;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc12, path_Vc12, "درصورت وجود یک مدل جهت ردیابی محصول با بارکد یا کد رجیستری");
                    }
                    if (request.vc13 != null)
                    {
                        fileNameOldPic_Vc13 = request.vc13;
                        request.vc13 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc13);
                        path_Vc13 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc13;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot+ fileNameOldPic_Vc13, path_Vc13, "نمایندگی شرکت  ");
                    }
                    if (request.vc14 != null)
                    {
                        fileNameOldPic_Vc14 = request.vc14;
                        request.vc14 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc14);
                        path_Vc14 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc14;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot+ fileNameOldPic_Vc14, path_Vc14, " شرکت نمایندگان خودد را ارزیابی کرده");
                    }
                    if (request.vc15 != null)
                    {
                        fileNameOldPic_Vc15 = request.vc15;
                        request.vc15 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc15);
                        path_Vc15 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc15;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc15, path_Vc15, " تصویر یا آدرس سایت و فعالیت در شبکه های اجتماعی");
                    }
                    if (request.vc16 != null)
                    {
                        fileNameOldPic_Vc16 = request.vc16;
                        request.vc16 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc16);
                        path_Vc16 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc16;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot +fileNameOldPic_Vc16, path_Vc16, "اجاره نامه / سند مالکیت فروشگاه");
                    }
                    if (request.vc17 != null)
                    {
                        fileNameOldPic_Vc17 = request.vc17;
                        request.vc17 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc17);
                        path_Vc17 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc17;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc17, path_Vc17, "تصویر یا آدرس سایت فروش آنلاین");
                    }
                    if (request.vc18 != null)
                    {
                        fileNameOldPic_Vc18 = request.vc18;
                        request.vc18 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc18);
                        path_Vc18 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc18;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc18, path_Vc18, "گزارش های بازاریابی حضوری");
                    }
                    if (request.vc19 != null)
                    {
                        fileNameOldPic_Vc19 = request.vc19;
                        request.vc19 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc19);
                        path_Vc19 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc19;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc19, path_Vc19, " مجوز حضور در نمایشگاه ها");
                    }
                    if (request.vc20 != null)
                    {
                        fileNameOldPic_Vc20 = request.vc20;
                        request.vc20 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc20);
                        path_Vc20 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc20;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc20, path_Vc20, "قرارداد های فروش به سازمان ها");
                    }
                    if (request.vc21 != null)
                    {
                        fileNameOldPic_Vc21 = request.vc21;
                        request.vc21 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc21);
                        path_Vc21 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc21;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc21, path_Vc21, "نمونه تبلیغات محیطی");
                    }
                    if (request.vc22 != null)
                    {
                        fileNameOldPic_Vc22 = request.vc22;
                        request.vc22 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc22);
                        path_Vc22 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc22;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc22, path_Vc22, "کارت گارانتی");
                    }
                    if (request.vc23 != null)
                    {
                        fileNameOldPic_Vc23 = request.vc23;
                        request.vc23 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc23);
                        path_Vc23 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc23;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc23, path_Vc23, "عکس از واحد خدمات پس از فروش یا سایر مستندات");
                    }
                    if (request.vc24 != null)
                    {
                        fileNameOldPic_Vc24 = request.vc24;
                        request.vc24 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc24);
                        path_Vc24 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc24;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot+ fileNameOldPic_Vc24, path_Vc24, "گزارش و گواهی بررسی و ارزیابی خدمات پس از فروش");
                    }
                    if (request.vc25 != null)
                    {
                        fileNameOldPic_Vc25 = request.vc25;
                        request.vc25 = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fileNameOldPic_Vc25);
                        path_Vc25 = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.vc25;
                        await ServiceFileUploader.CopyFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_Vc25, path_Vc25, "عکس از تجهیزات  واحد خدمات پس از فروش");
                    }
                    //#endregion                                

                    EntityEntry<ValueChain> q_Entity;
                    if (request.ValueChainId == 0)
                    {
                        // request.SaveDate = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);
                        q_Entity = _context.ValueChain.Add(_mapper.Map<ValueChain>(request));
                        await _context.SaveChangesAsync();
                        request = _mapper.Map<ValueChainDto>(q_Entity.Entity);
                    }

                }
                    return new ResultDto<ValueChainDto>()
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
