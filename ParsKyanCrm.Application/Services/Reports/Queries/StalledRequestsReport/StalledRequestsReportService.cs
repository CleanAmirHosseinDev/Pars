using ParsKyanCrm.Domain.Contexts;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Common;
using System;

namespace ParsKyanCrm.Application.Services.Reports.Queries.StalledRequestsReport
{
    public class StalledRequestsReportService : IStalledRequestsReportService
    {
        private readonly IDataBaseContext _context;

        public StalledRequestsReportService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<StalledRequestsReportDto> Execute(RequestStalledRequestsReportDto request)
        {
            var query = _context.RequestForRating.AsQueryable();

            if (!string.IsNullOrEmpty(request.FromDateStr))
            {
                var fromDate = Convert.ToDateTime(request.FromDateStr);
                query = query.Where(p => p.DateOfRequest >= fromDate);
            }

            if (!string.IsNullOrEmpty(request.ToDateStr))
            {
                var toDate = Convert.ToDateTime(request.ToDateStr);
                query = query.Where(p => p.DateOfRequest <= toDate);
            }

            switch (request.Category)
            {
                case StalledRequestCategory.UnconfirmedContracts:
                    query = query.Where(p => p.DateOfConfirm == null && DbFunctions.DateDiffDay(p.DateOfRequest, DateTime.Now) > 14);
                    break;
                case StalledRequestCategory.IncompleteInfo:
                    // This logic will be more complex and might require joining with RequestReferences
                    // For now, I'll use a placeholder
                    query = query.Where(p => p.IsDeleted == true); // Placeholder
                    break;
                case StalledRequestCategory.StalledWithAssessor:
                    query = query.Join(_context.RequestReferences,
                                       rfr => rfr.Id,
                                       rr => rr.RequestForRatingId,
                                       (rfr, rr) => new { rfr, rr })
                                 .Where(x => x.rr.UserRole.Role.Name == "Assessor" && x.rr.ReciveDate != null && x.rr.SendDate == null && DbFunctions.DateDiffDay(x.rr.ReciveDate, DateTime.Now) > 7)
                                 .Select(x => x.rfr);
                    break;
                case StalledRequestCategory.StalledInEvaluationCommittee:
                    query = query.Join(_context.RequestReferences,
                                       rfr => rfr.Id,
                                       rr => rr.RequestForRatingId,
                                       (rfr, rr) => new { rfr, rr })
                                 .Where(x => x.rr.UserRole.Role.Name == "EvaluationCommittee" && x.rr.ReciveDate != null && x.rr.SendDate == null && DbFunctions.DateDiffDay(x.rr.ReciveDate, DateTime.Now) > 14)
                                 .Select(x => x.rfr);
                    break;
            }

            int rowsCount = await query.CountAsync();
            var data = await query.Skip(request.PageIndex * request.PageSize).Take(request.PageSize)
                .Select(p => new StalledRequestDto
                {
                    CompanyName = p.Company.Name,
                    RequestNo = p.RequestNo,
                    DateOfRequestStr = p.DateOfRequest.ToPersianDate(),
                    Status = "Stalled", // Placeholder
                    DelayInDays = 0 // Placeholder
                }).ToListAsync();

            return new StalledRequestsReportDto
            {
                Data = data,
                IsSuccess = true,
                Rows = rowsCount
            };
        }
    }
}
