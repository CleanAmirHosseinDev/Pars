using AutoMapper;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetRequestForRatings
{
    public class GetRequestForRatingsService : IGetRequestForRatingsService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetRequestForRatingsService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<IEnumerable<RequestForRatingDto>>> Execute(RequestRequestForRatingDto request)
        {
            try
            {
                //var lists = (from s in _context.RequestForRating
                //             select s);

                string cons = "";
                if (request.FromDate.HasValue && request.ToDate.HasValue)
                {
                    cons = " cteMain.DateOfRequest between N'" + request.FromDate.Value + "' and N'" + request.ToDate.Value + "'";
                }
                if (request.TypeGroupCompanies.HasValue)
                {
                    if (cons == "")
                    {
                        cons += "cteMain.TypeGroupCompanies=" + request.TypeGroupCompanies.Value;
                    }
                    else
                    {
                        cons += " and cteMain.TypeGroupCompanies=" + request.TypeGroupCompanies.Value;
                    }

                }
                if (request.ReciveUser != null)
                {
                    if (cons == "")
                    {
                        cons += "cteMain.RequestID in (select distinct Requestid from RequestReferences where ReciveUser = " + request.ReciveUser + ")";
                    }
                    else
                    {
                        cons += " and cteMain.RequestID in (select distinct Requestid from RequestReferences where ReciveUser = " + request.ReciveUser + ")";
                    }
                }

                var data = await DapperOperation.Run<RequestForRatingDto>(@$"

select {"top " + request.PageSize} * from (

        select cte.Assessment,cte.ReasonAssessment1,cte.ChangeDate,cte.RequestNo,cte.EvaluationExpert,cte.NationalCode,cte.TypeGroupCompanies,cte.AgentMobile,cte.AgentName,cte.CustomerID,cte.DateOfConfirmed,cte.DateOfRequest,cte.IsFinished,cte.KindOfRequest,cte.KindOfRequestName,cte.RequestID,cte.ContractDocument,(select  distinct top 1 LevelStepAccessRole from LevelStepSetting where LevelStepIndex=(dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',3) )) as DestLevelStepAccessRole,dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',1) as LevelStepStatus,dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',2) as LevelStepAccessRole,dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',3) as DestLevelStepIndex,cte.CompanyName,(select top 1 RequestReferences.Comment from RequestReferences where RequestReferences.Requestid = cte.RequestID order by RequestReferences.ReferenceID desc) as Comment,dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',5) as DestLevelStepIndexButton,dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',6) as ReciveUser,dbo.fn_GetAllNameUsers(dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',6)) as ReciveUserName,dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',7) as SendUser ,
dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',8) as LevelStepSettingIndexID from (

	select rfr.CustomerID,
                rfr.DateOfConfirmed,
                rfr.ChangeDate,
                rfr.DateOfRequest,
                rfr.IsFinished,
                rfr.KindOfRequest,
                rfr.RequestID,
                rfr.RequestNo,
                (select RealName from users where userid=(select top 1 ReciveUser from RequestReferences where ReciveUser  is not null and [DestLevelStepIndex]=6 and RequestID=rfr.RequestID)) EvaluationExpert,               
                (select top 1 RequestReferences.LevelStepStatus+'|'+RequestReferences.LevelStepAccessRole+'|'+RequestReferences.DestLevelStepIndex+'|'+isnull(RequestReferences.Comment,N'')+'|'+isnull(RequestReferences.DestLevelStepIndexButton,N'')+'|'+isnull(RequestReferences.ReciveUser,'')+'|'+isnull(CAST(RequestReferences.SendUser AS nvarchar),'0')+'|'+isnull(CAST(RequestReferences.LevelStepSettingIndexID AS nvarchar),'0') from RequestReferences where RequestReferences.Requestid = rfr.RequestID order by RequestReferences.ReferenceID desc) as RequestReferences,
                 ss.Label as KindOfRequestName,
                 cus.AgentName,
                 cus.AgentMobile,
                 cus.CompanyName,
                 cus.NationalCode,
                 cus.TypeGroupCompanies,
                 doc.ContractDocument,
                 rfr.Assessment,
                 rfr.ReasonAssessment1
                 from {typeof(RequestForRating).Name} as rfr
                 left join {typeof(SystemSeting).Name} as ss on ss.SystemSetingID = rfr.KindOfRequest
                 left join {typeof(Customers).Name} as cus on cus.CustomerID = rfr.CustomerID
                 left join {typeof(ContractAndFinancialDocuments).Name}  as doc on doc.RequestID=rfr.RequestID
                 {(request.CustomerId.HasValue ? " where rfr.CustomerID = " + request.CustomerId.Value : string.Empty)}
                 {(request.RequestId.HasValue ? (request.CustomerId.HasValue ? " and" : " where") + " rfr.RequestID = " + request.RequestId.Value : string.Empty)}                                    
) as cte

) as cteMain
{(request.DestLevelStepIndex.HasValue ? " where cteMain.DestLevelStepIndex = " + request.DestLevelStepIndex.Value : string.Empty)}
{(!string.IsNullOrEmpty(request.Search) ? (request.DestLevelStepIndex.HasValue ? " and " : " where ") + " cteMain.CompanyName like N'%" + request.Search + "%'" + "or cteMain.AgentMobile like N'%" + request.Search + "%'" : string.Empty)}
{(!string.IsNullOrEmpty(request.LoginName) && request.IsMyRequests ? (request.DestLevelStepIndex.HasValue || !string.IsNullOrEmpty(request.Search) ? " and " : " where ") + "  cteMain.LevelStepAccessRole = " + request.LoginName : string.Empty)}
{(request.IsMyRequests ? "and ((cteMain.ReciveUser =" + request.UserID + " or cteMain.ReciveUser =N''))" : "")}
{(request.KindOfRequest.HasValue ? (request.DestLevelStepIndex.HasValue || !string.IsNullOrEmpty(request.Search) ? " and " : " where ") + "  cteMain.KindOfRequest = " + request.KindOfRequest.Value : string.Empty)}
{(!string.IsNullOrEmpty(request.UserID) ? (request.DestLevelStepIndex.HasValue || !string.IsNullOrEmpty(request.Search) || request.KindOfRequest.HasValue ? " and " : " where ") + "  cteMain.ReciveUser like N'%" + request.UserID + "%' " : string.Empty)}
{(cons != "" ? (request.DestLevelStepIndex.HasValue || !string.IsNullOrEmpty(request.Search) || request.KindOfRequest.HasValue || !string.IsNullOrEmpty(request.UserID) ? " and " : " where ") + cons : string.Empty)}

order by cteMain.ChangeDate desc

");

                request.PageSize = (request.IsExcelReport == true ? data.Count() : request.PageSize);

                return new ResultDto<IEnumerable<RequestForRatingDto>>
                {
                    Data = data,
                    IsSuccess = true,
                    Message = string.Empty,
                    Rows = data.LongCount(),
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
