using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParsKyanCrm.Common;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Common.Enums;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Infrastructure.Consts;
using Microsoft.AspNetCore.Hosting;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveBasicInformationCustomers
{

    public class SaveBasicInformationCustomersService : ISaveBasicInformationCustomersService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IWebHostEnvironment _env;

        public SaveBasicInformationCustomersService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            _env = env;
        }

        private bool Check_Remote(RequestSaveBasicInformationCustomersDto request)
        {
            try
            {
                string strCondition = string.Empty;

                if (!string.IsNullOrEmpty(request.NationalCode))
                {
                    strCondition = " " + nameof(request.NationalCode) + " = " + "N'" + request.NationalCode + "'";
                }

                if (!string.IsNullOrEmpty(strCondition))
                {
                    var q = Ado_NetOperation.GetAll_Table(typeof(Domain.Entities.Customers).Name, "*", strCondition + " AND " + nameof(request.CustomerId) + " != " + request.CustomerId);
                    return q != null && q.Rows.Count > 0 ? false : true;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> Validation_Execute(RequestSaveBasicInformationCustomersDto request)
        {
            try
            {

                ValidationResult result = await new ValidatorRequestSaveBasicInformationCustomersDto().ValidateAsync(request);
                if (!result.IsValid) return result.Errors.GetErrorsF();

                if (!Check_Remote(new RequestSaveBasicInformationCustomersDto() { CustomerId = request.CustomerId, NationalCode = request.NationalCode }))
                {
                    return "شناسه ملی مورد نظر از قبل موجود می باشد لطفا شناسه ملی دیگری وارد نمایید";
                }

                var qRequest = await _context.RequestForRating.FirstOrDefaultAsync(p => p.CustomerId == request.CustomerId && p.IsFinished == false);
                if (qRequest != null)
                {
                    var RequestReferences = await _context.RequestReferences.FirstOrDefaultAsync(p => p.Requestid == qRequest.RequestId);
                    if (RequestReferences != null)
                    {
                        if (RequestReferences.DestLevelStepIndexButton == "ارجاع به مشتری جهت اصلاح مشخصات اولیه توسط مشتری")
                        {
                            return "به دلیل وجود یک درخواست باز امکان ویرایش پروفایل از طرف شما وجود ندارد در صورت نیاز به تغییرات لطفا با مدیر سامانه تماس بگیرید";

                        }
                    }

                    var qCAFD = await _context.ContractAndFinancialDocuments.FirstOrDefaultAsync(p => p.RequestID == qRequest.RequestId);
                    if (qCAFD != null) return "امکان ویرایش پروفایل وجود ندارد چون قرارداد ثبت شده است";

                }


                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string MaxAllRequestNo()
        {
            try
            {
                List<RequestForRatingDto> q = Ado_NetOperation.ConvertDataTableToList<RequestForRatingDto>(Ado_NetOperation.GetAll_Table(typeof(RequestForRating).Name, "cast(isnull((max(cast((isnull(RequestNo,'1')) as bigint))+1),1) as nvarchar(max)) as RequestNoStr"));
                if (q != null) return q.FirstOrDefault().RequestNoStr.ToString();
                return "1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResultDto> Execute(RequestSaveBasicInformationCustomersDto request)
        {

            #region Upload Image

            string fileNameOldPic_LastInsuranceList = string.Empty, path_LastInsuranceList = string.Empty;
            string fileNameOldPic_AuditedFinancialStatements = string.Empty, path_AuditedFinancialStatements = string.Empty;

            #endregion

            try
            {

                #region Validation

                string strValidation = await Validation_Execute(request);
                if (!string.IsNullOrEmpty(strValidation))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = strValidation
                    };
                }

                #endregion                                

                var cus = await _context.Customers.FindAsync(request.CustomerId);
                request.LastInsuranceList = cus != null && !string.IsNullOrEmpty(cus.LastInsuranceList) ? cus.LastInsuranceList : string.Empty;
                request.AuditedFinancialStatements = cus != null && !string.IsNullOrEmpty(cus.AuditedFinancialStatements) ? cus.AuditedFinancialStatements : string.Empty;

                #region Upload Image

                if (request.Result_Final_LastInsuranceList != null)
                {
                    fileNameOldPic_LastInsuranceList = request.LastInsuranceList;
                    request.LastInsuranceList = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_LastInsuranceList.FileName);
                    path_LastInsuranceList = _env.ContentRootPath + VaribleForName.CustomersFolder + request.LastInsuranceList;
                    await ServiceFileUploader.SaveFile(request.Result_Final_LastInsuranceList, path_LastInsuranceList, "آخرین لیست بیمه");
                }

                if (request.Result_Final_AuditedFinancialStatements != null)
                {
                    fileNameOldPic_AuditedFinancialStatements = request.AuditedFinancialStatements;
                    request.AuditedFinancialStatements = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_AuditedFinancialStatements.FileName);
                    path_AuditedFinancialStatements = _env.ContentRootPath + VaribleForName.CustomersFolder + request.AuditedFinancialStatements;
                    await ServiceFileUploader.SaveFile(request.Result_Final_AuditedFinancialStatements, path_AuditedFinancialStatements, "آخرین صورت مالی حسابرسی شده یا اظهار نامه مالیاتی");
                }

                #endregion
                request.CountOfPersonal = (request.CountOfPersonal == 0 ? 1 : request.CountOfPersonal.Value);

                DateTime dt = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);

                if (!cus.IsProfileComplete)
                {

                    if (!request.TypeServiceRequestedId.HasValue)
                    {
                        return new ResultDto()
                        {
                            IsSuccess = false,
                            Message = "نوع خدمت مورد تقاضا را انتخاب کنید"
                        };
                    }

                    var rr = _context.RequestReferences.Add(new RequestReferences()
                    {
                         
                        DestLevelStepIndex = VaribleForName.DestLevelStepIndex,
                        LevelStepAccessRole = VaribleForName.LevelStepAccessRole,
                        LevelStepStatus = VaribleForName.LevelStepStatus,
                        DestLevelStepIndexButton = VaribleForName.DestLevelStepIndexButton,
                        Request = new Domain.Entities.RequestForRating()
                        {

                            RequestNo = int.Parse(MaxAllRequestNo()),
                            DateOfRequest = dt,
                            KindOfRequest = request.TypeServiceRequestedId,
                            CustomerId = cus.CustomerId,
                            IsFinished = false,
                            ChangeDate = dt
                        },
                        Comment = null,
                        SendUser = null,
                        SendTime = dt,
                    });
                    await _context.SaveChangesAsync();

                    _context.RequestReferences.Add(new RequestReferences()
                    {
                        Requestid = rr.Entity.Requestid,
                        Comment = null,
                        SendUser = null,
                        SendTime = dt,
                        DestLevelStepIndex = VaribleForName.DestLevelStepIndex1,
                        LevelStepAccessRole = VaribleForName.LevelStepAccessRole1,
                        LevelStepStatus = VaribleForName.LevelStepStatus1,
                        SmsContent = VaribleForName.SmsContent1,
                        SmsType = VaribleForName.SmsType1,
                        DestLevelStepIndexButton = VaribleForName.DestLevelStepIndexButton1,
                    });
                    await _context.SaveChangesAsync();

                    var aboutEntity = await _context.AboutUs.FirstOrDefaultAsync();
                    WebService.SMSService.Execute(aboutEntity.Mobile1, VaribleForName.SmsContent1);
                    WebService.SMSService.Execute(aboutEntity.Mobile2, VaribleForName.SmsContent1);

                }


                Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.Customers).Name, new Dictionary<string, object>()
                    {
                    {
                        "EmailRepresentative",request.EmailRepresentative
                    },
                    {
                        "NationalCodeRepresentative",request.NationalCodeRepresentative
                    },
                    {
                        "CustomerPersonalityType",request.CustomerPersonalityType
                    },
                    {
                        "TypeGroupCompanies",request.TypeGroupCompanies
                    }
                    ,
                    {
                       nameof(request.EconomicCodeReal),request.EconomicCodeReal
                    },
                    {
                        "SaveDate",dt
                    },
                    {
                        nameof(request.LastInsuranceList),request.LastInsuranceList
                    },
                    {
                        nameof(request.AuditedFinancialStatements),request.AuditedFinancialStatements
                    },
                    {
                        "IsProfileComplete",true
                    },
                        {
                            nameof(request.AgentName),request.AgentName
                        },
                    {
                            nameof(request.CompanyName),request.CompanyName
                        },
                     {
                            nameof(request.CeoNationalCode),request.CeoNationalCode
                        },
                    {
                            nameof(request.KindOfCompanyId),request.KindOfCompanyId
                        },
                    {
                            nameof(request.NationalCode),request.NationalCode
                        },
                    {
                            nameof(request.EconomicCode),request.EconomicCode
                        },
                    {
                            nameof(request.Tel),request.Tel
                        },
                    {
                            nameof(request.AddressCompany),request.AddressCompany
                        },
                     {
                            nameof(request.PostalCode),request.PostalCode
                        },
                    {
                            nameof(request.NamesAuthorizedSignatories),request.NamesAuthorizedSignatories
                        },
                    {
                            nameof(request.CountOfPersonal),request.CountOfPersonal
                        },
                    {
                            nameof(request.AmountOsLastSales),request.AmountOsLastSales
                        },
                    {
                            nameof(request.Email),request.Email
                        },
                    {
                            nameof(request.CeoName),request.CeoName
                        },
                    {
                            nameof(request.CeoMobile),request.CeoMobile
                        },
                    {
                            nameof(request.TypeServiceRequestedId),request.TypeServiceRequestedId
                        },
                    {
                            nameof(request.HowGetKnowCompanyId),request.HowGetKnowCompanyId
                        }
                    }, string.Format(nameof(request.CustomerId) + " = '{0}' ", request.CustomerId));

                #region Upload Image

                if (request.Result_Final_LastInsuranceList != null)
                    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_LastInsuranceList);

                if (request.Result_Final_AuditedFinancialStatements != null)
                    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolder + fileNameOldPic_AuditedFinancialStatements);

                path_LastInsuranceList = string.Empty;
                path_AuditedFinancialStatements = string.Empty;

                #endregion

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "کاربر محترم اطلاعات اولیه شما با موفقیت ثبت گردید از طریق همین ناحیه وضعیت درخواست خود را پیگیری بفرمایید"
                };
            }
            catch (Exception ex)
            {

                #region Upload Image

                FileOperation.DeleteFile(path_LastInsuranceList);
                FileOperation.DeleteFile(path_AuditedFinancialStatements);

                #endregion

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };

            }
        }
    }
}
