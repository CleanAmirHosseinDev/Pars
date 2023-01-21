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

        public async Task<ResultDto<ContractAndFinancialDocumentsDto>> Execute(ContractAndFinancialDocumentsDto request)
        {
            #region Upload Image

            string fileNameOldPic_FinancialDocument = string.Empty, path_FinancialDocument = string.Empty;
            string fileNameOldPic_ContractDocument = string.Empty, path_ContractDocument = string.Empty;

            #endregion
            try
            {
               
                #region Validation



                #endregion

                var con = await _context.ContractAndFinancialDocuments.FindAsync(request.FinancialId);
                request.ContractDocument = con != null && !string.IsNullOrEmpty(con.ContractDocument) ? con.ContractDocument : string.Empty;
                request.FinancialDocument = con != null && !string.IsNullOrEmpty(con.FinancialDocument) ? con.FinancialDocument : string.Empty;

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

                #endregion                                

                EntityEntry<ContractAndFinancialDocuments> q_Entity;
                if (request.FinancialId == 0)
                {
                    request.SaveDate = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);
                    request.Tax = Math.Round((request.PriceContract.HasValue ? request.PriceContract.Value * 9 : 0) / 100, 0);
                    q_Entity = _context.ContractAndFinancialDocuments.Add(_mapper.Map<ContractAndFinancialDocuments>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<ContractAndFinancialDocumentsDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.ContractAndFinancialDocuments).Name, new Dictionary<string, object>()
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
                            nameof(q_Entity.Entity.PriceContract),request.PriceContract
                        },
                        {
                            nameof(q_Entity.Entity.Tax),Math.Round((request.PriceContract.HasValue?request.PriceContract.Value * 9:0)/100,0)
                        },
                        {
                            nameof(q_Entity.Entity.SaveDate),DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now)
                        },
                    }, string.Format(nameof(q_Entity.Entity.FinancialId) + " = {0} ", request.FinancialId));
                    #region Upload Image

                    if (request.Result_Final_ContractDocument != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_ContractDocument);

                    if (request.Result_Final_FinancialDocument != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_FinancialDocument);

                    path_ContractDocument = string.Empty;
                    path_FinancialDocument = string.Empty;

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

                #endregion
                throw ex;
            }
        }
    }
}
