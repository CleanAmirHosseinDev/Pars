using AutoMapper;
using ParsKyanCrm.Application.Services.Securitys.Queries.Logins;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Patterns.FacadPattern
{
    public interface ISecurityFacad
    {
        ILoginsService LoginsService { get; }
    }

    public class SecurityFacad : ISecurityFacad
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;        
        public SecurityFacad(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;            
        }

        private ILoginsService _loginsService;
        public ILoginsService LoginsService
        {
            get
            {
                return _loginsService = _loginsService ?? new LoginsService(_context,_mapper,_basicInfoFacad);
            }
        }

    }

}
