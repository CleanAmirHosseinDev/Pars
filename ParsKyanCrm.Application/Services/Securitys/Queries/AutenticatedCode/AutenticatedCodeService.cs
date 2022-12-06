using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Application.Services.Securitys.Queries.Logins;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Common.PersianNumber;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Securitys.Queries.AutenticatedCode
{

    public class AutenticatedCodeService : IAutenticatedCodeService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IBaseSecurityFacad _baseSecurityFacad;
        public AutenticatedCodeService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IBaseSecurityFacad baseSecurityFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            _baseSecurityFacad = baseSecurityFacad;
        }

        public async Task<ResultDto<ResultLoginDto>> Execute(RequestAutenticatedCodeDto request)
        {
            try
            {
                var res_ResultLoginDto = new ResultLoginDto();
                string LoginName = "Customer";

                request.Code = PersianNumberHelper.PersianToEnglish(request.Code);

                res_ResultLoginDto.CustomerID = !string.IsNullOrEmpty(request.Bakdslkflkdsflkdslkfkldskfdslflsdkf_dnsfhsdkfh) ? request.Bakdslkflkdsflkdslkfkldskfdslflsdkf_dnsfhsdkfh.Decrypt_Advanced_For_Number() : null;

                var qCus = await _context.Customers.FirstOrDefaultAsync(p => p.AuthenticateCode == request.Code && p.CustomerId.ToString() == res_ResultLoginDto.CustomerID);

                var qUser = await _context.Users.FirstOrDefaultAsync(p => p.CustomerId == int.Parse(res_ResultLoginDto.CustomerID));

                res_ResultLoginDto.FullName = request.Fulllfsdfdsflsfldsfldslflsdlfdslflsdlfldsflldsf;
                res_ResultLoginDto.UserID = qUser.UserId;

                if (VaribleForName.IsDebug == true)
                {

                    if (request.Code == "1234") _baseSecurityFacad.AuthenticationJwtService.Execute(LoginName, res_ResultLoginDto, null, null);
                    else
                    {
                        return new ResultDto<ResultLoginDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            Message = "کد احراز شما یافت نشد",
                        };
                    }

                }
                else
                {

                    if (qCus != null) _baseSecurityFacad.AuthenticationJwtService.Execute(LoginName, res_ResultLoginDto, null, null);
                    else
                    {

                        return new ResultDto<ResultLoginDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            Message = "کد احراز شما یافت نشد",
                        };

                    }

                }



                return new ResultDto<ResultLoginDto>
                {
                    Data = res_ResultLoginDto,
                    IsSuccess = true,
                    Message = "/" + LoginName + "/Home/Index",
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
