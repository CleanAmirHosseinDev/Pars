using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

namespace ParsKyanCrm.Application.Services.Users.Queries.InitReferral
{

    public class InitReferralService : IInitReferralService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public InitReferralService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        private async Task<ResultDto<IEnumerable<RequestForRatingDto>>> GetRequestForRatings(RequestRequestForRatingDto request)
        {
            try
            {
                var lists = (from s in _context.RequestForRating
                             select s);

                request.PageSize = (request.IsExcelReport == true ? lists.ToList().Count() : request.PageSize);
                string cons = "";
                if (request.FromDate.HasValue && request.ToDate.HasValue)
                {
                    cons = " cte.DateOfRequest between N'" + request.FromDate.Value + "' and N'" + request.ToDate.Value + "'";
                }
                if (request.TypeGroupCompanies.HasValue)
                {
                    if (cons == "")
                    {
                        cons += "cte.TypeGroupCompanies=" + request.TypeGroupCompanies.Value;
                    }
                    else
                    {
                        cons += " and cte.TypeGroupCompanies=" + request.TypeGroupCompanies.Value;
                    }

                }
                if (request.ReciveUser != null)
                {
                    if (cons == "")
                    {
                        cons += "cte.RequestID in (select distinct Requestid from RequestReferences where ReciveUser = " + request.ReciveUser + ")";
                    }
                    else
                    {
                        cons += " and cte.RequestID in (select distinct Requestid from RequestReferences where ReciveUser = " + request.ReciveUser + ")";
                    }
                }

                var data = await DapperOperation.Run<RequestForRatingDto>(@$"



select {"top " + request.PageSize} cte.RequestNo,cte.EvaluationExpert,cte.NationalCode,cte.TypeGroupCompanies,cte.AgentMobile,cte.AgentName,cte.CustomerID,cte.DateOfConfirmed,cte.DateOfRequest,cte.IsFinished,cte.KindOfRequest,cte.KindOfRequestName,cte.RequestID,cte.ContractDocument,(select  distinct top 1 LevelStepAccessRole from LevelStepSetting where LevelStepIndex=(dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',3) )) as DestLevelStepAccessRole,dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',1) as LevelStepStatus,dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',2) as LevelStepAccessRole,dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',3) as DestLevelStepIndex,cte.CompanyName,(select top 1 RequestReferences.Comment from RequestReferences where RequestReferences.Requestid = cte.RequestID order by RequestReferences.ReferenceID desc) as Comment,dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',5) as DestLevelStepIndexButton,dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',6) as ReciveUser,dbo.fn_GetAllNameUsers(dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',6)) as ReciveUserName,dbo.fn_String_Split_with_Index(cte.RequestReferences,'|',7) as SendUser ,
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
{(request.KindOfRequest.HasValue ? (request.DestLevelStepIndex.HasValue || !string.IsNullOrEmpty(request.Search) ? " and " : " where ") + "  cte.KindOfRequest = " + request.KindOfRequest.Value : string.Empty)}
{(cons != "" ? (request.DestLevelStepIndex.HasValue || !string.IsNullOrEmpty(request.Search) ? " and " : " where ") + cons : string.Empty)}

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

        public async Task<ResultDto<IEnumerable<LevelStepSettingDto>>> Execute(string loginName, int? id = null)
        {
            try
            {

                var q = await GetRequestForRatings(new Dtos.Users.RequestRequestForRatingDto() { RequestId = id });

                if (!q.Data.Any())
                {

                    return new ResultDto<IEnumerable<LevelStepSettingDto>>
                    {
                        IsSuccess = false,
                        Message = string.Empty,
                        StatusCode = 404
                    };

                }

                if (q.Data.FirstOrDefault().DestLevelStepAccessRole != loginName)
                {

                    return new ResultDto<IEnumerable<LevelStepSettingDto>>
                    {
                        IsSuccess = false,
                        Message = string.Empty,
                    };

                }

                if (q.Rows == 1 && q.Data.FirstOrDefault().DestLevelStepIndex == "15")
                {

                    return new ResultDto<IEnumerable<LevelStepSettingDto>>
                    {
                        IsSuccess = false,
                        Message = "فرایند اتمام یافت",
                    };

                }

                var qLSI = await DapperOperation.Run<LevelStepSettingDto>(@$"select * from {typeof(LevelStepSetting).Name} where LevelStepIndex = " + q.Data.FirstOrDefault().DestLevelStepIndex + " and KindOfRequest =  " + q.Data.FirstOrDefault().KindOfRequest);
                foreach (var item in qLSI)
                {

                    if (!string.IsNullOrEmpty(item.DestLevelStepIndex))
                    {
                        var qqxcmcx = await DapperOperation.Run<LevelStepSettingDto>(@$"select * from {typeof(LevelStepSetting).Name} where LevelStepIndex = " + item.DestLevelStepIndex);
                        item.LevelStepAccessRole = qqxcmcx.FirstOrDefault().LevelStepAccessRole;
                        item.LevelStepStatus = qqxcmcx.FirstOrDefault().LevelStepStatus;
                        item.SendUser = q.Data.FirstOrDefault().SendUser;
                    }

                }

                return new ResultDto<IEnumerable<LevelStepSettingDto>>
                {
                    IsSuccess = true,
                    Message = string.Empty,
                    Data = qLSI
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
