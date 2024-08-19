using ParsKyanCrm.Application.Services.Reports.Queries.GeneralPerformanceReport;
using ParsKyanCrm.Application.Services.Reports.Queries.IndexBoxAdmin;
using ParsKyanCrm.Application.Services.Reports.Queries.NumberCodedFiles;
using ParsKyanCrm.Application.Services.Reports.Queries.PerformanceReportEvaluationStaffInDetail_ReportOne;
using ParsKyanCrm.Application.Services.Reports.Queries.PerformanceReportEvaluationStaffInDetail_ReportOne2;
using ParsKyanCrm.Application.Services.Reports.Queries.TotalNumberApplicationsAssessmentMinistryPrivacy;
using ParsKyanCrm.Application.Services.Reports.Queries.TotalNumberCorporateRequest;
using ParsKyanCrm.Application.Services.Reports.Queries.TotalNumberCustomersApprovedContract;
using ParsKyanCrm.Application.Services.Reports.Queries.TotalNumberCustomersWithoutRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Patterns.FacadPattern
{

    public interface IReportFacad
    {
        IGeneralPerformanceReportService GeneralPerformanceReportService { get; }

        IIndexBoxAdminService IndexBoxAdminService { get; }

        ITotalNumberCustomersApprovedContractService TotalNumberCustomersApprovedContractService { get; }

        ITotalNumberCustomersWithoutRegistrationService TotalNumberCustomersWithoutRegistrationService { get; }

        ITotalNumberApplicationsAssessmentMinistryPrivacyService TotalNumberApplicationsAssessmentMinistryPrivacyService { get; }
        ITotalNumberCorporateRequestService TotalNumberCorporateRequestService { get; }

        INumberCodedFilesService NumberCodedFilesService { get; }

        IPerformanceReportEvaluationStaffInDetail_ReportOneService PerformanceReportEvaluationStaffInDetail_ReportOneService { get; }

        IPerformanceReportEvaluationStaffInDetail_ReportOne2Service PerformanceReportEvaluationStaffInDetail_ReportOne2Service { get; }

    }

    public class ReportFacad : IReportFacad
    {

        private readonly IUserFacad _userFacad;

        public ReportFacad(IUserFacad userFacad)
        {
            _userFacad = userFacad;
        }

        private IGeneralPerformanceReportService _generalPerformanceReportService;
        public IGeneralPerformanceReportService GeneralPerformanceReportService
        {
            get
            {
                return _generalPerformanceReportService = _generalPerformanceReportService ?? new GeneralPerformanceReportService(_userFacad);
            }
        }

        private IIndexBoxAdminService _indexBoxAdminService;
        public IIndexBoxAdminService IndexBoxAdminService
        {
            get
            {
                return _indexBoxAdminService = _indexBoxAdminService ?? new IndexBoxAdminService();
            }
        }

        private ITotalNumberCustomersApprovedContractService _totalNumberCustomersApprovedContractService;
        public ITotalNumberCustomersApprovedContractService TotalNumberCustomersApprovedContractService
        {
            get
            {
                return _totalNumberCustomersApprovedContractService = _totalNumberCustomersApprovedContractService ?? new TotalNumberCustomersApprovedContractService();
            }
        }

        private ITotalNumberCustomersWithoutRegistrationService _totalNumberCustomersWithoutRegistrationService;
        public ITotalNumberCustomersWithoutRegistrationService TotalNumberCustomersWithoutRegistrationService
        {
            get
            {
                return _totalNumberCustomersWithoutRegistrationService = _totalNumberCustomersWithoutRegistrationService ?? new TotalNumberCustomersWithoutRegistrationService();
            }
        }

        private ITotalNumberApplicationsAssessmentMinistryPrivacyService _totalNumberApplicationsAssessmentMinistryPrivacyService;
        public ITotalNumberApplicationsAssessmentMinistryPrivacyService TotalNumberApplicationsAssessmentMinistryPrivacyService
        {
            get
            {
                return _totalNumberApplicationsAssessmentMinistryPrivacyService = _totalNumberApplicationsAssessmentMinistryPrivacyService ?? new TotalNumberApplicationsAssessmentMinistryPrivacyService();
            }
        }

        private ITotalNumberCorporateRequestService _totalNumberCorporateRequestService;
        public ITotalNumberCorporateRequestService TotalNumberCorporateRequestService
        {
            get
            {
                return _totalNumberCorporateRequestService = _totalNumberCorporateRequestService ?? new TotalNumberCorporateRequestService();
            }
        }

        private INumberCodedFilesService _numberCodedFilesService;
        public INumberCodedFilesService NumberCodedFilesService
        {
            get
            {
                return _numberCodedFilesService = _numberCodedFilesService ?? new NumberCodedFilesService();
            }
        }

        private IPerformanceReportEvaluationStaffInDetail_ReportOneService _performanceReportEvaluationStaffInDetail_ReportOneService;
        public IPerformanceReportEvaluationStaffInDetail_ReportOneService PerformanceReportEvaluationStaffInDetail_ReportOneService
        {
            get
            {
                return _performanceReportEvaluationStaffInDetail_ReportOneService = _performanceReportEvaluationStaffInDetail_ReportOneService ?? new PerformanceReportEvaluationStaffInDetail_ReportOneService();
            }
        }

        private IPerformanceReportEvaluationStaffInDetail_ReportOne2Service _performanceReportEvaluationStaffInDetail_ReportOne2Service;
        public IPerformanceReportEvaluationStaffInDetail_ReportOne2Service PerformanceReportEvaluationStaffInDetail_ReportOne2Service
        {
            get
            {
                return _performanceReportEvaluationStaffInDetail_ReportOne2Service = _performanceReportEvaluationStaffInDetail_ReportOne2Service ?? new PerformanceReportEvaluationStaffInDetail_ReportOne2Service();
            }
        }

    }
}
