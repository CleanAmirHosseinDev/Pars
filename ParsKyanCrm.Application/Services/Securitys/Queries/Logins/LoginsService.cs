using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Common.Enums;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Securitys.Queries.Logins
{
    public class LoginsService : ILoginsService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IBaseSecurityFacad _baseSecurityFacad;
        public LoginsService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IBaseSecurityFacad baseSecurityFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            _baseSecurityFacad = baseSecurityFacad;
        }

        //هنوز کامل نشده فقط مبین روی این قسمت کار کند
        public async Task<ResultDto<ResultLoginDto>> Execute(RequestLoginDto request)
        {
            try
            {
                string LoginName = string.Empty;

                var res_ResultLoginDto = new ResultLoginDto();

                UserRolesDto qCheckUserRole = null;
                Domain.Entities.Users user = null;

                if (string.IsNullOrEmpty(request.Mobile))
                {
                    //Admin And Supervisor
                    string strPassword = Infrastructure.EncryptDecrypt.Encrypt(request.Password);

                    user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.Username && x.Password == strPassword && x.Status == true);
                    if (user == null)
                        return new ResultDto<ResultLoginDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            Message = "نام کاربری یا کلمه عبور یافت نشد",
                        };
                    else
                    {

                        qCheckUserRole = await CheckUserRole(user.UserId);
                        if (qCheckUserRole != null)
                        {

                            LoginName = qCheckUserRole.Role.RoleTitle;

                            res_ResultLoginDto.FullName = !string.IsNullOrEmpty(user.UserName) ? user.UserName : string.Empty;
                            res_ResultLoginDto.UserID = user.UserId;
                            res_ResultLoginDto.CustomerID = null;


                        }
                        else
                            return new ResultDto<ResultLoginDto>
                            {
                                Data = null,
                                IsSuccess = false,
                                Message = "شما به پنل دسترسی ندارید",
                            };
                    }
                }
                else
                {
                    //Customer
                    LoginName = "Customer";
                    bool needSms = false;

                    var cusUser = await _context.Customers.FirstOrDefaultAsync(p => p.AgentMobile == request.Mobile);
                    if (cusUser != null)
                    {
                        if (cusUser.IsActive == (byte)Common.Enums.TablesGeneralIsActive.InActive)
                        {
                            return new ResultDto<ResultLoginDto>
                            {
                                Data = null,
                                IsSuccess = false,
                                Message = "اکانت شما توسط مدیران سامانه غیر فعال شده است لطفا برای استفاده مجدد از اکانت به قسمت پشتیبانی سامانه تماس حاصل فرمایید",
                            };
                        }

                        res_ResultLoginDto.FullName = !string.IsNullOrEmpty(cusUser.AgentName) ? cusUser.AgentName : "فاقد نام";
                        res_ResultLoginDto.UserID = 0;
                        res_ResultLoginDto.CustomerID = cusUser.CustomerId.ToString().Encrypt_Advanced_For_Number();

                        needSms = true;

                    }
                    else
                    {

                        var cusUserA = _context.Customers.Add(new Domain.Entities.Customers()
                        {
                            AgentMobile = request.Mobile,
                            IsActive = (byte)Common.Enums.TablesGeneralIsActive.Active,
                            SaveDate = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now)
                        });

                        await _context.SaveChangesAsync();

                        _context.UserRoles.Add(new UserRoles()
                        {
                            User = new Domain.Entities.Users()
                            {
                                CustomerId = cusUserA.Entity.CustomerId,
                                UserName = cusUserA.Entity.AgentMobile,
                                Ip = Common.Utility.GetUserHostAddress(),
                                IsActive = (byte)Common.Enums.TablesGeneralIsActive.Active,
                                Status = true,
                                Mobile = cusUserA.Entity.AgentMobile                                
                            },
                           RoleId = 10                           
                        }) ;
                        await _context.SaveChangesAsync();

                        res_ResultLoginDto.FullName = "فاقد نام";
                        res_ResultLoginDto.UserID = 0;
                        res_ResultLoginDto.CustomerID = cusUserA.Entity.CustomerId.ToString().Encrypt_Advanced_For_Number();

                        needSms = true;


                    }


                    //Send Sms
                    //Use Field needSms In Condition Under
                    if (true)
                    {

                    }

                }

                // authentication successful so generate jwt token           
                if (LoginName != "Customer") _baseSecurityFacad.AuthenticationJwtService.Execute(LoginName, res_ResultLoginDto, qCheckUserRole, user);

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

        private async Task<UserRolesDto> CheckUserRole(int UserID)
        {
            try
            {
                var qRole = await _context.UserRoles.FirstOrDefaultAsync(p => p.UserId == UserID);
                var qUserRole = await _context.Roles.FirstOrDefaultAsync(p => p.RoleId == qRole.RoleId);
                return _mapper.Map<UserRolesDto>(qRole);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
