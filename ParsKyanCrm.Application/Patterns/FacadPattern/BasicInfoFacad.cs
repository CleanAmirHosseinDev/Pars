using AutoMapper;
using ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveCity;
using ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveState;
using ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveSystemSeting;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.FillUserRoleAdminRoles;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetCity;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetCitys;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetState;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetStates;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetSystemSeting;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetSystemSetings;
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

        IGetCitysService GetCitysService { get; }

        IGetCityService GetCityService { get; }

        ISaveCityService SaveCityService { get; }

        IGetSystemSetingsService GetSystemSetingsService { get; }

        IGetSystemSetingService GetSystemSetingService { get; }

        ISaveSystemSetingService SaveSystemSetingService { get; }

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

        private IGetCitysService _getCitysService;
        public IGetCitysService GetCitysService
        {
            get
            {
                return _getCitysService = _getCitysService ?? new GetCitysService(_context, _mapper);
            }
        }

        private IGetCityService _getCityService;
        public IGetCityService GetCityService
        {
            get
            {
                return _getCityService = _getCityService ?? new GetCityService(_context, _mapper);
            }
        }

        private ISaveCityService _saveCityService;
        public ISaveCityService SaveCityService
        {
            get
            {
                return _saveCityService = _saveCityService ?? new SaveCityService(_context, _mapper);
            }
        }

        private IGetSystemSetingsService _getSystemSetingsService;
        public IGetSystemSetingsService GetSystemSetingsService
        {
            get
            {
                return _getSystemSetingsService = _getSystemSetingsService ?? new GetSystemSetingsService(_context, _mapper);
            }
        }

        private IGetSystemSetingService _getSystemSetingService;
        public IGetSystemSetingService GetSystemSetingService
        {
            get
            {
                return _getSystemSetingService = _getSystemSetingService ?? new GetSystemSetingService(_context, _mapper);
            }
        }

        private ISaveSystemSetingService _saveSystemSetingService;
        public ISaveSystemSetingService SaveSystemSetingService
        {
            get
            {
                return _saveSystemSetingService = _saveSystemSetingService ?? new SaveSystemSetingService(_context, _mapper);
            }
        }

    }

}
