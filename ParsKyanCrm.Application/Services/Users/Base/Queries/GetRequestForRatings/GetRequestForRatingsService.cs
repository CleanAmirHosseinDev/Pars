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
                var data = await DapperOperation.Run<RequestForRatingDto>(@$"


select cte.RequestNo,cte.AgentMobile,cte.AgentName,cte.CustomerID,cte.DateOfConfirmed,cte.DateOfRequest,cte.IsFinished,cte.KindOfRequest,cte.KindOfRequestName,cte.RequestID,(select distinct LevelStepAccessRole from LevelStepSetting where LevelStepIndex=(dbo.fn_String_Split_with_Index(cte.RequestReferences,'-',3) )) as DestLevelStepAccessRole,dbo.fn_String_Split_with_Index(cte.RequestReferences,'-',1) as LevelStepStatus,dbo.fn_String_Split_with_Index(cte.RequestReferences,'-',2) as LevelStepAccessRole,dbo.fn_String_Split_with_Index(cte.RequestReferences,'-',3) as DestLevelStepIndex from (

	select top {request.PageSize} rfr.CustomerID,
                rfr.DateOfConfirmed,
                rfr.DateOfRequest,
                rfr.IsFinished,
                rfr.KindOfRequest,
                rfr.RequestID,
                rfr.RequestNo,
                (select top 1 RequestReferences.LevelStepStatus+'-'+RequestReferences.LevelStepAccessRole+'-'+RequestReferences.DestLevelStepIndex from RequestReferences where RequestReferences.Requestid = rfr.RequestID order by RequestReferences.ReferenceID desc) as RequestReferences,
                 ss.Label as KindOfRequestName,
                 cus.AgentName,
                 cus.AgentMobile
                 from {typeof(RequestForRating).Name} as rfr
                 left join {typeof(SystemSeting).Name} as ss on ss.SystemSetingID = rfr.KindOfRequest
                 left join {typeof(Customers).Name} as cus on cus.CustomerID = rfr.CustomerID
                 {(request.CustomerId.HasValue ? " where rfr.CustomerID = " + request.CustomerId.Value : string.Empty)}
                 {(request.RequestId.HasValue ? (request.CustomerId.HasValue ? " and" : " where") + " rfr.RequestID = " + request.RequestId.Value : string.Empty)}
) as cte"
                 );

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
