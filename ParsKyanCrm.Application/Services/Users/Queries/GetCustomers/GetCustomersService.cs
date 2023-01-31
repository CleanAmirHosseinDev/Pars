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
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetCustomersService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<CustomersDto> Execute(RequestCustomersDto request)
        {
            try
            {
                CustomersDto res = new CustomersDto();
                res.HowGetKnowCompany = new Dtos.BasicInfo.SystemSetingDto();
                res.KindOfCompany = new Dtos.BasicInfo.SystemSetingDto();
                res.TypeServiceRequested = new Dtos.BasicInfo.SystemSetingDto();

                if (request.CustomerId != null && request.CustomerId != 0)
                {
                    var q_Find = await _context.Customers.Include(p => p.HowGetKnowCompany).Include(p => p.KindOfCompany).Include(p => p.TypeServiceRequested).FirstOrDefaultAsync(p => p.CustomerId == request.CustomerId && (p.IsActive == request.IsActive || request.IsActive == null));

                    res = _mapper.Map<CustomersDto>(q_Find);
                    res.HowGetKnowCompany = q_Find.HowGetKnowCompany != null ? _mapper.Map<Dtos.BasicInfo.SystemSetingDto>(q_Find.HowGetKnowCompany) : new Dtos.BasicInfo.SystemSetingDto();
                    res.KindOfCompany = q_Find.KindOfCompany != null ? _mapper.Map<Dtos.BasicInfo.SystemSetingDto>(q_Find.KindOfCompany) : new Dtos.BasicInfo.SystemSetingDto();
                    res.TypeServiceRequested = q_Find.TypeServiceRequested != null ? _mapper.Map<Dtos.BasicInfo.SystemSetingDto>(q_Find.TypeServiceRequested) : new Dtos.BasicInfo.SystemSetingDto();

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
