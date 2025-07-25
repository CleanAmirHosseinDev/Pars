using ParsKyanCrm.Domain.Contexts;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Common;
using System;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;

namespace ParsKyanCrm.Application.Services.Reports.Queries.StalledRequestsReport
{
    public class StalledRequestsReportService : IStalledRequestsReportService
    {
        private readonly IDataBaseContext _context;

        public StalledRequestsReportService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto<IEnumerable<StalledRequestDto>>> Execute(RequestStalledRequestsReportDto request)
        {
            if (request.Category == null)
                request.Category = StalledRequestCategory.UnconfirmedContracts;

            var query = _context.RequestForRating
                .Include(r => r.Customer)
                .AsQueryable();

            var category = request?.Category ?? StalledRequestCategory.UnconfirmedContracts;

            if (request != null)
            {
                switch (category)
                {
                    case StalledRequestCategory.UnconfirmedContracts:
                        query = query.Where(r =>
                            r.DateOfConfirmed == null &&
                            r.DateOfRequest.HasValue);
                        break;

                    case StalledRequestCategory.IncompleteInfo:
                        query = query.Where(r =>
                            r.IsFinished &&
                            !r.ChangeDate.HasValue);
                        break;

                    case StalledRequestCategory.StalledWithAssessor:
                        query = query.Where(r =>
                            _context.RequestReferences.Any(refItem =>
                                refItem.Requestid == r.RequestId &&
                                refItem.LevelStepAccessRole == "Assessor" &&
                                refItem.ReciveUser != null &&
                                refItem.SendTime == null));
                        break;

                    case StalledRequestCategory.StalledInEvaluationCommittee:
                        query = query.Where(r =>
                            _context.RequestReferences.Any(refItem =>
                                refItem.Requestid == r.RequestId &&
                                refItem.LevelStepAccessRole == "EvaluationCommittee" &&
                                refItem.ReciveUser != null &&
                                refItem.SendTime == null));
                        break;
                }
            }

            var allData = await query
                .OrderByDescending(x => x.DateOfRequest)
                .ToListAsync();

            var filtered = category switch
            {
                StalledRequestCategory.UnconfirmedContracts => allData
                    .Where(r => (DateTime.Now - r.DateOfRequest.Value).TotalDays > 14).ToList(),

                StalledRequestCategory.IncompleteInfo => allData
                    .Where(r => r.ChangeDate.HasValue &&
                                (DateTime.Now - r.ChangeDate.Value).TotalDays > 14).ToList(),

                StalledRequestCategory.StalledWithAssessor => allData
                    .Where(r => _context.RequestReferences.Any(refItem =>
                                refItem.Requestid == r.RequestId &&
                                refItem.LevelStepAccessRole == "Assessor" &&
                                refItem.ReciveUser != null &&
                                refItem.SendTime == null &&
                                (DateTime.Now - DateTime.Now).TotalDays > 7)) 
                    .ToList(),

                StalledRequestCategory.StalledInEvaluationCommittee => allData
                    .Where(r => _context.RequestReferences.Any(refItem =>
                                refItem.Requestid == r.RequestId &&
                                refItem.LevelStepAccessRole == "EvaluationCommittee" &&
                                refItem.ReciveUser != null &&
                                refItem.SendTime == null &&
                                (DateTime.Now - DateTime.Now).TotalDays > 14)) 
                    .ToList(),

                _ => allData
            };

            var pagedData = filtered
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .Select(r => new StalledRequestDto
                {
                    CompanyName = !string.IsNullOrEmpty(r.Customer?.CompanyName) ? r.Customer.CompanyName : "فاقد نام",
                    RequestNo = r.RequestNo,
                    DateOfRequestStr = r.DateOfRequest.HasValue
                        ? DateTimeOperation.ToPersianDate(r.DateOfRequest.Value)
                        : "تاریخ نامشخص",
                    Status = "Stalled",
                    DelayInDays = r.DateOfRequest.HasValue
                        ? (int)(DateTime.Now - r.DateOfRequest.Value).TotalDays
                        : 0
                }).ToList();

            return new ResultDto<IEnumerable<StalledRequestDto>>
            {
                Data = pagedData,
                IsSuccess = true,
                Message = $"تعداد {filtered.Count} رکورد یافت شد",
                Rows = filtered.Count
            };
        }
    }
}
