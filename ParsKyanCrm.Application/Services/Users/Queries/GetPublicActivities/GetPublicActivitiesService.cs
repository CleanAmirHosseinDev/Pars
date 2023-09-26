using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Domain.Entities;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetPublicActivities
{

    public class GetPublicActivitiesService : IGetPublicActivitiesService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetPublicActivitiesService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

      

        public async Task<ResultDto<PublicActivitiesDto>> Execute(RequestPublicActivitiesDto request)
        {
            try
            {
                PublicActivitiesDto res = new PublicActivitiesDto();                

                if (request.RequestId != null && request.RequestId != 0)
                {
                    var q_Find = await _context.PublicActivities.FirstOrDefaultAsync(p => p.RequestId == request.RequestId);

                    res = _mapper.Map<PublicActivitiesDto>(q_Find);

                    if (res == null)
                    {
                        return new ResultDto<PublicActivitiesDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            Message = "اطلاعاتی پر نشده است",
                        };
                    }
                }

                return new ResultDto<PublicActivitiesDto>
                {
                    Data = res,
                    IsSuccess = true,
                    Message = string.Empty,                    
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
