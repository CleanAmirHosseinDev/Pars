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
using ParsKyanCrm.Application.Services.Users.Queries.GetDataFormQuestionss;
using ParsKyanCrm.Application.Services.Users.Queries.GetDataFormAnswerTabless;
using ParsKyanCrm.Application.Services.Users.Queries.GetDataFromAnswerss;
using ParsKyanCrm.Application.Services.Users.Queries.GetCustomerss;
using ParsKyanCrm.Application.Services.Users.Commands.SaveCustomers;
using ParsKyanCrm.Application.Services.Users.Commands.DeleteCustomers;
using ParsKyanCrm.Application.Services.Users.Commands.DeleteCompanies;
using ParsKyanCrm.Application.Services.Users.Commands.DeleteServiceFee;
using ParsKyanCrm.Application.Services.Users.Commands.DeleteContract;
using ParsKyanCrm.Application.Services.Users.Commands.DeleteUsers;
using ParsKyanCrm.Application.Services.Users.Commands.UpdatePassUsers;
using ParsKyanCrm.Application.Services.Users.Commands.SaveDataFormAnswerTables;
using ParsKyanCrm.Application.Services.Users.Commands.SaveDataFromAnswers;
using ParsKyanCrm.Application.Services.Users.Commands.DeleteDataFormAnswerTables;
using ParsKyanCrm.Application.Services.Users.Queries.GetDataFormAnswerTables;
using ParsKyanCrm.Application.Services.Users.Queries.GetServiceFeeAndCustomerByRequest;
using ParsKyanCrm.Application.Services.Users.Queries.GetContractAndFinancialDocuments;
using ParsKyanCrm.Application.Services.Users.Commands.SaveContractAndFinancialDocuments;
using ParsKyanCrm.Application.Services.Users.Commands.SaveFurtherInfo;
using ParsKyanCrm.Application.Services.Users.Queries.GetFurtherInfo;
using ParsKyanCrm.Application.Services.Users.Commands.SaveCorporateGovernances;
using ParsKyanCrm.Application.Services.Users.Commands.SaveValueChain;
using ParsKyanCrm.Application.Services.Users.Queries.GetCorporateGovernances;
using ParsKyanCrm.Application.Services.Users.Commands.SaveValueChainService;
using ParsKyanCrm.Application.Services.Users.Queries.GetValueChain;
using ParsKyanCrm.Application.Services.Users.Commands.SavePublicActivities;
using ParsKyanCrm.Application.Services.Users.Queries.GetPublicActivities;
using ParsKyanCrm.Application.Services.Users.Queries.GetLevelStepSettings;
using ParsKyanCrm.Application.Services.Users.Commands.DeleteContractAndFinancialDocumentsService;

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

        IGetDataFormQuestionssService GetDataFormQuestionssService { get; }

        IGetDataFormAnswerTablessService GetDataFormAnswerTablessService { get; }

        IGetDataFormAnswerTablesService GetDataFormAnswerTablesService { get; }

        IGetDataFromAnswerssService GetDataFromAnswerssService { get; }

        IGetCustomerssService GetCustomerssService { get; }

        ISaveCustomersService SaveCustomersService { get; }

        IDeleteCustomersService DeleteCustomersService { get; }

        IDeleteCompaniesService DeleteCompaniesService { get; }

        IDeleteServiceFeeService DeleteServiceFeeService { get; }

        IDeleteContractService DeleteContractService { get; }

        IDeleteContractAndFinancialDocumentsService DeleteContractAndFinancialDocumentsService { get; }

        IDeleteUsersService DeleteUsersService { get; }

        IUpdatePassUsersService UpdatePassUsersService { get; }

        ISaveDataFormAnswerTablesService SaveDataFormAnswerTablesService { get; }

        ISaveDataFromAnswersService SaveDataFromAnswersService { get; }

        IDeleteDataFormAnswerTablesService DeleteDataFormAnswerTablesService { get; }

        IGetServiceFeeAndCustomerByRequestService GetServiceFeeAndCustomerByRequestService { get; }

        IGetContractAndFinancialDocumentsService GetContractAndFinancialDocumentsService { get; }
      

        ISaveContractAndFinancialDocumentsService SaveContractAndFinancialDocumentsService { get; }
        ISaveFurtherInfoService SaveFurtherInfoService { get; }
        IGetFurtherInfoService GetFurtherInfoService { get; }

        ISaveCorporateGovernanceService SaveCorporateGovernanceService { get; }

        IGetCorporateGovernancesService GetCorporateGovernancesService { get; }

        ISaveValueChainService SaveValueChainService { get; }

        IGetValueChainService GetValueChainService { get; }

        ISavePublicActivitiesService SavePublicActivitiesService { get; }

        IGetPublicActivitiesService GetPublicActivitiesService { get; }

        IGetLevelStepSettingsService GetLevelStepSettingsService { get; }

    }

    public class UserFacad : IUserFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IWebHostEnvironment _env;        
        private readonly IBaseUserFacad _baseUserFacad;

        private readonly IValidator<RequestReferencesDto> _validatorRequestReferencesDto;

        public UserFacad(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IWebHostEnvironment env, IBaseUserFacad baseUserFacad, IValidator<RequestReferencesDto> validatorRequestReferencesDto)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            _env = env;            
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

        private IGetContractAndFinancialDocumentsService _getContractAndFinancialDocumentsService;
        public IGetContractAndFinancialDocumentsService GetContractAndFinancialDocumentsService
        {
            get
            {
                return _getContractAndFinancialDocumentsService = _getContractAndFinancialDocumentsService ?? new GetContractAndFinancialDocumentsService(_context, _mapper, _basicInfoFacad);
            }
        }


        private IGetFurtherInfoService _getFurtherInfoService;
        public IGetFurtherInfoService GetFurtherInfoService
        {
            get
            {
                return _getFurtherInfoService = _getFurtherInfoService ?? new GetFurtherInfoService(_context, _mapper, _basicInfoFacad);
            }
        }

        private IGetPublicActivitiesService _getPublicActivitiesService;
        public IGetPublicActivitiesService GetPublicActivitiesService
        {
            get
            {
                return _getPublicActivitiesService = _getPublicActivitiesService ?? new GetPublicActivitiesService(_context, _mapper, _basicInfoFacad);
            }

        }

        private IGetCorporateGovernancesService _getCorporateGovernancesService;
        public IGetCorporateGovernancesService GetCorporateGovernancesService
        {
            get
            {
                return _getCorporateGovernancesService = _getCorporateGovernancesService ?? new GetCorporateGovernancesService(_context, _mapper, _basicInfoFacad);
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
                return _saveBasicInformationCustomersService = _saveBasicInformationCustomersService ?? new SaveBasicInformationCustomersService(_context, _mapper, _basicInfoFacad,_env);
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

        private IGetDataFormQuestionssService _getDataFormQuestionssService;
        public IGetDataFormQuestionssService GetDataFormQuestionssService
        {
            get
            {
                return _getDataFormQuestionssService = _getDataFormQuestionssService ?? new GetDataFormQuestionssService(_context, _mapper, _basicInfoFacad);
            }
        }

        private IGetDataFormAnswerTablesService _getDataFormAnswerTablesService;
        public IGetDataFormAnswerTablesService GetDataFormAnswerTablesService
        {
            get
            {
                return _getDataFormAnswerTablesService = _getDataFormAnswerTablesService ?? new GetDataFormAnswerTablesService(_context, _mapper, _basicInfoFacad);
            }
        }
        private IGetDataFormAnswerTablessService _getDataFormAnswerTablessService;
        public IGetDataFormAnswerTablessService GetDataFormAnswerTablessService
        {
            get
            {
                return _getDataFormAnswerTablessService = _getDataFormAnswerTablessService ?? new GetDataFormAnswerTablessService(_context, _mapper, _basicInfoFacad);
            }
        }

        private IGetDataFromAnswerssService _getDataFromAnswerssService;
        public IGetDataFromAnswerssService GetDataFromAnswerssService
        {
            get
            {
                return _getDataFromAnswerssService = _getDataFromAnswerssService ?? new GetDataFromAnswerssService(_context, _mapper, _basicInfoFacad);
            }
        }

        private IGetServiceFeeAndCustomerByRequestService _getServiceFeeAndCustomerByRequestService;
        public IGetServiceFeeAndCustomerByRequestService GetServiceFeeAndCustomerByRequestService
        {
            get
            {
                return _getServiceFeeAndCustomerByRequestService = _getServiceFeeAndCustomerByRequestService ?? new GetServiceFeeAndCustomerByRequestService(_context, _mapper, _basicInfoFacad);
            }
        }

        private IGetCustomerssService _getCustomerssService;
        public IGetCustomerssService GetCustomerssService
        {
            get
            {
                return _getCustomerssService = _getCustomerssService ?? new GetCustomerssService(_context, _mapper, _basicInfoFacad);
            }
        }

        private ISaveCustomersService _saveCustomersService;
        public ISaveCustomersService SaveCustomersService
        {
            get
            {
                return _saveCustomersService = _saveCustomersService ?? new SaveCustomersService(_context, _mapper, _basicInfoFacad,_env);
            }
        }

        private IDeleteCustomersService _deleteCustomersService;
        public IDeleteCustomersService DeleteCustomersService
        {
            get
            {
                return _deleteCustomersService = _deleteCustomersService ?? new DeleteCustomersService(_context, _mapper, _basicInfoFacad, _env);
            }
        }

        private IDeleteCompaniesService _deleteCompaniesService;
        public IDeleteCompaniesService DeleteCompaniesService
        {
            get
            {
                return _deleteCompaniesService = _deleteCompaniesService ?? new DeleteCompaniesService(_context, _mapper, _basicInfoFacad, _env);
            }
        }

        private IDeleteServiceFeeService _deleteServiceFeeService;
        public IDeleteServiceFeeService DeleteServiceFeeService
        {
            get
            {
                return _deleteServiceFeeService = _deleteServiceFeeService ?? new DeleteServiceFeeService(_context, _mapper, _basicInfoFacad, _env);
            }
        }

        private IDeleteContractService _deleteContractService;
        public IDeleteContractService DeleteContractService
        {
            get
            {
                return _deleteContractService = _deleteContractService ?? new DeleteContractService(_context, _mapper, _basicInfoFacad, _env);
            }
        }
        private IDeleteContractAndFinancialDocumentsService _deleteContractAndFinancialDocumentsService;
        public IDeleteContractAndFinancialDocumentsService DeleteContractAndFinancialDocumentsService
        {
            get
            {
                return _deleteContractAndFinancialDocumentsService = _deleteContractAndFinancialDocumentsService ?? new DeleteContractAndFinancialDocumentsService(_context, _mapper, _basicInfoFacad, _env);
            }
        }
        private IDeleteUsersService _deleteUsersService;
        public IDeleteUsersService DeleteUsersService
        {
            get
            {
                return _deleteUsersService = _deleteUsersService ?? new DeleteUsersService(_context, _mapper, _basicInfoFacad, _env);
            }
        }

        private IUpdatePassUsersService _updatePassUsersService;
        public IUpdatePassUsersService UpdatePassUsersService
        {
            get
            {
                return _updatePassUsersService = _updatePassUsersService ?? new UpdatePassUsersService(_context, _mapper, _basicInfoFacad);
            }
        }

        private ISaveDataFormAnswerTablesService _saveDataFormAnswerTablesService;
        public ISaveDataFormAnswerTablesService SaveDataFormAnswerTablesService
        {
            get
            {
                return _saveDataFormAnswerTablesService = _saveDataFormAnswerTablesService ?? new SaveDataFormAnswerTablesService(_context, _mapper, _basicInfoFacad, _env);
            }
        }

        private ISaveDataFromAnswersService _saveDataFromAnswersService;
        public ISaveDataFromAnswersService SaveDataFromAnswersService
        {
            get
            {
                return _saveDataFromAnswersService = _saveDataFromAnswersService ?? new SaveDataFromAnswersService(_context, _mapper, _basicInfoFacad, _env);
            }
        }

        private IDeleteDataFormAnswerTablesService _deleteDataFormAnswerTablesService;
        public IDeleteDataFormAnswerTablesService DeleteDataFormAnswerTablesService
        {
            get
            {
                return _deleteDataFormAnswerTablesService = _deleteDataFormAnswerTablesService ?? new DeleteDataFormAnswerTablesService(_context, _mapper, _basicInfoFacad, _env);
            }
        }

        private ISaveContractAndFinancialDocumentsService _saveContractAndFinancialDocumentsService;
        public ISaveContractAndFinancialDocumentsService SaveContractAndFinancialDocumentsService
        {
            get
            {
                return _saveContractAndFinancialDocumentsService = _saveContractAndFinancialDocumentsService ?? new SaveContractAndFinancialDocumentsService(_context, _mapper, _basicInfoFacad,_env);
            }
        }

        private ISaveFurtherInfoService _saveFurtherInfoService;
        public ISaveFurtherInfoService SaveFurtherInfoService
        {
            get
            {
                return _saveFurtherInfoService = _saveFurtherInfoService ?? new SaveFurtherInfoService(_context, _mapper, _basicInfoFacad, _env);
            }
        }


        private ISaveCorporateGovernanceService _saveCorporateGovernanceService;
        public ISaveCorporateGovernanceService SaveCorporateGovernanceService
        {
            get
            {
                return _saveCorporateGovernanceService = _saveCorporateGovernanceService ?? new SaveCorporateGovernanceService(_context, _mapper, _basicInfoFacad, _env);
            }
        }

        private ISaveValueChainService _saveValueChainService;
        public ISaveValueChainService SaveValueChainService
        {
            get
            {
                return _saveValueChainService = _saveValueChainService ?? new SaveValueChainService(_context, _mapper, _basicInfoFacad, _env);
            }
        }

        private IGetValueChainService _getValueChainService;
        public IGetValueChainService GetValueChainService
        {
            get
            {
                return _getValueChainService=_getValueChainService??new GetValueChainService(_context, _mapper, _basicInfoFacad);
            }
        }

        private IGetLevelStepSettingsService _getLevelStepSettingsService;
        public IGetLevelStepSettingsService GetLevelStepSettingsService
        {
            get
            {
                return _getLevelStepSettingsService = _getLevelStepSettingsService ?? new GetLevelStepSettingsService(_context, _mapper, _basicInfoFacad);
            }
        }


        private ISavePublicActivitiesService _savePublicActivitiesService;
        public ISavePublicActivitiesService SavePublicActivitiesService
        {
            get
            {
                return _savePublicActivitiesService= _savePublicActivitiesService ?? new SavePublicActivitiesService(_context, _mapper, _basicInfoFacad);
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
