using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetServiceFeeAndCustomerByRequest
{

    public class GetServiceFeeAndCustomerByRequestService : IGetServiceFeeAndCustomerByRequestService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetServiceFeeAndCustomerByRequestService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ResultGetServiceFeeAndCustomerByRequestDto> Execute(int ri)
        {

            try
            {
                var qRequest = await _context.RequestForRating.FindAsync(ri);

                var qCustomer = await _context.Customers.FindAsync(qRequest.CustomerId);

                var qContract = await _context.Contract.FirstOrDefaultAsync(m => m.KinfOfRequest == qRequest.KindOfRequest && m.IsActive == 15) ;

                // جا گذاری مقادیر در متن قرارداد
                string strContract = qContract.ContractText;
                var qKindOfCompany = await _context.SystemSeting.FindAsync(qCustomer.KindOfCompanyId);

                int cOP = qCustomer.CountOfPersonal.HasValue ? qCustomer.CountOfPersonal.Value : 0;

                var qServiceFee = await _context.ServiceFee.FirstOrDefaultAsync(p => p.IsActive == (byte)Common.Enums.TablesGeneralIsActive.Active && p.KindOfService == qRequest.KindOfRequest && (cOP >= p.FromCompanyRange && cOP <= p.ToCompanyRange));


                strContract = strContract.Replace("CompanyNameValue", qCustomer.CompanyName).Replace("KindOfCompanyValue", qKindOfCompany.Label).Replace("EconomicCodeValue", qCustomer.EconomicCode).Replace("NationalCodeValue", qCustomer.NationalCode);
                strContract = strContract.Replace("AddressCompanyValue", qCustomer.AddressCompany).Replace("PostalCodeValue", qCustomer.PostalCode).Replace("AgentNameValue", qCustomer.AgentName);
                strContract = strContract.Replace("EmailValue", qCustomer.Email).Replace("CompanyName2Value", qCustomer.CompanyName).Replace("KindOfCompany2Value", qKindOfCompany.Label).Replace("NamesAuthorizedSignatoriesValue", qCustomer.NamesAuthorizedSignatories);
                strContract = strContract.Replace("CompanyName3Value", qCustomer.CompanyName).Replace("NationalCode2Value", qCustomer.NationalCode).Replace("CompanyName4Value", qCustomer.CompanyName).Replace("NamesAuthorizedSignatories2Value", qCustomer.NamesAuthorizedSignatories);
             
                // مبلع فرمول
                strContract = strContract.Replace("ServiceFeePriceValue", "46000000");

                return new ResultGetServiceFeeAndCustomerByRequestDto()
                {
                    Customers = _mapper.Map<CustomersDto>(qCustomer),
                    ServiceFee = _mapper.Map<ServiceFeeDto>(qServiceFee != null ? qServiceFee : new ServiceFee()),
                    Contract=_mapper.Map<ContractDto>(qContract != null ? qContract : new Contract())
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
