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

                if (request.Id != null && request.CustomerId != 0 && request.RequestId != null)
                {
                    var q_Find = await _context.CustomerRequestInformation.FirstOrDefaultAsync(s => s.Id == request.Id);
                    res = _mapper.Map<CustomerRequestInformationsDto>(q_Find);
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
