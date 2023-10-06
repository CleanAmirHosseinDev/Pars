using ParsKyanCrm.Application.Services.Reports.Queries.GeneralPerformanceReport;
using ParsKyanCrm.Application.Services.Reports.Queries.IndexBoxAdmin;
using ParsKyanCrm.Application.Services.Reports.Queries.TotalNumberCustomersApprovedContract;
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

    }
}
