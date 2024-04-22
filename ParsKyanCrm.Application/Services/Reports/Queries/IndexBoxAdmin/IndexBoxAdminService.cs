using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Reports.Queries.IndexBoxAdmin
{

    public class IndexBoxAdminService : IIndexBoxAdminService
    {

        public IndexBoxAdminService()
        {

        }


        public async Task<ResultIndexBoxAdminDto> Execute()
        {
            try
            {

                var data = (await DapperOperation.Run<ResultIndexBoxAdminDto>(@$"

                
                
declare @totalNumberCustomersApprovedContract as int = (select cast(count(*) as nvarchar(50)) as TotalNumberCustomersApprovedContract from Customers as cus
inner join RequestForRating as rfr on rfr.CustomerID = cus.CustomerID
where cus.IsActive = 15 and cus.IsProfileComplete = 1 and rfr.RequestID in (select ContractAndFinancialDocuments.RequestID from ContractAndFinancialDocuments where ContractAndFinancialDocuments.IsActive = 15 and ContractAndFinancialDocuments.ContractCode is not null))


declare @totalNumberCustomersWithoutRegistration as int = (select cast(count(*) as nvarchar(50)) as TotalNumberCustomersWithoutRegistration from Customers as cus
where cus.IsActive = 15 and cus.CustomerID not in(select CustomerID from RequestForRating));


declare @totalNumberApplicationsAssessmentMinistryPrivacy as int = (select cast(count(*) as nvarchar(50)) as TotalNumberApplicationsAssessmentMinistryPrivacy from Customers as cus
inner join RequestForRating as rfr on rfr.CustomerID = cus.CustomerID
where cus.IsActive = 15 and cus.IsProfileComplete = 1 and rfr.KindOfRequest = 66)

declare @numberCodedFiles as int = (
        select cast(count(*) as nvarchar(50)) as NumberCodedFiles from RequestForRating as rfr
        inner join Customers as cus on rfr.CustomerID = cus.CustomerID
        where cus.IsActive = 15 and cus.IsProfileComplete = 1 and rfr.IsFinished = 1
)

select @totalNumberCustomersApprovedContract as TotalNumberCustomersApprovedContract,
@totalNumberCustomersWithoutRegistration as TotalNumberCustomersWithoutRegistration,
@totalNumberApplicationsAssessmentMinistryPrivacy as TotalNumberApplicationsAssessmentMinistryPrivacy,
@numberCodedFiles as NumberCodedFiles

")).ToList().FirstOrDefault();

                return data != null ? data : new ResultIndexBoxAdminDto();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
