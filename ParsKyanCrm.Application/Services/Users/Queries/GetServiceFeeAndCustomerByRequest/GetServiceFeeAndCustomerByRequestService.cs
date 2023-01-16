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

                int cOP = qCustomer.CountOfPersonal.HasValue ? qCustomer.CountOfPersonal.Value : 0;

                var qServiceFee = await _context.ServiceFee.FirstOrDefaultAsync(p => p.IsActive == (byte)Common.Enums.TablesGeneralIsActive.Active && p.KindOfService == qRequest.KindOfRequest && (cOP >= p.FromCompanyRange && cOP <= p.ToCompanyRange));

                return new ResultGetServiceFeeAndCustomerByRequestDto()
                {
                    Customers = _mapper.Map<CustomersDto>(qCustomer),
                    ServiceFee = _mapper.Map<ServiceFeeDto>(qServiceFee != null ? qServiceFee : new ServiceFee())
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
