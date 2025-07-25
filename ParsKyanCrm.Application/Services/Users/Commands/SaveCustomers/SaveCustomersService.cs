﻿using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveCustomers
{

    public class SaveCustomersService : ISaveCustomersService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        private readonly IWebHostEnvironment _env;

        public SaveCustomersService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;            
            _env = env;
        }

        private bool Check_Remote(CustomersDto request)
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

        private string Validation_Execute(CustomersDto request)
        {
            try
            {

                if (!Check_Remote(new CustomersDto() { CustomerId = request.CustomerId, NationalCode = request.NationalCode }))
                {
                    return "شناسه ملی مورد نظر ار قبل موجود می باشد لطفا شناسه ملی دیگری وارد نمایید";
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResultDto> Execute(CustomersDto request)
        {

            #region Upload Image

            //string fileNameOldPic_LastInsuranceList = string.Empty, path_LastInsuranceList = string.Empty;
            //string fileNameOldPic_AuditedFinancialStatements = string.Empty, path_AuditedFinancialStatements = string.Empty;

            string fileNameOldPic_ScanCustomerNationalCard = string.Empty, path_ScanCustomerNationalCard = string.Empty;
            string fileNameOldPic_ScanManagerNationalCard = string.Empty, path_ScanManagerNationalCard = string.Empty;
            #endregion

            try
            {

                #region Validation

                string strValidation = Validation_Execute(request);
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
                //request.LastInsuranceList = cus != null && !string.IsNullOrEmpty(cus.LastInsuranceList) ? cus.LastInsuranceList : string.Empty;
                //request.AuditedFinancialStatements = cus != null && !string.IsNullOrEmpty(cus.AuditedFinancialStatements) ? cus.AuditedFinancialStatements : string.Empty;

                request.ScanCustomerNationalCard = cus != null && !string.IsNullOrEmpty(cus.ScanCustomerNationalCard) ? cus.ScanCustomerNationalCard : string.Empty;
                request.ScanManagerNationalCard = cus != null && !string.IsNullOrEmpty(cus.ScanManagerNationalCard) ? cus.ScanManagerNationalCard : string.Empty;

                #region Upload Image

                //if (request.Result_Final_LastInsuranceList != null)
                //{
                //    fileNameOldPic_LastInsuranceList = request.LastInsuranceList;
                //    request.LastInsuranceList = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_LastInsuranceList.FileName);
                //    path_LastInsuranceList = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.LastInsuranceList;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_LastInsuranceList, path_LastInsuranceList, "لیست آخرین بیمه");
                //}

                //if (request.Result_Final_AuditedFinancialStatements != null)
                //{
                //    fileNameOldPic_AuditedFinancialStatements = request.AuditedFinancialStatements;
                //    request.AuditedFinancialStatements = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_AuditedFinancialStatements.FileName);
                //    path_AuditedFinancialStatements = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.AuditedFinancialStatements;
                //    await ServiceFileUploader.SaveFile(request.Result_Final_AuditedFinancialStatements, path_AuditedFinancialStatements, "لیست آخرین تغییرات روزنامه رسمی");
                //}

                if (request.Result_Final_ScanCustomerNationalCard != null)
                {
                    fileNameOldPic_ScanCustomerNationalCard = request.ScanCustomerNationalCard;
                    request.ScanCustomerNationalCard = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_ScanCustomerNationalCard.FileName);
                    path_ScanCustomerNationalCard = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.ScanCustomerNationalCard;
                    await ServiceFileUploader.SaveFile(request.Result_Final_ScanCustomerNationalCard, path_ScanCustomerNationalCard, "اسکن کارت ملی نماینده");
                }
                if (request.Result_Final_ScanManagerNationalCard != null)
                {
                    fileNameOldPic_ScanManagerNationalCard = request.ScanManagerNationalCard;
                    request.ScanManagerNationalCard = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_ScanManagerNationalCard.FileName);
                    path_ScanManagerNationalCard = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.ScanManagerNationalCard;
                    await ServiceFileUploader.SaveFile(request.Result_Final_ScanManagerNationalCard, path_ScanManagerNationalCard, "اسکن کارت ملی نماینده");
                }
                #endregion                                

                DateTime dt = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);

                Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.Customers).Name, new Dictionary<string, object>()
                    {
                    {
                        "AgentMobile",request.AgentMobile
                    },
                    {
                        "EmailRepresentative",request.EmailRepresentative
                    },
                    {
                        "NationalCodeRepresentative",request.NationalCodeRepresentative
                    }//,
                    //{
                    //    nameof(request.LastInsuranceList),request.LastInsuranceList
                    //}
                    ,
                    {
                        nameof(request.EconomicCodeReal),request.EconomicCodeReal
                    },
                    //{
                    //    nameof(request.AuditedFinancialStatements),request.AuditedFinancialStatements
                    //},
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
                    //{
                    //        nameof(request.CountOfPersonal),request.CountOfPersonal
                    //    },
                    //{
                    //        nameof(request.AmountOsLastSales),request.AmountOsLastSales
                    //    },
                    {
                            nameof(request.Email),request.Email
                        },
                    {
                            nameof(request.CeoName),request.CeoName
                        },
                    {
                            nameof(request.CeoMobile),request.CeoMobile
                        },
                    //{
                    //        nameof(request.TypeServiceRequestedId),request.TypeServiceRequestedId
                    //    },
                    {
                            nameof(request.HowGetKnowCompanyId),request.HowGetKnowCompanyId
                        },
                    {
                        "ScanCustomerNationalCard",request.ScanCustomerNationalCard
                    },
                    {
                        "ScanManagerNationalCard",request.ScanManagerNationalCard
                    }
                    }, string.Format(nameof(request.CustomerId) + " = '{0}' ", request.CustomerId));
                if (request.ChangeUsername)
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.Users).Name, new Dictionary<string, object>()
                    {
                    {
                        "UserName",request.AgentMobile
                    },
                    {
                        "Mobile",request.AgentMobile
                    }
                    }, string.Format(nameof(request.CustomerId) + " = '{0}' ", request.CustomerId));

                }

                #region Upload Image

                //if (request.Result_Final_LastInsuranceList != null)
                //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_LastInsuranceList);

                //if (request.Result_Final_AuditedFinancialStatements != null)
                //    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_AuditedFinancialStatements);

                if (request.Result_Final_ScanCustomerNationalCard != null)
                    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_ScanCustomerNationalCard);


                if (request.Result_Final_ScanManagerNationalCard != null)
                    FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_ScanManagerNationalCard);

                //path_LastInsuranceList = string.Empty;
                //path_AuditedFinancialStatements = string.Empty;

                path_ScanCustomerNationalCard = string.Empty;

                path_ScanManagerNationalCard = string.Empty;

                #endregion

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "مشتری موردنظر با موفقیت ثبت شد"
                };
            }
            catch (Exception ex)
            {

                #region Upload Image

                //FileOperation.DeleteFile(path_LastInsuranceList);
                //FileOperation.DeleteFile(path_AuditedFinancialStatements);

                FileOperation.DeleteFile(path_ScanCustomerNationalCard);
                FileOperation.DeleteFile(path_ScanManagerNationalCard);
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
