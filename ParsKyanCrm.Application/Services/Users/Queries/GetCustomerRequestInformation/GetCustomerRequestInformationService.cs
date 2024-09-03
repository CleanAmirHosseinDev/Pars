using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetCustomerRequestInformation
{

    public class GetCustomerRequestInformationService : IGetCustomerRequestInformationService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetCustomerRequestInformationService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        public async Task<CustomerRequestInformationsDto> Execute(RequestCustomerRequestInformationDto request)
        {
            try
            {
                CustomerRequestInformationsDto res = new CustomerRequestInformationsDto();

                if (request.RequestId != null)
                {
                    var q_Find = await _context.CustomerRequestInformation.FirstOrDefaultAsync(s => s.RequestId == request.RequestId);
                    res = _mapper.Map<CustomerRequestInformationsDto>(q_Find);
                }
                else if (request.Id != null)
                {
                    var q_Find = await _context.CustomerRequestInformation.FirstOrDefaultAsync(s => s.Id == request.Id);
                    res = _mapper.Map<CustomerRequestInformationsDto>(q_Find);
                }
                else if (request.CustomerId != null)
                {
                    var q_Find = await _context.CustomerRequestInformation.FirstOrDefaultAsync(s => s.CustomerId == request.CustomerId);
                    res = _mapper.Map<CustomerRequestInformationsDto>(q_Find);
                }

                if (res != null)
                {
                    return res;
                }
                else
                {
                    return new CustomerRequestInformationsDto()
                    {
                        RequestId = request.RequestId,
                        CustomerId = request.CustomerId,
                        AmountOfLastSale = 0,
                        CountOfPersonel = 0,
                        IsActive = 15,
                        LastAuditingTaxList = "",
                        LastInsuranceList = "",
                        Id = 0
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
