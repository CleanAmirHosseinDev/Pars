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
using ParsKyanCrm.Application.Services.Users.Queries.GetRequestReferencess;
using ParsKyanCrm.Application.Services.Users.Queries.GetDataFormQuestionss;
using ParsKyanCrm.Application.Services.Users.Commands.SaveDataFormQuestions;
using ParsKyanCrm.Application.Services.Users.Commands.DeleteDataFormQuestions;
using ParsKyanCrm.Application.Services.Users.Queries.GetDataFormQuestionsOption;
using ParsKyanCrm.Application.Services.Users.Commands.SaveDataFormQuestionsOption;
using ParsKyanCrm.Application.Services.Users.Commands.DeleteDataFormQuestionsOption;
using ParsKyanCrm.Application.Services.Users.Queries.GetDataFormAnswerTabless;
using ParsKyanCrm.Application.Services.Users.Queries.GetDataFromAnswerss;
using ParsKyanCrm.Application.Services.Users.Queries.GetDataForms;
using ParsKyanCrm.Application.Services.Users.Queries.GetDataForm;
using ParsKyanCrm.Application.Services.Users.Commands.SaveDataForm;
using ParsKyanCrm.Application.Services.Users.Commands.DeleteDataForm;
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
using ParsKyanCrm.Application.Services.Users.Commands.SaveCustomers_RegisterLanding;
using ParsKyanCrm.Application.Services.Users.Queries.GetStates;
using ParsKyanCrm.Application.Services.Users.Queries.FillUserRoleAdminRoles;
using ParsKyanCrm.Application.Services.Users.Queries.GetState;
using ParsKyanCrm.Application.Services.Users.Commands.SaveState;
using ParsKyanCrm.Application.Services.Users.Queries.GetCitys;
using ParsKyanCrm.Application.Services.Users.Queries.GetCity;
using ParsKyanCrm.Application.Services.Users.Commands.SaveCity;
using ParsKyanCrm.Application.Services.Users.Queries.GetSystemSetings;
using ParsKyanCrm.Application.Services.Users.Queries.GetSystemSeting;
using ParsKyanCrm.Application.Services.Users.Commands.SaveSystemSeting;
using ParsKyanCrm.Application.Services.Users.Queries.GetActivitys;
using ParsKyanCrm.Application.Services.Users.Queries.GetActivity;
using ParsKyanCrm.Application.Services.Users.Commands.SaveActivity;
using ParsKyanCrm.Application.Services.Users.Queries.GetAboutUs;
using ParsKyanCrm.Application.Services.Users.Commands.SaveAboutUs;
using ParsKyanCrm.Application.Services.Users.Queries.GetLicensesAndHonors;
using ParsKyanCrm.Application.Services.Users.Queries.GetLicensesAndHonorss;
using ParsKyanCrm.Application.Services.Users.Commands.SaveLicensesAndHonors;
using ParsKyanCrm.Application.Services.Users.Queries.GetManagerOfParsKyan;
using ParsKyanCrm.Application.Services.Users.Queries.GetManagerOfParsKyans;
using ParsKyanCrm.Application.Services.Users.Commands.SaveManagerOfParsKyan;
using ParsKyanCrm.Application.Services.Users.Queries.GetRankingOfCompaniess;
using ParsKyanCrm.Application.Services.Users.Queries.GetRankingOfCompanies;
using ParsKyanCrm.Application.Services.Users.Commands.SaveRankingOfCompanies;
using ParsKyanCrm.Application.Services.Users.Queries.GetNewsAndContents;
using ParsKyanCrm.Application.Services.Users.Queries.GetNewsAndContent;
using ParsKyanCrm.Application.Services.Users.Commands.SaveNewsAndContent;
using ParsKyanCrm.Application.Services.Users.Commands.DeleteNewsAndContent;
using ParsKyanCrm.Application.Services.Users.Commands.DeleteRankingOfCompanies;
using ParsKyanCrm.Application.Services.Users.Commands.DeleteActivity;
using ParsKyanCrm.Application.Services.Users.Commands.DeleteLicensesAndHonors;
using ParsKyanCrm.Application.Services.Users.Commands.DeleteManagerOfParsKyan;
using ParsKyanCrm.Application.Services.Users.Commands.InsertLoginLog;
using ParsKyanCrm.Application.Services.Users.Queries.GetLoginLogs;
using ParsKyanCrm.Application.Services.Users.Queries.GetRequestForRatings;
using ParsKyanCrm.Application.Services.Users.Commands.SaveAssessment;


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

        IGetDataFormQuestionsOptionService GetDataFormQuestionsOptionService { get; }

        ISaveDataFormQuestionsService SaveDataFormQuestionsService { get; }

        ISaveDataFormQuestionsOptionService SaveDataFormQuestionsOptionService { get; }
        
        IDeleteDataFormQuestionsService DeleteDataFormQuestionsService { get; }

        IDeleteDataFormQuestionsOptionService DeleteDataFormQuestionsOptionService { get; }
        IGetDataFormAnswerTablessService GetDataFormAnswerTablessService { get; }

        IGetDataFormAnswerTablesService GetDataFormAnswerTablesService { get; }

        IGetDataFormsService GetDataFormsService { get; }
        IGetDataFormService GetDataFormService { get; }
        ISaveDataFormService SaveDataFormService { get; }
        IDeleteDataFormService DeleteDataFormService { get; }

        IGetDataFromAnswerssService GetDataFromAnswerssService { get; }

        IGetCustomerssService GetCustomerssService { get; }

        ISaveCustomersService SaveCustomersService { get; }

        IDeleteCustomersService DeleteCustomersService { get; }

        IDeleteRequestForRatingService DeleteRequestForRatingService { get; }

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

        IGetContractPagessService GetContractPagessService { get; }

        IGetContractPagesService GetContractPagesService { get; }

        ISaveCustomers_RegisterLandingService SaveCustomers_RegisterLandingService { get; }

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

        IDeleteNewsAndContentService DeleteNewsAndContentService { get; }

        IDeleteRankingOfCompaniesService DeleteRankingOfCompaniesService { get; }

        IDeleteActivityService DeleteActivityService { get; }

        IDeleteLicensesAndHonorsService DeleteLicensesAndHonorsService { get; }

        IDeleteManagerOfParsKyanService DeleteManagerOfParsKyanService { get; }

        IInsertLoginLogService InsertLoginLogService { get; }

        IGetLoginLogsService GetLoginLogsService { get; }

        ISaveAssessmentService SaveAssessmentService { get; }

    }

    public class UserFacad : IUserFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        private readonly IValidator<RequestReferencesDto> _validatorRequestReferencesDto;

        public UserFacad(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env, IValidator<RequestReferencesDto> validatorRequestReferencesDto)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
            _validatorRequestReferencesDto = validatorRequestReferencesDto;
        }

        private IGetUserssService _getUserssService;
        public IGetUserssService GetUserssService
        {
            get
            {
                return _getUserssService = _getUserssService ?? new GetUserssService(_context, _mapper);
            }
        }

        private IGetContractAndFinancialDocumentsService _getContractAndFinancialDocumentsService;
        public IGetContractAndFinancialDocumentsService GetContractAndFinancialDocumentsService
        {
            get
            {
                return _getContractAndFinancialDocumentsService = _getContractAndFinancialDocumentsService ?? new GetContractAndFinancialDocumentsService(_context, _mapper);
            }
        }


        private IGetFurtherInfoService _getFurtherInfoService;
        public IGetFurtherInfoService GetFurtherInfoService
        {
            get
            {
                return _getFurtherInfoService = _getFurtherInfoService ?? new GetFurtherInfoService(_context, _mapper);
            }
        }

        private IGetPublicActivitiesService _getPublicActivitiesService;
        public IGetPublicActivitiesService GetPublicActivitiesService
        {
            get
            {
                return _getPublicActivitiesService = _getPublicActivitiesService ?? new GetPublicActivitiesService(_context, _mapper);
            }

        }

        private IGetCorporateGovernancesService _getCorporateGovernancesService;
        public IGetCorporateGovernancesService GetCorporateGovernancesService
        {
            get
            {
                return _getCorporateGovernancesService = _getCorporateGovernancesService ?? new GetCorporateGovernancesService(_context, _mapper);
            }
        }

        private IGetUsersService _getUsersService;
        public IGetUsersService GetUsersService
        {
            get
            {
                return _getUsersService = _getUsersService ?? new GetUsersService(_context, _mapper);
            }
        }

        private ISaveUsersService _saveUsersService;
        public ISaveUsersService SaveUsersService
        {
            get
            {
                return _saveUsersService = _saveUsersService ?? new SaveUsersService(_context, _mapper);
            }
        }

        private IGetRolessService _getRolessService;
        public IGetRolessService GetRolessService
        {
            get
            {
                return _getRolessService = _getRolessService ?? new GetRolessService(_context, _mapper);
            }
        }

        private IGetAccessLevelsService _getAccessLevelsService;
        public IGetAccessLevelsService GetAccessLevelsService
        {
            get
            {
                return _getAccessLevelsService = _getAccessLevelsService ?? new GetAccessLevelsService(_context, _mapper);
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
                return _saveBasicInformationCustomersService = _saveBasicInformationCustomersService ?? new SaveBasicInformationCustomersService(_context, _mapper, _env);
            }
        }

        private IGetCustomersService _getCustomersService;
        public IGetCustomersService GetCustomersService
        {
            get
            {
                return _getCustomersService = _getCustomersService ?? new GetCustomersService(_context, _mapper);
            }
        }

        private IGetRequestForRatingsService _getRequestForRatingsService;
        public IGetRequestForRatingsService GetRequestForRatingsService
        {
            get
            {
                return _getRequestForRatingsService = _getRequestForRatingsService ?? new GetRequestForRatingsService(_context, _mapper);
            }
        }

        private ISaveBoardOfDirectorsService _saveBoardOfDirectorsService;
        public ISaveBoardOfDirectorsService SaveBoardOfDirectorsService
        {
            get
            {
                return _saveBoardOfDirectorsService = _saveBoardOfDirectorsService ?? new SaveBoardOfDirectorsService(_context, _mapper, _env);
            }
        }

        private IGetBoardOfDirectorssService _getBoardOfDirectorssService;
        public IGetBoardOfDirectorssService GetBoardOfDirectorssService
        {
            get
            {
                return _getBoardOfDirectorssService = _getBoardOfDirectorssService ?? new GetBoardOfDirectorssService(_context, _mapper);
            }
        }

        private IGetBoardOfDirectorsService _getBoardOfDirectorsService;
        public IGetBoardOfDirectorsService GetBoardOfDirectorsService
        {
            get
            {
                return _getBoardOfDirectorsService = _getBoardOfDirectorsService ?? new GetBoardOfDirectorsService(_context, _mapper);
            }
        }

        private ISaveRequestForRatingService _saveRequestForRatingService;
        public ISaveRequestForRatingService SaveRequestForRatingService
        {
            get
            {
                return _saveRequestForRatingService = _saveRequestForRatingService ?? new SaveRequestForRatingService(_context, _mapper, _validatorRequestReferencesDto);
            }
        }

        private IGetLevelStepSettingService _getLevelStepSettingService;
        public IGetLevelStepSettingService GetLevelStepSettingService
        {
            get
            {
                return _getLevelStepSettingService = _getLevelStepSettingService ?? new GetLevelStepSettingService(_context, _mapper);
            }
        }

        private IGetServiceFeeService _getServiceFeeService;
        public IGetServiceFeeService GetServiceFeeService
        {
            get
            {
                return _getServiceFeeService = _getServiceFeeService ?? new GetServiceFeeService(_context, _mapper);
            }
        }

        private IGetServiceFeesService _getServiceFeesService;
        public IGetServiceFeesService GetServiceFeesService
        {
            get
            {
                return _getServiceFeesService = _getServiceFeesService ?? new GetServiceFeesService(_context, _mapper);
            }
        }

        private ISaveServiceFeeService _saveServiceFeeService;
        public ISaveServiceFeeService SaveServiceFeeService
        {
            get
            {
                return _saveServiceFeeService = _saveServiceFeeService ?? new SaveServiceFeeService(_context, _mapper);
            }
        }

        private IGetContractService _getContractService;
        public IGetContractService GetContractService
        {
            get
            {
                return _getContractService = _getContractService ?? new GetContractService(_context, _mapper);
            }
        }


        private IGetContractsService _getContractsService;
        public IGetContractsService GetContractsService
        {
            get
            {
                return _getContractsService = _getContractsService ?? new GetContractsService(_context, _mapper);
            }
        }

        private ISaveContractService _saveContractService;
        public ISaveContractService SaveContractService
        {
            get
            {
                return _saveContractService = _saveContractService ?? new SaveContractService(_context, _mapper);
            }
        }

        private IGetCompaniesService _getCompaniesService;
        public IGetCompaniesService GetCompaniesService
        {
            get
            {
                return _getCompaniesService = _getCompaniesService ?? new GetCompaniesService(_context, _mapper);
            }
        }

        private IGetCompaniessService _getCompaniessService;
        public IGetCompaniessService GetCompaniessService
        {
            get
            {
                return _getCompaniessService = _getCompaniessService ?? new GetCompaniessService(_context, _mapper);
            }
        }

        private ISaveCompaniesService _saveCompaniesService;
        public ISaveCompaniesService SaveCompaniesService
        {
            get
            {
                return _saveCompaniesService = _saveCompaniesService ?? new SaveCompaniesService(_context, _mapper);
            }
        }

        private IInitReferralService _initReferralService;
        public IInitReferralService InitReferralService
        {
            get
            {
                return _initReferralService = _initReferralService ?? new InitReferralService(_context, _mapper);
            }
        }

        private IGetRequestReferencessService _getRequestReferencessService;
        public IGetRequestReferencessService GetRequestReferencessService
        {
            get
            {
                return _getRequestReferencessService = _getRequestReferencessService ?? new GetRequestReferencessService(_context, _mapper);
            }
        }

        private IGetDataFormQuestionssService _getDataFormQuestionssService;
        public IGetDataFormQuestionssService GetDataFormQuestionssService
        {
            get
            {
                return _getDataFormQuestionssService = _getDataFormQuestionssService ?? new GetDataFormQuestionssService(_context, _mapper);
            }
        }
        private IGetDataFormQuestionsOptionService _getDataFormQuestionsOptionService;
        public IGetDataFormQuestionsOptionService GetDataFormQuestionsOptionService
        {
            get
            {
                return _getDataFormQuestionsOptionService = _getDataFormQuestionsOptionService ?? new GetDataFormQuestionsOptionService(_context, _mapper);
            }
        }
        private ISaveDataFormQuestionsOptionService _saveDataFormQuestionsOptionService;
        public ISaveDataFormQuestionsOptionService SaveDataFormQuestionsOptionService
        {
            get
            {
                return _saveDataFormQuestionsOptionService = _saveDataFormQuestionsOptionService ?? new SaveDataFormQuestionsOptionService(_context, _mapper, _env);
            }
        }

        private ISaveDataFormQuestionsService _saveDataFormQuestionsService;
        public ISaveDataFormQuestionsService SaveDataFormQuestionsService
        {
            get
            {
                return _saveDataFormQuestionsService = _saveDataFormQuestionsService ?? new SaveDataFormQuestionsService(_context, _mapper, _env);
            }
        }

        private IDeleteDataFormQuestionsOptionService _deleteDataFormQuestionsOptionService;
        public IDeleteDataFormQuestionsOptionService DeleteDataFormQuestionsOptionService
        {
            get
            {
                return _deleteDataFormQuestionsOptionService = _deleteDataFormQuestionsOptionService ?? new DeleteDataFormQuestionsOptionService(_context, _mapper, _env);
            }
        }
        private IDeleteDataFormQuestionsService _deleteDataFormQuestionsService;
        public IDeleteDataFormQuestionsService DeleteDataFormQuestionsService
        {
            get
            {
                return _deleteDataFormQuestionsService = _deleteDataFormQuestionsService ?? new DeleteDataFormQuestionsService(_context, _mapper, _env);
            }
        }
        private IGetDataFormAnswerTablesService _getDataFormAnswerTablesService;
        public IGetDataFormAnswerTablesService GetDataFormAnswerTablesService
        {
            get
            {
                return _getDataFormAnswerTablesService = _getDataFormAnswerTablesService ?? new GetDataFormAnswerTablesService(_context, _mapper);
            }
        }
        private IGetDataFormAnswerTablessService _getDataFormAnswerTablessService;
        public IGetDataFormAnswerTablessService GetDataFormAnswerTablessService
        {
            get
            {
                return _getDataFormAnswerTablessService = _getDataFormAnswerTablessService ?? new GetDataFormAnswerTablessService(_context, _mapper);
            }
        }
        private IGetDataFormsService _getDataFormsService;
        public IGetDataFormsService GetDataFormsService
        {
            get
            {
                return _getDataFormsService = _getDataFormsService ?? new GetDataFormsService(_context, _mapper);
            }
        }
        private IGetDataFormService _getDataFormService;
        public IGetDataFormService GetDataFormService
        {
            get
            {
                return _getDataFormService = _getDataFormService ?? new GetDataFormService(_context, _mapper);
            }
        }
        private ISaveDataFormService _saveDataFormService;
        public ISaveDataFormService SaveDataFormService
        {
            get
            {
                return _saveDataFormService = _saveDataFormService ?? new SaveDataFormService(_context, _mapper);
            }
        }
        private IDeleteDataFormService _deleteDataFormService;
        public IDeleteDataFormService DeleteDataFormService
        {
            get
            {
                return _deleteDataFormService = _deleteDataFormService ?? new DeleteDataFormService(_context, _mapper, _env);
            }
        }
        private IGetDataFromAnswerssService _getDataFromAnswerssService;
        public IGetDataFromAnswerssService GetDataFromAnswerssService
        {
            get
            {
                return _getDataFromAnswerssService = _getDataFromAnswerssService ?? new GetDataFromAnswerssService(_context, _mapper);
            }
        }

        private IGetServiceFeeAndCustomerByRequestService _getServiceFeeAndCustomerByRequestService;
        public IGetServiceFeeAndCustomerByRequestService GetServiceFeeAndCustomerByRequestService
        {
            get
            {
                return _getServiceFeeAndCustomerByRequestService = _getServiceFeeAndCustomerByRequestService ?? new GetServiceFeeAndCustomerByRequestService(_context, _mapper);
            }
        }

        private IGetCustomerssService _getCustomerssService;
        public IGetCustomerssService GetCustomerssService
        {
            get
            {
                return _getCustomerssService = _getCustomerssService ?? new GetCustomerssService(_context, _mapper);
            }
        }

        private ISaveCustomersService _saveCustomersService;
        public ISaveCustomersService SaveCustomersService
        {
            get
            {
                return _saveCustomersService = _saveCustomersService ?? new SaveCustomersService(_context, _mapper, _env);
            }
        }

        private IDeleteCustomersService _deleteCustomersService;
        public IDeleteCustomersService DeleteCustomersService
        {
            get
            {
                return _deleteCustomersService = _deleteCustomersService ?? new DeleteCustomersService(_context, _mapper, _env);
            }
        }


        private IDeleteRequestForRatingService _deleteRequestForRatingService;
        public IDeleteRequestForRatingService DeleteRequestForRatingService
        {
            get
            {
                return _deleteRequestForRatingService = _deleteRequestForRatingService ?? new DeleteRequestForRatingService(_context, _mapper, _env);
            }
        }


        private IDeleteCompaniesService _deleteCompaniesService;
        public IDeleteCompaniesService DeleteCompaniesService
        {
            get
            {
                return _deleteCompaniesService = _deleteCompaniesService ?? new DeleteCompaniesService(_context, _mapper, _env);
            }
        }

        private IDeleteServiceFeeService _deleteServiceFeeService;
        public IDeleteServiceFeeService DeleteServiceFeeService
        {
            get
            {
                return _deleteServiceFeeService = _deleteServiceFeeService ?? new DeleteServiceFeeService(_context, _mapper, _env);
            }
        }

        private IDeleteContractService _deleteContractService;
        public IDeleteContractService DeleteContractService
        {
            get
            {
                return _deleteContractService = _deleteContractService ?? new DeleteContractService(_context, _mapper, _env);
            }
        }
        private IDeleteContractAndFinancialDocumentsService _deleteContractAndFinancialDocumentsService;
        public IDeleteContractAndFinancialDocumentsService DeleteContractAndFinancialDocumentsService
        {
            get
            {
                return _deleteContractAndFinancialDocumentsService = _deleteContractAndFinancialDocumentsService ?? new DeleteContractAndFinancialDocumentsService(_context, _mapper, _env);
            }
        }
        private IDeleteUsersService _deleteUsersService;
        public IDeleteUsersService DeleteUsersService
        {
            get
            {
                return _deleteUsersService = _deleteUsersService ?? new DeleteUsersService(_context, _mapper, _env);
            }
        }

        private IUpdatePassUsersService _updatePassUsersService;
        public IUpdatePassUsersService UpdatePassUsersService
        {
            get
            {
                return _updatePassUsersService = _updatePassUsersService ?? new UpdatePassUsersService(_context, _mapper);
            }
        }

        private ISaveDataFormAnswerTablesService _saveDataFormAnswerTablesService;
        public ISaveDataFormAnswerTablesService SaveDataFormAnswerTablesService
        {
            get
            {
                return _saveDataFormAnswerTablesService = _saveDataFormAnswerTablesService ?? new SaveDataFormAnswerTablesService(_context, _mapper, _env);
            }
        }

        private ISaveDataFromAnswersService _saveDataFromAnswersService;
        public ISaveDataFromAnswersService SaveDataFromAnswersService
        {
            get
            {
                return _saveDataFromAnswersService = _saveDataFromAnswersService ?? new SaveDataFromAnswersService(_context, _mapper, _env);
            }
        }

        private IDeleteDataFormAnswerTablesService _deleteDataFormAnswerTablesService;
        public IDeleteDataFormAnswerTablesService DeleteDataFormAnswerTablesService
        {
            get
            {
                return _deleteDataFormAnswerTablesService = _deleteDataFormAnswerTablesService ?? new DeleteDataFormAnswerTablesService(_context, _mapper, _env);
            }
        }

        private ISaveContractAndFinancialDocumentsService _saveContractAndFinancialDocumentsService;
        public ISaveContractAndFinancialDocumentsService SaveContractAndFinancialDocumentsService
        {
            get
            {
                return _saveContractAndFinancialDocumentsService = _saveContractAndFinancialDocumentsService ?? new SaveContractAndFinancialDocumentsService(_context, _mapper, _env);
            }
        }

        private ISaveFurtherInfoService _saveFurtherInfoService;
        public ISaveFurtherInfoService SaveFurtherInfoService
        {
            get
            {
                return _saveFurtherInfoService = _saveFurtherInfoService ?? new SaveFurtherInfoService(_context, _mapper, _env);
            }
        }


        private ISaveCorporateGovernanceService _saveCorporateGovernanceService;
        public ISaveCorporateGovernanceService SaveCorporateGovernanceService
        {
            get
            {
                return _saveCorporateGovernanceService = _saveCorporateGovernanceService ?? new SaveCorporateGovernanceService(_context, _mapper, _env);
            }
        }

        private ISaveValueChainService _saveValueChainService;
        public ISaveValueChainService SaveValueChainService
        {
            get
            {
                return _saveValueChainService = _saveValueChainService ?? new SaveValueChainService(_context, _mapper, _env);
            }
        }

        private IGetValueChainService _getValueChainService;
        public IGetValueChainService GetValueChainService
        {
            get
            {
                return _getValueChainService = _getValueChainService ?? new GetValueChainService(_context, _mapper);
            }
        }

        private IGetLevelStepSettingsService _getLevelStepSettingsService;
        public IGetLevelStepSettingsService GetLevelStepSettingsService
        {
            get
            {
                return _getLevelStepSettingsService = _getLevelStepSettingsService ?? new GetLevelStepSettingsService(_context, _mapper);
            }
        }

        private IGetContractPagessService _getContractPagessService;
        public IGetContractPagessService GetContractPagessService
        {
            get
            {
                return _getContractPagessService = _getContractPagessService ?? new GetContractPagessService(_context, _mapper);
            }
        }


        private IGetContractPagesService _getContractPagesService;
        public IGetContractPagesService GetContractPagesService
        {
            get
            {
                return _getContractPagesService = _getContractPagesService ?? new GetContractPagesService(_context, _mapper);
            }
        }

        private ISavePublicActivitiesService _savePublicActivitiesService;
        public ISavePublicActivitiesService SavePublicActivitiesService
        {
            get
            {
                return _savePublicActivitiesService = _savePublicActivitiesService ?? new SavePublicActivitiesService(_context, _mapper, _env);
            }
        }

        private ISaveCustomers_RegisterLandingService _saveCustomers_RegisterLandingService;
        public ISaveCustomers_RegisterLandingService SaveCustomers_RegisterLandingService
        {
            get
            {
                return _saveCustomers_RegisterLandingService = _saveCustomers_RegisterLandingService ?? new SaveCustomers_RegisterLandingService(_context, _mapper, _env);
            }
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

        private IDeleteNewsAndContentService _deleteNewsAndContentService;
        public IDeleteNewsAndContentService DeleteNewsAndContentService
        {
            get
            {
                return _deleteNewsAndContentService = _deleteNewsAndContentService ?? new DeleteNewsAndContentService(_context, _mapper, _env);
            }
        }

        private IDeleteRankingOfCompaniesService _deleteRankingOfCompaniesService;
        public IDeleteRankingOfCompaniesService DeleteRankingOfCompaniesService
        {
            get
            {
                return _deleteRankingOfCompaniesService = _deleteRankingOfCompaniesService ?? new DeleteRankingOfCompaniesService(_context, _mapper, _env);
            }
        }

        private IDeleteActivityService _deleteActivityService;
        public IDeleteActivityService DeleteActivityService
        {
            get
            {
                return _deleteActivityService = _deleteActivityService ?? new DeleteActivityService(_context, _mapper, _env);
            }
        }

        private IDeleteLicensesAndHonorsService _deleteLicensesAndHonorsService;
        public IDeleteLicensesAndHonorsService DeleteLicensesAndHonorsService
        {
            get
            {
                return _deleteLicensesAndHonorsService = _deleteLicensesAndHonorsService ?? new DeleteLicensesAndHonorsService(_context, _mapper, _env);
            }
        }

        private IDeleteManagerOfParsKyanService _deleteManagerOfParsKyanService;
        public IDeleteManagerOfParsKyanService DeleteManagerOfParsKyanService
        {
            get
            {
                return _deleteManagerOfParsKyanService = _deleteManagerOfParsKyanService ?? new DeleteManagerOfParsKyanService(_context, _mapper, _env);
            }
        }

        private IInsertLoginLogService _insertLoginLogService;
        public IInsertLoginLogService InsertLoginLogService
        {
            get
            {
                return _insertLoginLogService = _insertLoginLogService ?? new InsertLoginLogService();
            }
        }

        private IGetLoginLogsService _getLoginLogsService;
        public IGetLoginLogsService GetLoginLogsService
        {
            get
            {
                return _getLoginLogsService = _getLoginLogsService ?? new GetLoginLogsService(_context, _mapper);
            }
        }


        private ISaveAssessmentService _saveAssessmentService;
        public ISaveAssessmentService SaveAssessmentService
        {
            get
            {
                return _saveAssessmentService = _saveAssessmentService ?? new SaveAssessmentService(_context);
            }
        }

    }



}
