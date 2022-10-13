using AutoMapper;
using ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveState;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.FillUserRoleAdminRoles;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetState;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetStates;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Patterns.FacadPattern
{

    public interface IBasicInfoFacad
    {
        IFillUserRoleAdminRolesService FillUserRoleAdminRolesService { get; }
        IGetStatesService GetStatesService { get; }

        IGetStateService GetStateService { get; }

        ISaveStateService SaveStateService { get; }

    }

    public class BasicInfoFacad : IBasicInfoFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public BasicInfoFacad(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private IFillUserRoleAdminRolesService _fillUserRoleAdminRolesService;
        public IFillUserRoleAdminRolesService FillUserRoleAdminRolesService
        {
            get
            {
                return _fillUserRoleAdminRolesService = _fillUserRoleAdminRolesService ?? new FillUserRoleAdminRolesService();
            }
        }

        private IGetStatesService _getStatesService;
        public IGetStatesService GetStatesService
        {
            get
            {
                return _getStatesService = _getStatesService ?? new GetStatesService(_context, _mapper);
            }
        }

        private IGetStateService _getStateService;
        public IGetStateService GetStateService
        {
            get
            {
                return _getStateService = _getStateService ?? new GetStateService(_context, _mapper);
            }
        }

        private ISaveStateService _saveStateService;
        public ISaveStateService SaveStateService
        {
            get
            {
                return _saveStateService = _saveStateService ?? new SaveStateService(_context, _mapper);
            }
        }

    }

}
