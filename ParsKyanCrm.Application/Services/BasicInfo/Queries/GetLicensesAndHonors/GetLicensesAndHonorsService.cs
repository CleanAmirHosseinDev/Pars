using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetLicensesAndHonors
{

    public class GetLicensesAndHonorsService : IGetLicensesAndHonorsService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetLicensesAndHonorsService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LicensesAndHonorsDto> Execute(RequestLicensesAndHonorsDto request)
        {
            try
            {

                LicensesAndHonorsDto res = new LicensesAndHonorsDto();

                if (request.LicensesAndHonorsId != null && request.LicensesAndHonorsId != 0)
                {
                    var q_Find = await _context.LicensesAndHonors.Include(p => p.User).FirstOrDefaultAsync(p => p.LicensesAndHonorsId == request.LicensesAndHonorsId.Value && (p.IsActive == request.IsActive || request.IsActive == null));
                    res = _mapper.Map<LicensesAndHonorsDto>(q_Find);
                    res.User = q_Find.User != null ? _mapper.Map<UsersDto>(q_Find.User) : new UsersDto();
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
