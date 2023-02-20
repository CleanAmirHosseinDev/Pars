using AutoMapper;
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
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IWebHostEnvironment _env;

        public SaveCustomersService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
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

            string fileNameOldPic_LastInsuranceList = string.Empty, path_LastInsuranceList = string.Empty;
            string fileNameOldPic_AuditedFinancialStatements = string.Empty, path_AuditedFinancialStatements = string.Empty;

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
                request.LastInsuranceList = cus != null && !string.IsNullOrEmpty(cus.LastInsuranceList) ? cus.LastInsuranceList : string.Empty;
                request.AuditedFinancialStatements = cus != null && !string.IsNullOrEmpty(cus.AuditedFinancialStatements) ? cus.AuditedFinancialStatements : string.Empty;

                #region Upload Image

                if (request.Result_Final_LastInsuranceList != null)
                {
                    fileNameOldPic_LastInsuranceList = request.LastInsuranceList;
                    request.LastInsuranceList = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_LastInsuranceList.FileName);
                    path_LastInsuranceList = _env.ContentRootPath + VaribleForName.CustomersFolder + request.LastInsuranceList;
                    await ServiceFileUploader.SaveFile(request.Result_Final_LastInsuranceList, path_LastInsuranceList, "لیست آخرین بیمه");
                }

                if (request.Result_Final_AuditedFinancialStatements != null)
                {
                    fileNameOldPic_AuditedFinancialStatements = request.AuditedFinancialStatements;
                    request.AuditedFinancialStatements = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_AuditedFinancialStatements.FileName);
                    path_AuditedFinancialStatements = _env.ContentRootPath + VaribleForName.CustomersFolder + request.AuditedFinancialStatements;
                    await ServiceFileUploader.SaveFile(request.Result_Final_AuditedFinancialStatements, path_AuditedFinancialStatements, "لیست آخرین تغییرات روزنامه رسمی");
                }

                #endregion                                

                DateTime dt = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);

                Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.Customers).Name, new Dictionary<string, object>()
                    {                    
                    {
                        nameof(request.LastInsuranceList),request.LastInsuranceList
                    },
                    {
                        nameof(request.AuditedFinancialStatements),request.AuditedFinancialStatements
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
                    Message = "مشتری موردنظر با موفقیت ثبت شد"
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
