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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetRequestReferencess
{
    public class GetRequestReferencessService : IGetRequestReferencessService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetRequestReferencessService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ResultDto<IEnumerable<RequestReferencesDto>>> Execute(RequestRequestReferencesDto request)
        {
            try
            {

                var q = await DapperOperation.Run<RequestReferencesDto>(@$"


select cus.CompanyName,rr.Comment,rr.DestLevelStepIndex,rr.LevelStepAccessRole,rr.DestLevelStepIndexButton,
rr.LevelStepStatus,rr.ReferenceID,rr.Requestid,rr.SendTime,rr.SendUser,(select RoleDesc from UserRoles  as ur inner join Roles as r on ur.RoleID=r.RoleID where UserID=u.UserID)UserRoleDes,
cus.AgentName,u.RealName,u.UserName,rol.RoleDesc,ss.Label as KindOfRequestName,rfr.RequestNo
from {typeof(RequestReferences).Name} as rr
left join {typeof(RequestForRating).Name} as rfr on rfr.RequestID = rr.Requestid
left join {typeof(Customers).Name} as cus on cus.CustomerID = rfr.CustomerID
left join {typeof(Domain.Entities.Users).Name} as u on u.UserID = rr.SendUser
left join Roles as rol on rol.RoleID = cast(rr.LevelStepAccessRole as int)
left join {typeof(SystemSeting).Name} as ss on ss.SystemSetingID = rfr.KindOfRequest

{(request.Requestid.HasValue ? "where rr.Requestid = " + request.Requestid.Value : string.Empty)}

");

                return new ResultDto<IEnumerable<RequestReferencesDto>>
                {
                    Data = q,
                    IsSuccess = true,
                    Message = string.Empty,
                    Rows = q.LongCount(),
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
