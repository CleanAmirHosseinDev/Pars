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

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveCorporateGovernances
{
    public class SaveCorporateGovernanceService : ISaveCorporateGovernanceService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IWebHostEnvironment _env;
        public SaveCorporateGovernanceService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            _env = env;
        }

        public async Task<ResultDto<CorporateGovernanceDto>> Execute(CorporateGovernanceDto request)
        {
            #region Upload Image
            //
            //OrganazationChart
            //OrganizationalDuties
            //RiskManagementGuidelines
            //TransactionRegulations
            //DeductionTaxAccount
            //CrmSoftwareContract
            //RepresentativeFile
            //letterOfCommendation
            //InovationFile
            //Proceedings

            string fileNameOldPic_OrganazationChart = string.Empty, path_OrganazationChart = string.Empty;
            string fileNameOldPic_OrganizationalDuties = string.Empty, path_OrganizationalDuties = string.Empty;
            string fileNameOldPic_RiskManagementGuidelines = string.Empty, path_RiskManagementGuidelines = string.Empty;
            string fileNameOldPic_TransactionRegulations = string.Empty, path_TransactionRegulations = string.Empty;
            string fileNameOldPic_DeductionTaxAccount = string.Empty, path_DeductionTaxAccount = string.Empty;
            string fileNameOldPic_CrmSoftwareContract = string.Empty, path_CrmSoftwareContract = string.Empty;
            string fileNameOldPic_RepresentativeFile = string.Empty, path_RepresentativeFile = string.Empty;
            string fileNameOldPic_LetterOfCommendation = string.Empty, path_LetterOfCommendation = string.Empty;
            string fileNameOldPic_InovationFile = string.Empty, path_InovationFile = string.Empty;
            string fileNameOldPic_Proceedings = string.Empty, path_Proceedings = string.Empty;

            #endregion
            try
            {

                if (request.HaveAuditCommitteeStr!=null)
                {
                   if( request.HaveAuditCommitteeStr=="on")
                    {
                        request.HaveAuditCommittee = true;
                    }else
                    {
                        request.HaveAuditCommittee = false;
                    }
                }
                if (request.HaveRepresentativeStr != null)
                {
                    if (request.HaveRepresentativeStr == "on")
                    {
                        request.HaveRepresentative = true;
                    }
                    else
                    {
                        request.HaveRepresentative = false;
                    }
                }
                //شروع کنیم yes tnx

                #region Validation



                #endregion

                var con = await _context.CorporateGovernance.FindAsync(request.CorporateGovernanceId);
                request.OrganazationChart = con != null && !string.IsNullOrEmpty(con.OrganazationChart) ? con.OrganazationChart : string.Empty;
                request.OrganizationalDuties = con != null && !string.IsNullOrEmpty(con.OrganizationalDuties) ? con.OrganizationalDuties : string.Empty;
                request.RiskManagementGuidelines = con != null && !string.IsNullOrEmpty(con.RiskManagementGuidelines) ? con.RiskManagementGuidelines : string.Empty;
                request.TransactionRegulations = con != null && !string.IsNullOrEmpty(con.TransactionRegulations) ? con.TransactionRegulations : string.Empty;
                request.DeductionTaxAccount = con != null && !string.IsNullOrEmpty(con.DeductionTaxAccount) ? con.DeductionTaxAccount : string.Empty;
                request.CrmSoftwareContract = con != null && !string.IsNullOrEmpty(con.CrmSoftwareContract) ? con.CrmSoftwareContract : string.Empty;
                request.RepresentativeFile = con != null && !string.IsNullOrEmpty(con.TransactionRegulations) ? con.TransactionRegulations : string.Empty;
                request.LetterOfCommendation = con != null && !string.IsNullOrEmpty(con.LetterOfCommendation) ? con.LetterOfCommendation : string.Empty;
                request.InovationFile = con != null && !string.IsNullOrEmpty(con.InovationFile) ? con.InovationFile : string.Empty;
                request.Proceedings = con != null && !string.IsNullOrEmpty(con.Proceedings) ? con.Proceedings : string.Empty;

                //#region Upload Image

                if (request.Result_Final_OrganazationChart != null)
                {
                    fileNameOldPic_OrganazationChart = request.OrganazationChart;
                    request.OrganazationChart = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_OrganazationChart.FileName);
                    path_OrganazationChart = _env.ContentRootPath + VaribleForName.CustomersFolder + request.OrganazationChart;
                    await ServiceFileUploader.SaveFile(request.Result_Final_OrganazationChart, path_OrganazationChart, "چارت سازمانی");
                }

                if (request.Result_Final_OrganizationalDuties != null)
                {
                    fileNameOldPic_OrganizationalDuties = request.OrganizationalDuties;
                    request.OrganizationalDuties = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_OrganizationalDuties.FileName);
                    path_OrganizationalDuties = _env.ContentRootPath + VaribleForName.CustomersFolder + request.OrganizationalDuties;
                    await ServiceFileUploader.SaveFile(request.Result_Final_OrganizationalDuties, path_OrganizationalDuties, "  فایل شرح وظایف شغلی پرسنل");
                }

                if (request.Result_Final_RiskManagementGuidelines != null)
                {
                    fileNameOldPic_RiskManagementGuidelines = request.RiskManagementGuidelines;
                    request.RiskManagementGuidelines = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_RiskManagementGuidelines.FileName);
                    path_RiskManagementGuidelines = _env.ContentRootPath + VaribleForName.CustomersFolder + request.RiskManagementGuidelines;
                    await ServiceFileUploader.SaveFile(request.Result_Final_RiskManagementGuidelines, path_RiskManagementGuidelines, " دستورالعمل مدیریت ریسک");
                }

                if (request.Result_Final_TransactionRegulations != null)
                {
                    fileNameOldPic_TransactionRegulations = request.TransactionRegulations;
                    request.TransactionRegulations = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_TransactionRegulations.FileName);
                    path_TransactionRegulations = _env.ContentRootPath + VaribleForName.CustomersFolder + request.TransactionRegulations;
                    await ServiceFileUploader.SaveFile(request.Result_Final_TransactionRegulations, path_TransactionRegulations, "  آیین نامه معاملات");
                }

                if (request.Result_Final_DeductionTaxAccount != null)
                {
                    fileNameOldPic_DeductionTaxAccount = request.DeductionTaxAccount;
                    request.DeductionTaxAccount = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_DeductionTaxAccount.FileName);
                    path_DeductionTaxAccount = _env.ContentRootPath + VaribleForName.CustomersFolder + request.DeductionTaxAccount;
                    await ServiceFileUploader.SaveFile(request.Result_Final_DeductionTaxAccount, path_DeductionTaxAccount, "بارگزاری مفاصا حساب مالیاتی");
                }

                if (request.Result_Final_RepresentativeFile != null)
                {
                    fileNameOldPic_RepresentativeFile = request.RepresentativeFile;
                    request.RepresentativeFile = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_RepresentativeFile.FileName);
                    path_RepresentativeFile = _env.ContentRootPath + VaribleForName.CustomersFolder + request.RepresentativeFile;
                    await ServiceFileUploader.SaveFile(request.Result_Final_RepresentativeFile, path_RepresentativeFile, " مستندات نمایندگی");
                }
                if (request.Result_Final_LetterOfCommendation != null)
                {
                    fileNameOldPic_LetterOfCommendation = request.LetterOfCommendation;
                    request.LetterOfCommendation = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_LetterOfCommendation.FileName);
                    path_LetterOfCommendation = _env.ContentRootPath + VaribleForName.CustomersFolder + request.LetterOfCommendation;
                    await ServiceFileUploader.SaveFile(request.Result_Final_LetterOfCommendation, path_LetterOfCommendation, "تقدیر نامه از تامین کنندگان یا مشتریان خود ");
                }
                if (request.Result_Final_CrmSoftwareContract != null)
                {
                    fileNameOldPic_CrmSoftwareContract = request.CrmSoftwareContract;
                    request.CrmSoftwareContract = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_CrmSoftwareContract.FileName);
                    path_CrmSoftwareContract = _env.ContentRootPath + VaribleForName.CustomersFolder + request.CrmSoftwareContract;
                    await ServiceFileUploader.SaveFile(request.Result_Final_CrmSoftwareContract, path_CrmSoftwareContract, "   تصویرقراداد نرم افزار امور مشتریان");
                }

                if (request.Result_Final_InovationFile != null)
                {
                    fileNameOldPic_InovationFile = request.InovationFile;
                    request.InovationFile = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_InovationFile.FileName);
                    path_InovationFile = _env.ContentRootPath + VaribleForName.CustomersFolder + request.InovationFile;
                    await ServiceFileUploader.SaveFile(request.Result_Final_InovationFile, path_InovationFile, " اختراع");
                }
                if (request.Result_Final_Proceedings != null)
                {
                    fileNameOldPic_Proceedings = request.Proceedings;
                    request.Proceedings = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_Proceedings.FileName);
                    path_Proceedings = _env.ContentRootPath + VaribleForName.CustomersFolder + request.Proceedings;
                    await ServiceFileUploader.SaveFile(request.Result_Final_Proceedings, path_Proceedings, " نمونه صورتجلسه حسابرسی");
                }

                //#endregion                                

                EntityEntry<CorporateGovernance> q_Entity;
                if (request.CorporateGovernanceId == 0)
                {
                    // request.SaveDate = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);
                    q_Entity = _context.CorporateGovernance.Add(_mapper.Map<CorporateGovernance>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<CorporateGovernanceDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.CorporateGovernance).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.RequestId),request.RequestId
                        },
                        {
                            nameof(q_Entity.Entity.CompanyWebSite),request.CompanyWebSite
                        },
                        {
                            nameof(q_Entity.Entity.CrmSoftwareContract),request.CrmSoftwareContract
                        },
                        {
                            nameof(q_Entity.Entity.DeductionTaxAccount),request.DeductionTaxAccount
                        },
                       
                        {
                            nameof(q_Entity.Entity.HaveAuditCommittee),request.HaveAuditCommittee
                        },
                        {
                            nameof(q_Entity.Entity.HaveRepresentative),request.HaveRepresentative
                        }
                        ,
                        {
                            nameof(q_Entity.Entity.HighProductKnowledge),request.HighProductKnowledge
                        }
                        ,
                        {
                            nameof(q_Entity.Entity.InovationFile),request.InovationFile
                        }
                         ,
                        {
                            nameof(q_Entity.Entity.LetterOfCommendation),request.LetterOfCommendation
                        } ,
                        {
                            nameof(q_Entity.Entity.OrganazationChart),request.OrganazationChart
                        },
                        {
                            nameof(q_Entity.Entity.OrganizationalDuties),request.OrganizationalDuties
                        }
                        ,
                        {
                            nameof(q_Entity.Entity.Proceedings),request.Proceedings
                        }
                        ,
                        {
                            nameof(q_Entity.Entity.RepresentativeFile),request.RepresentativeFile
                        }
                        ,
                        {
                            nameof(q_Entity.Entity.RiskManagementGuidelines),request.RiskManagementGuidelines
                        },
                        {
                            nameof(q_Entity.Entity.TransactionRegulations),request.TransactionRegulations
                        }
                    }, string.Format(nameof(q_Entity.Entity.RequestId) + " = {0} ", request.RequestId));
                    #region Upload Image

                    if (request.Result_Final_CrmSoftwareContract != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_CrmSoftwareContract);

                    if (request.Result_Final_DeductionTaxAccount != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_DeductionTaxAccount);

                    if (request.Result_Final_InovationFile != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_InovationFile);

                    if (request.Result_Final_LetterOfCommendation != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_LetterOfCommendation);

                    if (request.Result_Final_OrganazationChart != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_OrganazationChart);

                    if (request.Result_Final_OrganizationalDuties != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_OrganizationalDuties);

                    if (request.Result_Final_Proceedings != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_Proceedings);

                    if (request.Result_Final_RepresentativeFile != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_RepresentativeFile);

                    if (request.Result_Final_RiskManagementGuidelines != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_RiskManagementGuidelines);

                    if (request.Result_Final_TransactionRegulations != null)
                        FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_TransactionRegulations);

                 

                    path_CrmSoftwareContract = string.Empty;
                    path_DeductionTaxAccount = string.Empty;
                    path_InovationFile = string.Empty;
                    path_LetterOfCommendation = string.Empty;
                    path_OrganazationChart = string.Empty;
                    path_OrganizationalDuties = string.Empty;
                    path_Proceedings = string.Empty;
                    path_RepresentativeFile = string.Empty;
                    path_RiskManagementGuidelines = string.Empty;
                    path_TransactionRegulations = string.Empty;
                    #endregion
                }

                return new ResultDto<CorporateGovernanceDto>()
                {
                    IsSuccess = true,
                    Message = "اطلاعات با موفقیت انجام شد",
                    Data = request
                };


            }
            catch (Exception ex)
            {
                #region Upload Image

                FileOperation.DeleteFile(path_Proceedings);
                FileOperation.DeleteFile(path_InovationFile);
                FileOperation.DeleteFile(path_CrmSoftwareContract);
                FileOperation.DeleteFile(path_DeductionTaxAccount);
                FileOperation.DeleteFile(path_InovationFile);
                FileOperation.DeleteFile(path_LetterOfCommendation);
                FileOperation.DeleteFile(path_OrganazationChart);
                FileOperation.DeleteFile(path_OrganizationalDuties);
                FileOperation.DeleteFile(path_RiskManagementGuidelines);
                FileOperation.DeleteFile(path_RepresentativeFile);
                FileOperation.DeleteFile(path_TransactionRegulations);
               
                #endregion
                throw ex;
            }
        }
    }
}
