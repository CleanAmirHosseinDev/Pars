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

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveBasicInformationCustomers
{

    public class SaveBasicInformationCustomersService : ISaveBasicInformationCustomersService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IValidator<CustomersDto> _validator;

        public SaveBasicInformationCustomersService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IValidator<CustomersDto> validator)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            _validator = validator;
        }

        private bool Check_Remote(CustomersDto request)
        {
            try
            {
                string strCondition = string.Empty;

                if (!string.IsNullOrEmpty(request.Email))
                {
                    strCondition = " " + nameof(request.Email) + " = " + "N'" + request.Email + "'";
                }
                else if (!string.IsNullOrEmpty(request.EconomicCode))
                {
                    strCondition = " " + nameof(request.EconomicCode) + " = " + "N'" + request.EconomicCode + "'";
                }
                else if (!string.IsNullOrEmpty(request.CeoMobile))
                {
                    strCondition = " " + nameof(request.CeoMobile) + " = " + "N'" + request.CeoMobile + "'";
                }
                else if (!string.IsNullOrEmpty(request.NationalCode))
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

        private async Task<string> Validation_Execute(CustomersDto request)
        {
            try
            {

                ValidationResult result = await _validator.ValidateAsync(request);
                if (!result.IsValid) return result.Errors.GetErrorsF();



                if (!Check_Remote(new CustomersDto() { CustomerId = request.CustomerId, NationalCode = request.NationalCode }))
                {
                    return "شناسه ملی مورد نظر ار قبل موجود می باشد لطفا شناسه ملی دیگری وارد نمایید";
                }

                if (!Check_Remote(new CustomersDto() { CustomerId = request.CustomerId, Email = request.Email }))
                {
                    return "پست الکترونیکی مورد نظر ار قبل موجود می باشد لطفا پست الکترونیکی دیگری وارد نمایید";
                }

                if (!Check_Remote(new CustomersDto() { CustomerId = request.CustomerId, CeoMobile = request.CeoMobile }))
                {
                    return "موبایل مدیر عامل مورد نظر ار قبل موجود می باشد لطفا موبایل مدیر عامل دیگری وارد نمایید";
                }

                if (!Check_Remote(new CustomersDto() { CustomerId = request.CustomerId, EconomicCode = request.EconomicCode }))
                {
                    return "کد اقتصادی مورد نظر ار قبل موجود می باشد لطفا کد اقتصادی دیگری وارد نمایید";
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

        public async Task<ResultDto> Execute(CustomersDto request)
        {
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
                if (!cus.IsProfileComplete)
                {

                    var qUser = await _context.Users.FirstOrDefaultAsync(p => p.CustomerId == cus.CustomerId);

                    _context.RequestReferences.Add(new RequestReferences()
                    {
                        LevelStepsId = 1,
                        Request = new Domain.Entities.RequestForRating()
                        {

                            RequestNo = int.Parse(MaxAllRequestNo()),
                            DateOfRequest = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now),
                            Status = (int)RequestForRatingStatus.UnderInvestigation,
                            KindOfRequest = request.TypeServiceRequestedId,
                            CustomerId = cus.CustomerId,
                        },
                        ResiveTime = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now),
                        Comment = "ثبت اولیه مشتری",                        
                        SendUser = qUser.UserId
                    });
                    await _context.SaveChangesAsync();

                }


                Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.Customers).Name, new Dictionary<string, object>()
                    {
                    {
                        nameof(request.IsProfileComplete),true
                    },
                        {
                            nameof(request.AgentName),request.AgentName
                        },
                    {
                            nameof(request.CompanyName),request.CompanyName
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
                            nameof(request.NamesAuthorizedSignatories),request.NamesAuthorizedSignatories
                        },
                    {
                            nameof(request.CountOfPersonal),request.CountOfPersonal
                        },
                    {
                            nameof(request.AmountOsLastSaels),request.AmountOsLastSaels
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

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "کاربر محترم اطلاعات اولیه و درخواست شما با موفقیت ثبت گردید از طریق همین ناحیه وضعیت درخواست خود را پیگیری بفرمایید"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
