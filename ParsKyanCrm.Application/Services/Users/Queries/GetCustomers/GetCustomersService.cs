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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetCustomers
{

    public class GetCustomersService : IGetCustomersService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetCustomersService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        public async Task<CustomersDto> Execute(RequestCustomersDto request)
        {
            try
            {
                CustomersDto res = new CustomersDto();
                res.HowGetKnowCompany = new Dtos.Users.SystemSetingDto();
                res.KindOfCompany = new Dtos.Users.SystemSetingDto();
                res.TypeServiceRequested = new Dtos.Users.SystemSetingDto();

                if (request.CustomerId != null && request.CustomerId != 0)
                {
                    var q_Find = await _context.Customers.Include(p => p.HowGetKnowCompany).Include(p => p.KindOfCompany).Include(p => p.TypeServiceRequested).FirstOrDefaultAsync(p => p.CustomerId == request.CustomerId && (p.IsActive == request.IsActive || request.IsActive == null));

                    res = _mapper.Map<CustomersDto>(q_Find);
                    if (q_Find.CityId != null)
                    {
                        var city = await _context.City.FirstOrDefaultAsync(c => c.CityId == q_Find.CityId);
                        if (city != null)
                        {
                            res.City = new CityDto
                            {
                                CityId = city.CityId,
                                CityName = city.CityName,
                                StateId = city.StateId
                            };
                        }
                    }
                    res.HowGetKnowCompany = q_Find.HowGetKnowCompany != null ? _mapper.Map<Dtos.Users.SystemSetingDto>(q_Find.HowGetKnowCompany) : new Dtos.Users.SystemSetingDto();
                    res.KindOfCompany = q_Find.KindOfCompany != null ? _mapper.Map<Dtos.Users.SystemSetingDto>(q_Find.KindOfCompany) : new Dtos.Users.SystemSetingDto();
                    res.TypeServiceRequested = q_Find.TypeServiceRequested != null ? _mapper.Map<Dtos.Users.SystemSetingDto>(q_Find.TypeServiceRequested) : new Dtos.Users.SystemSetingDto();

                    res.TypeGroupCompaniesName = q_Find.TypeGroupCompanies != null ? (await _context.SystemSeting.FindAsync(q_Find.TypeGroupCompanies.Value)).Label : string.Empty;
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
