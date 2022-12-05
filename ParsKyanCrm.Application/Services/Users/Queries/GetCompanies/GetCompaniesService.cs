using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetCompanies
{

    public class GetCompaniesService : IGetCompaniesService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetCompaniesService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<CompaniesDto> Execute(RequestCompaniesDto request)
        {
            try
            {
                CompaniesDto res = new CompaniesDto();
                res.CompanyGroup = new SystemSetingDto();
                res.KindOfCompanyNavigation = new SystemSetingDto();

                if (request.CompaniesId != null && request.CompaniesId != 0)
                {
                    var q_Find = await _context.Companies.Include(p => p.KindOfCompanyNavigation).Include(p => p.CompanyGroup).FirstOrDefaultAsync(p => p.CompaniesId == request.CompaniesId && (p.IsActive == request.IsActive || request.IsActive == null));

                    res = _mapper.Map<CompaniesDto>(q_Find);
                    res.KindOfCompanyNavigation = q_Find.KindOfCompanyNavigation != null ? _mapper.Map<SystemSetingDto>(q_Find.KindOfCompanyNavigation) : new SystemSetingDto();
                    res.CompanyGroup = q_Find.CompanyGroup != null ? _mapper.Map<SystemSetingDto>(q_Find.CompanyGroup) : new SystemSetingDto();

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
