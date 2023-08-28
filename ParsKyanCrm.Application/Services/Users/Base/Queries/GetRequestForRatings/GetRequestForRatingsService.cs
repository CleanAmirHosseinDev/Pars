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

namespace ParsKyanCrm.Application.Services.Users.Base.Queries.GetRequestForRatings
{
    public class GetRequestForRatingsService : IGetRequestForRatingsService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetRequestForRatingsService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ResultDto<IEnumerable<RequestForRatingDto>>> Execute(RequestRequestForRatingDto request)
        {
            try
            {
                var lists = (from s in _context.RequestForRating
                             select s);
                var data = await DapperOperation.Run<RequestForRatingDto>(@$"


select {"top " + request.PageSize} cte.RequestNo,cte.NationalCode,cte.AgentMobile,cte.AgentName,cte.CustomerID,cte.DateOfConfirmed,cte.DateOfRequest,cte.IsFinished,cte.KindOfRequest,cte.KindOfRequestName,cte.RequestID,cte.ContractDocument,(select  distinct top 1 LevelStepAccessRole from LevelStepSetting where LevelStepIndex=(dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',3) )) as DestLevelStepAccessRole,dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',1) as LevelStepStatus,dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',2) as LevelStepAccessRole,dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',3) as DestLevelStepIndex,cte.CompanyName,(select top 1 RequestReferences.Comment from RequestReferences where RequestReferences.Requestid = cte.RequestID order by RequestReferences.ReferenceID desc) as Comment,dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',5) as DestLevelStepIndexButton,dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',6) as ReciveUser,dbo.fn_GetAllNameUsers(dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',6)) as ReciveUserName,dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',7) as SendUser ,
dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',8) as LevelStepSettingIndexID from (

	select rfr.CustomerID,
                rfr.DateOfConfirmed,
                rfr.ChangeDate,
                rfr.DateOfRequest,
                rfr.IsFinished,
                rfr.KindOfRequest,
                rfr.RequestID,
                rfr.RequestNo,

                (select top 1 RequestReferences.LevelStepStatus+'|'+RequestReferences.LevelStepAccessRole+'|'+RequestReferences.DestLevelStepIndex+'|'+isnull(RequestReferences.Comment,N'')+'|'+isnull(RequestReferences.DestLevelStepIndexButton,N'')+'|'+isnull(RequestReferences.ReciveUser,'')+'|'+isnull(CAST(RequestReferences.SendUser AS nvarchar),'0')+'|'+isnull(CAST(RequestReferences.LevelStepSettingIndexID AS nvarchar),'0') from RequestReferences where RequestReferences.Requestid = rfr.RequestID order by RequestReferences.ReferenceID desc) as RequestReferences,
                 ss.Label as KindOfRequestName,
                 cus.AgentName,
                 cus.AgentMobile,
                 cus.CompanyName,
                 cus.NationalCode,
                 doc.ContractDocument
                 from {typeof(RequestForRating).Name} as rfr
                 left join {typeof(SystemSeting).Name} as ss on ss.SystemSetingID = rfr.KindOfRequest
                 left join {typeof(Customers).Name} as cus on cus.CustomerID = rfr.CustomerID
                 left join {typeof(ContractAndFinancialDocuments).Name}  as doc on doc.RequestID=rfr.RequestID
                 {(request.CustomerId.HasValue ? " where rfr.CustomerID = " + request.CustomerId.Value : string.Empty)}
                 {(request.RequestId.HasValue ? (request.CustomerId.HasValue ? " and" : " where") + " rfr.RequestID = " + request.RequestId.Value : string.Empty)}                                    
) as cte
{(request.DestLevelStepIndex.HasValue ? " where dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',3) = " + request.DestLevelStepIndex.Value : string.Empty)}
{(!string.IsNullOrEmpty(request.Search) ? (request.DestLevelStepIndex.HasValue ? " and " : " where ") + " cte.CompanyName like N'%" + request.Search + "%'" + "or cte.AgentMobile like N'%" + request.Search + "%'" : string.Empty)}
{(!string.IsNullOrEmpty(request.LoginName) && request.IsMyRequests ? (request.DestLevelStepIndex.HasValue || !string.IsNullOrEmpty(request.Search) ? " and " : " where ") + "  dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',2) = " + request.LoginName : string.Empty)}
{(request.IsMyRequests ? "and ((dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',6)=" + request.UserID + " or dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',6)=N''))" : "")}
{(request.KindOfRequest.HasValue ? (request.DestLevelStepIndex.HasValue  || !string.IsNullOrEmpty(request.Search) ? " and " : " where ") + "  cte.KindOfRequest = " + request.KindOfRequest.Value : string.Empty)}
order by cte.ChangeDate desc
");

                #region این کدها برای ReciveUser می باشد
                //var dataTemp = new List<RequestForRatingDto>();

                //if (request.UserID.HasValue)
                //{
                //    if (data.Where(p => p.LevelStepAccessRole == request.LoginName).Any(p => !string.IsNullOrEmpty(p.ReciveUser))) dataTemp = data.Where(p => p.ReciveUser == request.UserID.Value.ToString()).ToList();

                //    if(!string.IsNullOrEmpty(request.LoginName) && dataTemp.Count() == 0) data = data.Where(p => p.LevelStepAccessRole == request.LoginName);
                //}
                //else 
                //if(!string.IsNullOrEmpty(request.LoginName)) data = data.Where(p => p.LevelStepAccessRole == request.LoginName);

                //if (dataTemp.Count() > 0) data = dataTemp;     
                #endregion


                return new ResultDto<IEnumerable<RequestForRatingDto>>
                {
                    Data = data,
                    IsSuccess = true,
                    Message = string.Empty,
                    Rows = lists.LongCount(),
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
