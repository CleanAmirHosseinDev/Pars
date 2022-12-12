using AutoMapper;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using ParsKyanCrm.Application.Services.Users.Queries.GetUserss;
using ParsKyanCrm.Application.Services.Users.Queries.GetUsers;
using ParsKyanCrm.Application.Services.Users.Commands.SaveUsers;
using ParsKyanCrm.Application.Services.Users.Queries.GetRoless;
using ParsKyanCrm.Application.Services.Users.Queries.GetAccessLevels;
using ParsKyanCrm.Application.Services.Users.Commands.SaveAccessLevels;
using ParsKyanCrm.Application.Services.Users.Commands.SaveBasicInformationCustomers;
using ParsKyanCrm.Application.Services.Users.Queries.GetCustomers;
using FluentValidation;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Services.Users.Commands.SaveBoardOfDirectors;
using ParsKyanCrm.Application.Services.Users.Queries.GetBoardOfDirectorss;
using ParsKyanCrm.Application.Services.Users.Queries.GetBoardOfDirectors;
using ParsKyanCrm.Application.Services.Users.Commands.SaveRequestForRating;
using ParsKyanCrm.Application.Services.Users.Queries.GetLevelStepSetting;
using ParsKyanCrm.Application.Services.Users.Queries.GetServiceFee;
using ParsKyanCrm.Application.Services.Users.Queries.GetServiceFees;
using ParsKyanCrm.Application.Services.Users.Commands.SaveServiceFee;
using ParsKyanCrm.Application.Services.Users.Queries.GetContract;
using ParsKyanCrm.Application.Services.Users.Queries.GetContracts;
using ParsKyanCrm.Application.Services.Users.Commands.SaveContract;
using ParsKyanCrm.Application.Services.Users.Queries.GetCompanies;
using ParsKyanCrm.Application.Services.Users.Queries.GetCompaniess;
using ParsKyanCrm.Application.Services.Users.Commands.SaveCompanies;
using ParsKyanCrm.Application.Services.Users.Queries.InitReferral;
using ParsKyanCrm.Application.Services.Users.Base.Queries.GetRequestForRatings;
using ParsKyanCrm.Application.Services.Users.Queries.GetRequestReferencess;

namespace ParsKyanCrm.Application.Patterns.FacadPattern
{
    public interface IUserFacad
    {
        IGetUserssService GetUserssService { get; }

        IGetUsersService GetUsersService { get; }

        ISaveUsersService SaveUsersService { get; }

        IGetRolessService GetRolessService { get; }

        IGetAccessLevelsService GetAccessLevelsService { get; }

        ISaveAccessLevelsService SaveAccessLevelsService { get; }

        ISaveBasicInformationCustomersService SaveBasicInformationCustomersService { get; }

        IGetCustomersService GetCustomersService { get; }

        IGetRequestForRatingsService GetRequestForRatingsService { get; }

        ISaveBoardOfDirectorsService SaveBoardOfDirectorsService { get; }

        IGetBoardOfDirectorssService GetBoardOfDirectorssService { get; }

        IGetBoardOfDirectorsService GetBoardOfDirectorsService { get; }

        ISaveRequestForRatingService SaveRequestForRatingService { get; }

        IGetLevelStepSettingService GetLevelStepSettingService { get; }

        IGetServiceFeeService GetServiceFeeService { get; }

        IGetServiceFeesService GetServiceFeesService { get; }

        ISaveServiceFeeService SaveServiceFeeService { get; }

        IGetContractService GetContractService { get; }

        IGetContractsService GetContractsService { get; }

        ISaveContractService SaveContractService { get; }

        IGetCompaniesService GetCompaniesService { get; }

        IGetCompaniessService GetCompaniessService { get; }

        ISaveCompaniesService SaveCompaniesService { get; }

        IInitReferralService InitReferralService { get; }

        IGetRequestReferencessService GetRequestReferencessService { get; }

    }

    public class UserFacad : IUserFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IWebHostEnvironment _env;
        private readonly IValidator<CustomersDto> _validator;
        private readonly IBaseUserFacad _baseUserFacad;

