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

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveContractAndFinancialDocuments
{

    public class SaveContractAndFinancialDocumentsService : ISaveContractAndFinancialDocumentsService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IWebHostEnvironment _env;
        public SaveContractAndFinancialDocumentsService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            _env = env;
        }

        private string MaxAllContractCode()
        {
            try
            {
                List<ContractAndFinancialDocumentsDto> q = Ado_NetOperation.ConvertDataTableToList<ContractAndFinancialDocumentsDto>(Ado_NetOperation.GetAll_Table(typeof(ContractAndFinancialDocuments).Name, "cast(isnull((max(cast((isnull(ContractCode,'999')) as bigint))+1),1) as nvarchar(max)) as ContractCode"));
                if (q != null) return q.FirstOrDefault().ContractCode.ToString();
                return "117";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string MaxAllContractMainCode()
        {
            try
            {
                List<ContractAndFinancialDocumentsDto> q = Ado_NetOperation.ConvertDataTableToList<ContractAndFinancialDocumentsDto>(Ado_NetOperation.GetAll_Table(typeof(ContractAndFinancialDocuments).Name, "cast(isnull((max(cast((isnull(ContractMainCode,'999')) as bigint))+1),1) as nvarchar(max)) as ContractMainCode"));
                if (q != null) return q.FirstOrDefault().ContractMainCode.ToString();
                return "117";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResultDto<ContractAndFinancialDocumentsDto>> Execute(ContractAndFinancialDocumentsDto request)
        {
            #region Upload Image
            //

            string fileNameOldPic_FinancialDocument = string.Empty, path_FinancialDocument = string.Empty;
            string fileNameOldPic_ContractDocument = string.Empty, path_ContractDocument = string.Empty;
            string fileNameOldPic_EvaluationFile = string.Empty, path_EvaluationFile = string.Empty;
            string fileNameOldPic_ContractDocumentCustomer = string.Empty, path_ContractDocumentCustomer = string.Empty;
            string fileNameOldPic_CommitteeEvaluationFile = string.Empty, path_CommitteeEvaluationFile = string.Empty;
            string fileNameOldPic_LastFinancialDocument = string.Empty, path_LastFinancialDocument = string.Empty;
            string fileNameOldPic_LeaderEvaluationFile = string.Empty, path_LeaderEvaluationFile = string.Empty;
            #endregion
            try
            {
                if (request.DicCountPerecent == 0)
                {
                    request.DicCountPerecent = null;
                }
                #region Validation



                #endregion

                var con = await _context.ContractAndFinancialDocuments.FindAsync(request.FinancialId);
                request.ContractDocument = con != null && !string.IsNullOrEmpty(con.ContractDocument) ? con.ContractDocument : string.Empty;
                request.FinancialDocument = con != null && !string.IsNullOrEmpty(con.FinancialDocument) ? con.FinancialDocument : string.Empty;
                request.EvaluationFile = con != null && !string.IsNullOrEmpty(con.EvaluationFile) ? con.EvaluationFile : string.Empty;
                request.ContractDocumentCustomer = con != null && !string.IsNullOrEmpty(con.ContractDocumentCustomer) ? con.ContractDocumentCustomer : string.Empty;
                request.LastFinancialDocument = con != null && !string.IsNullOrEmpty(con.LastFinancialDocument) ? con.LastFinancialDocument : string.Empty;
                request.CommitteeEvaluationFile = con != null && !string.IsNullOrEmpty(con.CommitteeEvaluationFile) ? con.CommitteeEvaluationFile : string.Empty;
                request.LeaderEvaluationFile = con != null && !string.IsNullOrEmpty(con.LeaderEvaluationFile) ? con.LeaderEvaluationFile : string.Empty;
                #region Upload Image

                if (request.Result_Final_ContractDocument != null)
                {
                    fileNameOldPic_FinancialDocument = request.FinancialDocument;
                    request.FinancialDocument = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_FinancialDocument.FileName);
                    path_FinancialDocument = _env.ContentRootPath + VaribleForName.CustomersFolder + request.FinancialDocument;
                    await ServiceFileUploader.SaveFile(request.Result_Final_FinancialDocument, path_FinancialDocument, "سند تسویه");
                }

                if (request.Result_Final_ContractDocument != null)
                {
                    fileNameOldPic_ContractDocument = request.ContractDocument;
                    request.ContractDocument = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_ContractDocument.FileName);
                    path_ContractDocument = _env.ContentRootPath + VaribleForName.CustomersFolder + request.ContractDocument;
                    await ServiceFileUploader.SaveFile(request.Result_Final_ContractDocument, path_ContractDocument, "قرارداد مشتری");
                }

                if (request.Result_Final_EvaluationFile != null)
                {
                    fileNameOldPic_EvaluationFile = request.EvaluationFile;
                    request.EvaluationFile = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_EvaluationFile.FileName);
                    path_EvaluationFile = _env.ContentRootPath + VaribleForName.CustomersFolder + request.EvaluationFile;
                    await ServiceFileUploader.SaveFile(request.Result_Final_EvaluationFile, path_EvaluationFile, " نتایج ارزیابی");
                }

                if (request.Result_Final_ContractDocumentCustomer != null)
                {
                    fileNameOldPic_EvaluationFile = request.ContractDocumentCustomer;
                    request.ContractDocumentCustomer = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_ContractDocumentCustomer.FileName);
                    path_ContractDocumentCustomer = _env.ContentRootPath + VaribleForName.CustomersFolder + request.ContractDocumentCustomer;
                    await ServiceFileUploader.SaveFile(request.Result_Final_ContractDocumentCustomer, path_ContractDocumentCustomer, " فرم بدون امضا مشتری");
                }

                if (request.Result_Final_CommitteeEvaluationFile != null)
                {
                    fileNameOldPic_CommitteeEvaluationFile = request.CommitteeEvaluationFile;
                    request.CommitteeEvaluationFile = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_CommitteeEvaluationFile.FileName);
                    path_CommitteeEvaluationFile = _env.ContentRootPath + VaribleForName.CustomersFolder + request.CommitteeEvaluationFile;
                    await ServiceFileUploader.SaveFile(request.Result_Final_CommitteeEvaluationFile, path_CommitteeEvaluationFile, "فایل ارزیابی کمیته");
                }

                if (request.Result_Final_LastFinancialDocument != null)
                {
                    fileNameOldPic_LastFinancialDocument = request.LastFinancialDocument;
                    request.LastFinancialDocument = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_LastFinancialDocument.FileName);
                    path_LastFinancialDocument = _env.ContentRootPath + VaribleForName.CustomersFolder + request.LastFinancialDocument;
                    await ServiceFileUploader.SaveFile(request.Result_Final_LastFinancialDocument, path_LastFinancialDocument, "فایل تسویه نهایی");
                }

                if (request.Result_Final_LeaderEvaluationFile != null)
                {
                    fileNameOldPic_LeaderEvaluationFile = request.LeaderEvaluationFile;
                    request.LeaderEvaluationFile = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_LeaderEvaluationFile.FileName);
                    path_LeaderEvaluationFile = _env.ContentRootPath + VaribleForName.CustomersFolder + request.LeaderEvaluationFile;
                    await ServiceFileUploader.SaveFile(request.Result_Final_LeaderEvaluationFile, path_LeaderEvaluationFile, "فایل ارزیابی مسول امور ازیابی");
                }

                #endregion                

                EntityEntry<ContractAndFinancialDocuments> q_Entity;
                if (request.FinancialId == 0)
                {
                    request.SaveDate = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);

                    if (request.IsCustomer)
                    {
                        request.ContractCode = MaxAllContractCode();
                    }
                    //else
                    //{
                    //    request.ContractMainCode = MaxAllContractMainCode();
                    //}

                    request.Tax = Math.Round((request.FinalPriceContract.HasValue ? request.FinalPriceContract.Value * 9 : 0) / 100, 0);
                    q_Entity = _context.ContractAndFinancialDocuments.Add(_mapper.Map<ContractAndFinancialDocuments>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<ContractAndFinancialDocumentsDto>(q_Entity.Entity);
                }
                else
                {
                    var dicSqlUpdate = new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.FinancialDocument),request.FinancialDocument
                        },
                        {
                            nameof(q_Entity.Entity.ContractDocument),request.ContractDocument
                        },
                        {
                            nameof(q_Entity.Entity.RequestID),request.RequestID
                        },
                        {
                            nameof(q_Entity.Entity.ContentContract),request.ContentContract
                        },
                         {
                            nameof(q_Entity.Entity.FinalPriceContract),request.FinalPriceContract
                        },
                        {
                            nameof(q_Entity.Entity.PriceContract),request.PriceContract
                        },
                        {
                            nameof(q_Entity.Entity.Tax),Math.Round((request.PriceContract.HasValue?request.FinalPriceContract.Value * 9:0)/100,0)
                        },
                        {
                            nameof(q_Entity.Entity.DicCountPerecent),request.DicCountPerecent
                        }
                        ,
                        {
                            nameof(q_Entity.Entity.DisCountMoney),request.DisCountMoney
                        }
                        ,
                        {
                            nameof(q_Entity.Entity.EvaluationFile),request.EvaluationFile
                        },
                        {
                            nameof(q_Entity.Entity.ContractDocumentCustomer),request.ContractDocumentCustomer
                        },
                        {
                            nameof(q_Entity.Entity.LastFinancialDocument),request.LastFinancialDocument
                        }
                        ,
                        {
                            nameof(q_Entity.Entity.CommitteeEvaluationFile),request.CommitteeEvaluationFile
                        },
                        {
                            nameof(q_Entity.Entity.LeaderEvaluationFile),request.LeaderEvaluationFile
                        }
                        ,
                        {
                            nameof(q_Entity.Entity.ConfirmCommitteeEvaluation),request.ConfirmCommitteeEvaluation=="on"?true:false
                        },


                    };
                    if (request.IsCustomer && string.IsNullOrEmpty(con.ContractCode))
                    {
                        dicSqlUpdate.Add("ContractCode", MaxAllContractCode());
                        dicSqlUpdate.Add("SaveDate", DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now));
                    }

                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.ContractAndFinancialDocuments).Name, dicSqlUpdate, string.Format(nameof(q_Entity.Entity.FinancialId) + " = {0} ", request.FinancialId));

                    #region Upload Image

                    if (request.Result_Final_ContractDocument != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_ContractDocument);

                    if (request.Result_Final_FinancialDocument != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_FinancialDocument);

                    if (request.Result_Final_EvaluationFile != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_EvaluationFile);

                    if (request.Result_Final_ContractDocumentCustomer != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_ContractDocumentCustomer);

                    if (request.Result_Final_LastFinancialDocument != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_LastFinancialDocument);

                    if (request.Result_Final_CommitteeEvaluationFile != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_CommitteeEvaluationFile);

                    if (request.Result_Final_LeaderEvaluationFile != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_LeaderEvaluationFile);

                    path_ContractDocument = string.Empty;
                    path_FinancialDocument = string.Empty;
                    path_EvaluationFile = string.Empty;
                    path_ContractDocumentCustomer = string.Empty;
                    path_LastFinancialDocument = string.Empty;
                    path_CommitteeEvaluationFile = string.Empty;
                    path_LeaderEvaluationFile = string.Empty;

                    #endregion
                }

                return new ResultDto<ContractAndFinancialDocumentsDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت نرخ نامه قرارداد با موفقیت انجام شد",
                    Data = request
                };


            }
            catch (Exception ex)
            {
                #region Upload Image

                FileOperation.DeleteFile(path_ContractDocument);
                FileOperation.DeleteFile(path_FinancialDocument);
                FileOperation.DeleteFile(path_EvaluationFile);
                FileOperation.DeleteFile(path_ContractDocumentCustomer);
                FileOperation.DeleteFile(path_CommitteeEvaluationFile);
                FileOperation.DeleteFile(path_LastFinancialDocument);
                FileOperation.DeleteFile(path_LeaderEvaluationFile);
                #endregion
                throw ex;
            }
        }


        //public async Task<ResultDto<ContractAndFinancialDocumentsDto>> Execute(ContractAndFinancialDocumentsDto request)
        //{
        //    #region Upload Image
        //    //

        //    string fileNameOldPic_FinancialDocument = string.Empty, path_FinancialDocument = string.Empty;
        //    string fileNameOldPic_ContractDocument = string.Empty, path_ContractDocument = string.Empty;
        //    string fileNameOldPic_EvaluationFile = string.Empty, path_EvaluationFile = string.Empty;
        //    string fileNameOldPic_ContractDocumentCustomer = string.Empty, path_ContractDocumentCustomer = string.Empty;
        //    string fileNameOldPic_CommitteeEvaluationFile = string.Empty, path_CommitteeEvaluationFile = string.Empty;
        //    string fileNameOldPic_LastFinancialDocument = string.Empty, path_LastFinancialDocument = string.Empty;

        //    #endregion
        //    try
        //    {

        //        #region Validation



        //        #endregion

        //        request.Values = request.Values != null ? request.Values : new List<NormalJsonClassDto>();

        //        var con = await _context.ContractAndFinancialDocuments.FindAsync(request.FinancialId);
        //        request.ContractDocument = con != null && !string.IsNullOrEmpty(con.ContractDocument) ? con.ContractDocument : string.Empty;
        //        request.FinancialDocument = con != null && !string.IsNullOrEmpty(con.FinancialDocument) ? con.FinancialDocument : string.Empty;
        //        request.EvaluationFile = con != null && !string.IsNullOrEmpty(con.EvaluationFile) ? con.EvaluationFile : string.Empty;
        //        request.ContractDocumentCustomer = con != null && !string.IsNullOrEmpty(con.ContractDocumentCustomer) ? con.ContractDocumentCustomer : string.Empty;
        //        request.LastFinancialDocument = con != null && !string.IsNullOrEmpty(con.LastFinancialDocument) ? con.LastFinancialDocument : string.Empty;
        //        request.CommitteeEvaluationFile = con != null && !string.IsNullOrEmpty(con.CommitteeEvaluationFile) ? con.CommitteeEvaluationFile : string.Empty;

        //        #region Upload Image

        //        if (request.Result_Final_FinancialDocument != null)
        //        {
        //            fileNameOldPic_FinancialDocument = request.FinancialDocument;
        //            request.FinancialDocument = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_FinancialDocument.FileName);
        //            path_FinancialDocument = _env.ContentRootPath + VaribleForName.CustomersFolder + request.FinancialDocument;

        //            await ServiceFileUploader.SaveFile(request.Result_Final_FinancialDocument, path_FinancialDocument, "سند تسویه");

        //            request.Values.Add(new NormalJsonClassDto()
        //            {
        //                Text = "FinancialDocument",
        //                ValueObj = request.FinancialDocument
        //            });
        //        }

        //        if (request.Result_Final_ContractDocument != null)
        //        {
        //            fileNameOldPic_ContractDocument = request.ContractDocument;
        //            request.ContractDocument = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_ContractDocument.FileName);
        //            path_ContractDocument = _env.ContentRootPath + VaribleForName.CustomersFolder + request.ContractDocument;
        //            await ServiceFileUploader.SaveFile(request.Result_Final_ContractDocument, path_ContractDocument, "قرارداد مشتری");
        //            request.Values.Add(new NormalJsonClassDto()
        //            {
        //                Text = "ContractDocument",
        //                ValueObj = request.ContractDocument
        //            });
        //        }

        //        if (request.Result_Final_EvaluationFile != null)
        //        {
        //            fileNameOldPic_EvaluationFile = request.EvaluationFile;
        //            request.EvaluationFile = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_EvaluationFile.FileName);
        //            path_EvaluationFile = _env.ContentRootPath + VaribleForName.CustomersFolder + request.EvaluationFile;
        //            await ServiceFileUploader.SaveFile(request.Result_Final_EvaluationFile, path_EvaluationFile, " نتایج ارزیابی");
        //            request.Values.Add(new NormalJsonClassDto()
        //            {
        //                Text = "EvaluationFile",
        //                ValueObj = request.EvaluationFile
        //            });
        //        }

        //        if (request.Result_Final_ContractDocumentCustomer != null)
        //        {
        //            fileNameOldPic_EvaluationFile = request.ContractDocumentCustomer;
        //            request.ContractDocumentCustomer = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_ContractDocumentCustomer.FileName);
        //            path_ContractDocumentCustomer = _env.ContentRootPath + VaribleForName.CustomersFolder + request.ContractDocumentCustomer;
        //            await ServiceFileUploader.SaveFile(request.Result_Final_ContractDocumentCustomer, path_ContractDocumentCustomer, " فرم بدون امضا مشتری");
        //            request.Values.Add(new NormalJsonClassDto()
        //            {
        //                Text = "ContractDocumentCustomer",
        //                ValueObj = request.ContractDocumentCustomer
        //            });
        //        }

        //        if (request.Result_Final_CommitteeEvaluationFile != null)
        //        {
        //            fileNameOldPic_CommitteeEvaluationFile = request.CommitteeEvaluationFile;
        //            request.CommitteeEvaluationFile = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_CommitteeEvaluationFile.FileName);
        //            path_CommitteeEvaluationFile = _env.ContentRootPath + VaribleForName.CustomersFolder + request.CommitteeEvaluationFile;
        //            await ServiceFileUploader.SaveFile(request.Result_Final_CommitteeEvaluationFile, path_CommitteeEvaluationFile, "فایل ارزیابی کمیته");
        //            request.Values.Add(new NormalJsonClassDto()
        //            {
        //                Text = "CommitteeEvaluationFile",
        //                ValueObj = request.CommitteeEvaluationFile
        //            });
        //        }
        //        if (request.Result_Final_LastFinancialDocument != null)
        //        {
        //            fileNameOldPic_LastFinancialDocument = request.LastFinancialDocument;
        //            request.LastFinancialDocument = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_LastFinancialDocument.FileName);
        //            path_LastFinancialDocument = _env.ContentRootPath + VaribleForName.CustomersFolder + request.LastFinancialDocument;
        //            await ServiceFileUploader.SaveFile(request.Result_Final_LastFinancialDocument, path_LastFinancialDocument, "فایل تسویه نهایی");
        //            request.Values.Add(new NormalJsonClassDto()
        //            {
        //                Text = "LastFinancialDocument",
        //                ValueObj = request.LastFinancialDocument
        //            });

        //        }

        //        #endregion                

        //        EntityEntry<ContractAndFinancialDocuments> q_Entity;
        //        if (request.FinancialId == 0)
        //        {
        //            request.SaveDate = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);

        //            if (!request.IsCustomer)
        //            {
        //                request.ContractCode = MaxAllContractCode();
        //            }
        //            else
        //            {
        //                request.ContractMainCode = MaxAllContractMainCode();
        //            }

        //            request.Tax = Math.Round((request.PriceContract.HasValue ? request.PriceContract.Value * 9 : 0) / 100, 0);
        //            q_Entity = _context.ContractAndFinancialDocuments.Add(_mapper.Map<ContractAndFinancialDocuments>(request));
        //            await _context.SaveChangesAsync();
        //            request = _mapper.Map<ContractAndFinancialDocumentsDto>(q_Entity.Entity);
        //        }
        //        else
        //        {

        //            request.Values.Add(new NormalJsonClassDto()
        //            {
        //                Text = "SaveDate",
        //                ValueObj = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now)
        //            });
        //            request.Values.Add(new NormalJsonClassDto()
        //            {
        //                Text = request.IsCustomer ? "ContractMainCode" : "ContractCode",
        //                ValueObj = request.IsCustomer ? MaxAllContractMainCode() : MaxAllContractCode()
        //            });

        //            var q_Dic = new Dictionary<string, object>();
        //            foreach (var item in request.Values) q_Dic.Add(item.Text, item.ValueObj);


        //            Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.ContractAndFinancialDocuments).Name, q_Dic, string.Format("FinancialId" + " = {0} ", request.FinancialId));


        //            #region Upload Image

        //            if (request.Result_Final_ContractDocument != null)
        //                FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_ContractDocument);

        //            if (request.Result_Final_FinancialDocument != null)
        //                FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_FinancialDocument);

        //            if (request.Result_Final_EvaluationFile != null)
        //                FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_EvaluationFile);

        //            if (request.Result_Final_ContractDocumentCustomer != null)
        //                FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_ContractDocumentCustomer);

        //            if (request.Result_Final_LastFinancialDocument != null)
        //                FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_LastFinancialDocument);

        //            if (request.Result_Final_CommitteeEvaluationFile != null)
        //                FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_CommitteeEvaluationFile);

        //            path_ContractDocument = string.Empty;
        //            path_FinancialDocument = string.Empty;
        //            path_EvaluationFile = string.Empty;
        //            path_ContractDocumentCustomer = string.Empty;
        //            path_LastFinancialDocument = string.Empty;
        //            path_CommitteeEvaluationFile = string.Empty;

        //            #endregion
        //        }

        //        return new ResultDto<ContractAndFinancialDocumentsDto>()
        //        {
        //            IsSuccess = true,
        //            Message = "ثبت نرخ نامه قرارداد با موفقیت انجام شد",
        //            Data = request
        //        };


        //    }
        //    catch (Exception ex)
        //    {
        //        #region Upload Image

        //        FileOperation.DeleteFile(path_ContractDocument);
        //        FileOperation.DeleteFile(path_FinancialDocument);
        //        FileOperation.DeleteFile(path_EvaluationFile);
        //        FileOperation.DeleteFile(path_ContractDocumentCustomer);
        //        FileOperation.DeleteFile(path_CommitteeEvaluationFile);
        //        FileOperation.DeleteFile(path_LastFinancialDocument);
        //        #endregion
        //        throw ex;
        //    }
        //}
    }
}
