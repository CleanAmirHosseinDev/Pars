using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveAboutUs;
using ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveActivity;
using ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveCity;
using ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveLicensesAndHonors;
using ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveManagerOfParsKyan;
using ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveNewsAndContent;
using ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveRankingOfCompanies;
using ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveState;
using ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveSystemSeting;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.FillUserRoleAdminRoles;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetAboutUs;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetActivity;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetActivitys;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetCity;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetCitys;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetLicensesAndHonors;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetLicensesAndHonorss;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetManagerOfParsKyan;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetManagerOfParsKyans;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetNewsAndContent;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetNewsAndContents;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetRankingOfCompanies;
using ParsKyanCrm.Application.Services.BasicInfo.Queries.GetRankingOfCompaniess;
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

        IGetActivitysService GetActivitysService { get; }

        IGetActivityService GetActivityService { get; }

        ISaveActivityService SaveActivityService { get; }

        IGetAboutUsService GetAboutUsService { get; }

        ISaveAboutUsService SaveAboutUsService { get; }

        IGetLicensesAndHonorsService GetLicensesAndHonorsService { get; }

        IGetLicensesAndHonorssService GetLicensesAndHonorssService { get; }

        ISaveLicensesAndHonorsService SaveLicensesAndHonorsService { get; }

        IGetManagerOfParsKyanService GetManagerOfParsKyanService { get; }

        IGetManagerOfParsKyansService GetManagerOfParsKyansService { get; }

        ISaveManagerOfParsKyanService SaveManagerOfParsKyanService { get; }

        IGetRankingOfCompaniessService GetRankingOfCompaniessService { get; }

        IGetRankingOfCompaniesService GetRankingOfCompaniesService { get; }

        ISaveRankingOfCompaniesService SaveRankingOfCompaniesService { get; }

        IGetNewsAndContentsService GetNewsAndContentsService { get; }

        IGetNewsAndContentService GetNewsAndContentService { get; }

        ISaveNewsAndContentService SaveNewsAndContentService { get; }

    }

    public class BasicInfoFacad : IBasicInfoFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public BasicInfoFacad(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
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

        private IGetActivitysService _getActivitysService;
        public IGetActivitysService GetActivitysService
        {
            get
            {
                return _getActivitysService = _getActivitysService ?? new GetActivitysService(_context, _mapper);
            }
        }

        private IGetActivityService _getActivityService;
        public IGetActivityService GetActivityService
        {
            get
            {
                return _getActivityService = _getActivityService ?? new GetActivityService(_context, _mapper);
            }
        }

        private ISaveActivityService _saveActivityService;
        public ISaveActivityService SaveActivityService
        {
            get
            {
                return _saveActivityService = _saveActivityService ?? new SaveActivityService(_context, _mapper, _env);
            }
        }

        private IGetAboutUsService _getAboutUsService;
        public IGetAboutUsService GetAboutUsService
        {
            get
            {
                return _getAboutUsService = _getAboutUsService ?? new GetAboutUsService(_context, _mapper);
            }
        }

        private ISaveAboutUsService _saveAboutUsService;
        public ISaveAboutUsService SaveAboutUsService
        {
            get
            {
                return _saveAboutUsService = _saveAboutUsService ?? new SaveAboutUsService(_context, _mapper);
            }
        }

        private IGetLicensesAndHonorsService _getLicensesAndHonorsService;
        public IGetLicensesAndHonorsService GetLicensesAndHonorsService
        {
            get
            {
                return _getLicensesAndHonorsService = _getLicensesAndHonorsService ?? new GetLicensesAndHonorsService(_context, _mapper);
            }
        }

        private IGetLicensesAndHonorssService _getLicensesAndHonorssService;
        public IGetLicensesAndHonorssService GetLicensesAndHonorssService
        {
            get
            {
                return _getLicensesAndHonorssService = _getLicensesAndHonorssService ?? new GetLicensesAndHonorssService(_context, _mapper);
            }
        }

        private ISaveLicensesAndHonorsService _saveLicensesAndHonorsService;
        public ISaveLicensesAndHonorsService SaveLicensesAndHonorsService
        {
            get
            {
                return _saveLicensesAndHonorsService = _saveLicensesAndHonorsService ?? new SaveLicensesAndHonorsService(_context, _mapper, _env);
            }
        }

        private IGetManagerOfParsKyanService _getManagerOfParsKyanService;
        public IGetManagerOfParsKyanService GetManagerOfParsKyanService
        {
            get
            {
                return _getManagerOfParsKyanService = _getManagerOfParsKyanService ?? new GetManagerOfParsKyanService(_context, _mapper);
            }
        }

        private IGetManagerOfParsKyansService _getManagerOfParsKyansService;
        public IGetManagerOfParsKyansService GetManagerOfParsKyansService
        {
            get
            {
                return _getManagerOfParsKyansService = _getManagerOfParsKyansService ?? new GetManagerOfParsKyansService(_context, _mapper);
            }
        }

        private ISaveManagerOfParsKyanService _saveManagerOfParsKyanService;
        public ISaveManagerOfParsKyanService SaveManagerOfParsKyanService
        {
            get
            {
                return _saveManagerOfParsKyanService = _saveManagerOfParsKyanService ?? new SaveManagerOfParsKyanService(_context, _mapper, _env);
            }
        }

        private IGetRankingOfCompaniessService _getRankingOfCompaniessService;
        public IGetRankingOfCompaniessService GetRankingOfCompaniessService
        {
            get
            {
                return _getRankingOfCompaniessService = _getRankingOfCompaniessService ?? new GetRankingOfCompaniessService(_context, _mapper);
            }
        }

        private IGetRankingOfCompaniesService _getRankingOfCompaniesService;
        public IGetRankingOfCompaniesService GetRankingOfCompaniesService
        {
            get
            {
                return _getRankingOfCompaniesService = _getRankingOfCompaniesService ?? new GetRankingOfCompaniesService(_context, _mapper);
            }
        }

        private ISaveRankingOfCompaniesService _saveRankingOfCompaniesService;
        public ISaveRankingOfCompaniesService SaveRankingOfCompaniesService
        {
            get
            {
                return _saveRankingOfCompaniesService = _saveRankingOfCompaniesService ?? new SaveRankingOfCompaniesService(_context, _mapper, _env);
            }
        }

        private IGetNewsAndContentsService _getNewsAndContentsService;
        public IGetNewsAndContentsService GetNewsAndContentsService
        {
            get
            {
                return _getNewsAndContentsService = _getNewsAndContentsService ?? new GetNewsAndContentsService(_context, _mapper);
            }
        }

        private IGetNewsAndContentService _getNewsAndContentService;
        public IGetNewsAndContentService GetNewsAndContentService
        {
            get
            {
                return _getNewsAndContentService = _getNewsAndContentService ?? new GetNewsAndContentService(_context, _mapper);
            }
        }

        private ISaveNewsAndContentService _saveNewsAndContentService;
        public ISaveNewsAndContentService SaveNewsAndContentService
        {
            get
            {
                return _saveNewsAndContentService = _saveNewsAndContentService ?? new SaveNewsAndContentService(_context, _mapper, _env);
            }
        }

    }

}