        private readonly IValidator<RequestReferencesDto> _validatorRequestReferencesDto;

        public UserFacad(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IWebHostEnvironment env, IValidator<CustomersDto> validator, IBaseUserFacad baseUserFacad, IValidator<RequestReferencesDto> validatorRequestReferencesDto)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            _env = env;
            _validator = validator;
            _baseUserFacad = baseUserFacad;
            _validatorRequestReferencesDto = validatorRequestReferencesDto;
        }

        private IGetUserssService _getUserssService;
        public IGetUserssService GetUserssService
        {
            get
            {
                return _getUserssService = _getUserssService ?? new GetUserssService(_context, _mapper, _basicInfoFacad);
            }
        }

        private IGetUsersService _getUsersService;
        public IGetUsersService GetUsersService
        {
            get
            {
                return _getUsersService = _getUsersService ?? new GetUsersService(_context, _mapper, _basicInfoFacad);
            }
        }

        private ISaveUsersService _saveUsersService;
        public ISaveUsersService SaveUsersService
        {
            get
            {
                return _saveUsersService = _saveUsersService ?? new SaveUsersService(_context, _mapper, _basicInfoFacad);
            }
        }

        private IGetRolessService _getRolessService;
        public IGetRolessService GetRolessService
        {
            get
            {
                return _getRolessService = _getRolessService ?? new GetRolessService(_context, _mapper, _basicInfoFacad);
            }
        }

        private IGetAccessLevelsService _getAccessLevelsService;
        public IGetAccessLevelsService GetAccessLevelsService
        {
            get
            {
                return _getAccessLevelsService = _getAccessLevelsService ?? new GetAccessLevelsService(_context, _mapper, _basicInfoFacad);
            }
        }

        private ISaveAccessLevelsService _saveAccessLevelsService;
        public ISaveAccessLevelsService SaveAccessLevelsService
        {
            get
            {
                return _saveAccessLevelsService = _saveAccessLevelsService ?? new SaveAccessLevelsService();
            }
        }

        private ISaveBasicInformationCustomersService _saveBasicInformationCustomersService;
        public ISaveBasicInformationCustomersService SaveBasicInformationCustomersService
        {
            get
            {
                return _saveBasicInformationCustomersService = _saveBasicInformationCustomersService ?? new SaveBasicInformationCustomersService(_context, _mapper, _basicInfoFacad, _validator);
            }
        }

        private IGetCustomersService _getCustomersService;
        public IGetCustomersService GetCustomersService
        {
            get
            {
                return _getCustomersService = _getCustomersService ?? new GetCustomersService(_context, _mapper, _basicInfoFacad);
            }
        }
        
        public IGetRequestForRatingsService GetRequestForRatingsService
        {
            get
            {
                return _baseUserFacad.GetRequestForRatingsService;
            }
        }

        private ISaveBoardOfDirectorsService _saveBoardOfDirectorsService;
        public ISaveBoardOfDirectorsService SaveBoardOfDirectorsService
        {
            get
            {
                return _saveBoardOfDirectorsService = _saveBoardOfDirectorsService ?? new SaveBoardOfDirectorsService(_context, _mapper, _basicInfoFacad,_env);
            }
        }

        private IGetBoardOfDirectorssService _getBoardOfDirectorssService;
        public IGetBoardOfDirectorssService GetBoardOfDirectorssService
        {
            get
            {
                return _getBoardOfDirectorssService = _getBoardOfDirectorssService ?? new GetBoardOfDirectorssService(_context, _mapper, _basicInfoFacad);
            }
        }

        private IGetBoardOfDirectorsService _getBoardOfDirectorsService;
        public IGetBoardOfDirectorsService GetBoardOfDirectorsService
        {
            get
            {
                return _getBoardOfDirectorsService = _getBoardOfDirectorsService ?? new GetBoardOfDirectorsService(_context, _mapper, _basicInfoFacad);
            }
        }

        private ISaveRequestForRatingService _saveRequestForRatingService;
        public ISaveRequestForRatingService SaveRequestForRatingService
        {
            get
            {
                return _saveRequestForRatingService = _saveRequestForRatingService ?? new SaveRequestForRatingService(_context, _mapper, _basicInfoFacad, _validatorRequestReferencesDto);
            }
        }

        private IGetLevelStepSettingService _getLevelStepSettingService;
        public IGetLevelStepSettingService GetLevelStepSettingService
        {
            get
            {
                return _getLevelStepSettingService = _getLevelStepSettingService ?? new GetLevelStepSettingService(_context, _mapper, _basicInfoFacad);
            }
        }

        private IGetServiceFeeService _getServiceFeeService;
        public IGetServiceFeeService GetServiceFeeService
        {
            get
            {
                return _getServiceFeeService = _getServiceFeeService ?? new GetServiceFeeService(_context, _mapper, _basicInfoFacad);
            }
        }

        private IGetServiceFeesService _getServiceFeesService;
        public IGetServiceFeesService GetServiceFeesService
        {
            get
            {
                return _getServiceFeesService = _getServiceFeesService ?? new GetServiceFeesService(_context, _mapper, _basicInfoFacad);
            }
        }

        private ISaveServiceFeeService _saveServiceFeeService;
        public ISaveServiceFeeService SaveServiceFeeService
        {
            get
            {
                return _saveServiceFeeService = _saveServiceFeeService ?? new SaveServiceFeeService(_context, _mapper, _basicInfoFacad);
            }
        }

        private IGetContractService _getContractService;
        public IGetContractService GetContractService
        {
            get
            {
                return _getContractService = _getContractService ?? new GetContractService(_context, _mapper, _basicInfoFacad);
            }
        }


        private IGetContractsService _getContractsService;
        public IGetContractsService GetContractsService
        {
            get
            {
                return _getContractsService = _getContractsService ?? new GetContractsService(_context, _mapper, _basicInfoFacad);
            }
        }

        private ISaveContractService _saveContractService;
        public ISaveContractService SaveContractService
        {
            get
            {
                return _saveContractService = _saveContractService ?? new SaveContractService(_context, _mapper, _basicInfoFacad);
            }
        }

        private IGetCompaniesService _getCompaniesService;
        public IGetCompaniesService GetCompaniesService
        {
            get
            {
                return _getCompaniesService = _getCompaniesService ?? new GetCompaniesService(_context, _mapper, _basicInfoFacad);
            }
        }

        private IGetCompaniessService _getCompaniessService;
        public IGetCompaniessService GetCompaniessService
        {
            get
            {
                return _getCompaniessService = _getCompaniessService ?? new GetCompaniessService(_context, _mapper, _basicInfoFacad);
            }
        }

        private ISaveCompaniesService _saveCompaniesService;
        public ISaveCompaniesService SaveCompaniesService
        {
            get
            {
                return _saveCompaniesService = _saveCompaniesService ?? new SaveCompaniesService(_context, _mapper, _basicInfoFacad);
            }
        }

        private IInitReferralService _initReferralService;
        public IInitReferralService InitReferralService
        {
            get
            {
                return _initReferralService = _initReferralService ?? new InitReferralService(_context, _mapper, _basicInfoFacad, _baseUserFacad);
            }
        }

        private IGetRequestReferencessService _getRequestReferencessService;
        public IGetRequestReferencessService GetRequestReferencessService
        {
            get
            {
                return _getRequestReferencessService = _getRequestReferencessService ?? new GetRequestReferencessService(_context, _mapper, _basicInfoFacad);
            }
        }

    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public interface IBaseUserFacad
    {
        IGetRequestForRatingsService GetRequestForRatingsService { get; }
    }

    public class BaseUserFacad : IBaseUserFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IWebHostEnvironment _env;

        public BaseUserFacad(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            _env = env;
        }
        private IGetRequestForRatingsService _getRequestForRatingsService;
        public IGetRequestForRatingsService GetRequestForRatingsService
        {
            get
            {
                return _getRequestForRatingsService = _getRequestForRatingsService ?? new GetRequestForRatingsService(_context, _mapper, _basicInfoFacad);
            }
        }
    }

}
