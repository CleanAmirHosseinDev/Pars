using AutoMapper;
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

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveBasicInformationCustomers
{

    public class SaveBasicInformationCustomersService : ISaveBasicInformationCustomersService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public SaveBasicInformationCustomersService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public ResultDto Execute(CustomersDto request)
        {
            try
            {
                Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.Customers).Name, new Dictionary<string, object>()
                    {
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
