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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetServiceFee
{

    public class GetServiceFeeService : IGetServiceFeeService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetServiceFeeService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ServiceFeeDto> Execute(RequestServiceFeeDto request)
        {
            try
            {
                ServiceFeeDto res = new ServiceFeeDto();
                res.KindOfServiceNavigation = new SystemSetingDto();

                if (request.ServiceFeeId != null && request.ServiceFeeId != 0)
                {
                    var q_Find = await _context.ServiceFee.Include(p => p.KindOfServiceNavigation).Include(p => p.KindOfServiceNavigation).FirstOrDefaultAsync(p => p.ServiceFeeId == request.ServiceFeeId && (p.IsActive == request.IsActive || request.IsActive == null));

                    res = _mapper.Map<ServiceFeeDto>(q_Find);                    
                    res.KindOfServiceNavigation = q_Find.KindOfServiceNavigation != null ? _mapper.Map<SystemSetingDto>(q_Find.KindOfServiceNavigation) : new SystemSetingDto();

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
