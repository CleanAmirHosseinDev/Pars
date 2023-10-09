using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Reports.Queries.TotalNumberApplicationsAssessmentMinistryPrivacy
{
    public interface ITotalNumberApplicationsAssessmentMinistryPrivacyService
    {
        Task<ResultDto<IEnumerable<ResultTotalNumberApplicationsAssessmentMinistryPrivacyDto>>> Execute(RequestTotalNumberApplicationsAssessmentMinistryPrivacyDto request);

        Task<byte[]> Execute1(RequestTotalNumberApplicationsAssessmentMinistryPrivacyDto request);

    }
}
