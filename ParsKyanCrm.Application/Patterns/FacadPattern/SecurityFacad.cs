using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using ParsKyanCrm.Application.Services.Securitys.Queries.AutenticatedCode;
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

        IAutenticatedCodeService AutenticatedCodeService { get; }
    }

    public class SecurityFacad : ISecurityFacad
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public SecurityFacad(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        private ILoginsService _loginsService;
        public ILoginsService LoginsService
        {
            get
            {
                return _loginsService = _loginsService ?? new LoginsService(_context, _mapper);
            }
        }

        private IAutenticatedCodeService _autenticatedCodeService;
        public IAutenticatedCodeService AutenticatedCodeService
        {
            get
            {
                return _autenticatedCodeService = _autenticatedCodeService ?? new AutenticatedCodeService(_context, _mapper);
            }
        }

    }
}
