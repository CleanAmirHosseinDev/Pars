using AutoMapper;
using ParsKyanCrm.Application.Services.Securitys.Base.Queries.AuthenticationJwt;
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
        private readonly IBaseSecurityFacad _baseSecurityFacad;
        public SecurityFacad(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IBaseSecurityFacad baseSecurityFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            _baseSecurityFacad = baseSecurityFacad;
        }

        private ILoginsService _loginsService;
        public ILoginsService LoginsService
        {
            get
            {
                return _loginsService = _loginsService ?? new LoginsService(_context,_mapper,_basicInfoFacad,_baseSecurityFacad);
            }
        }

    }

    public interface IBaseSecurityFacad
    {
        IAuthenticationJwtService AuthenticationJwtService { get; }
    }

    public class BaseSecurityFacad : IBaseSecurityFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public BaseSecurityFacad(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        private IAuthenticationJwtService _authenticationJwtService;
        public IAuthenticationJwtService AuthenticationJwtService
        {
            get
            {
                return _authenticationJwtService = _authenticationJwtService ?? new AuthenticationJwtService(_basicInfoFacad);
            }
        }

    }

}
