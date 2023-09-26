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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetFurtherInfo
{

    public class GetFurtherInfoService : IGetFurtherInfoService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetFurtherInfoService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

      

        public async Task<ResultDto<FurtherInfoDto>> Execute(RequestFurtherInfoDto request)
        {
            try
            {
                FurtherInfoDto res = new FurtherInfoDto();                

                if (request.RequestId != null && request.RequestId != 0)
                {
                    var q_Find = await _context.FurtherInfo.FirstOrDefaultAsync(p => p.RequestId == request.RequestId);

                    res = _mapper.Map<FurtherInfoDto>(q_Find);

                    if (res == null)
                    {
                        return new ResultDto<FurtherInfoDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            Message = "اطلاعاتی پر نشده است",
                        };
                    }
                }

                return new ResultDto<FurtherInfoDto>
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
