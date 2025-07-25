﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Common.Enums;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Data;
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
        public LoginsService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        private List<NormalJsonClassDto> FillUserRoleAdminRolesService(string roles = null)
        {
            try
            {

                List<string> lstArr = new List<string>();

                if (!string.IsNullOrEmpty(roles)) lstArr.AddRange(roles.Split(','));

                UserRoleAdminRoles? qEnum = null;
                var q = EnumOperation<UserRoleAdminRoles>.ToSelectListByGroup(qEnum, lstArr).ToList();

                return q;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task InsertLoginLogService(LoginLogDto request, bool isLogin = true)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    dt.Columns.Add("Userid", typeof(int));
                    dt.Columns.Add("LoginDate", typeof(DateTime));
                    dt.Columns.Add("Ip", typeof(string));
                    dt.Columns.Add("SignOutDate", typeof(DateTime));
                    dt.Columns.Add("AreaName", typeof(string));
                    DataRow _ravi = dt.NewRow();

                    _ravi["Userid"] = request.Userid;
                    _ravi["Ip"] = await Infrastructure.Ipconfig.GetUserHostAddress();

                    if (isLogin)
                    {
                        _ravi["LoginDate"] = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);
                        _ravi["SignOutDate"] = DBNull.Value;
                    }
                    else
                    {
                        _ravi["LoginDate"] = DBNull.Value;
                        _ravi["SignOutDate"] = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);
                    }

                    _ravi["AreaName"] = request.AreaName;


                    dt.Rows.Add(_ravi);
                    Ado_NetOperation.SqlInsert("LoginLog", dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private async Task AuthenticationJwtService(string LoginName, ResultLoginDto res_ResultLoginDto, UserRolesDto qCheckUserRole, Domain.Entities.Users user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(VaribleForName.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Role,LoginName)
                    }),
                    Expires = LoginName == "Supervisor" ? DateTime.UtcNow.AddMinutes(680) : DateTime.UtcNow.AddMinutes(120),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                if (!string.IsNullOrEmpty(res_ResultLoginDto.CustomerID)) tokenDescriptor.Subject.AddClaim(new Claim("CustomerID", res_ResultLoginDto.CustomerID));
                if (res_ResultLoginDto.UserID != 0) tokenDescriptor.Subject.AddClaim(new Claim("UserID", res_ResultLoginDto.UserID.ToString()));

                if (qCheckUserRole != null)
                {
                    tokenDescriptor.Subject.AddClaim(new Claim("LoginName", qCheckUserRole.Role.RoleId.ToString()));
                    res_ResultLoginDto.LoginName = qCheckUserRole.Role.RoleId.ToString();
                }

                List<NormalJsonClassDto> obj_fillUserRoleCustomerRoles = null;

                switch (LoginName)
                {
                    case "Admin":

                        obj_fillUserRoleCustomerRoles =
                            user.UserName != "admin" ?
                            FillUserRoleAdminRolesService(qCheckUserRole.Roles).Where(p => p.Selected).ToList() :
                            FillUserRoleAdminRolesService();

                        //tokenDescriptor.Subject.AddClaim(new Claim("Menus", JsonConvert.SerializeObject(obj_fillUserRoleCustomerRoles)));

                        break;

                    case "Supervisor":



                        break;

                    default:

                        break;
                }

                var token = tokenHandler.CreateToken(tokenDescriptor);

                res_ResultLoginDto.Token = tokenHandler.WriteToken(token);

                if (obj_fillUserRoleCustomerRoles != null) res_ResultLoginDto.Menus = obj_fillUserRoleCustomerRoles;

                res_ResultLoginDto.RoleDesc = qCheckUserRole.Role.RoleDesc;

                await InsertLoginLogService(new Dtos.Users.LoginLogDto()
                {
                    AreaName = res_ResultLoginDto.RoleDesc,
                    Userid = !string.IsNullOrEmpty(res_ResultLoginDto.CustomerID) ? int.Parse(res_ResultLoginDto.CustomerID) : res_ResultLoginDto.UserID
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
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

                    user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.Username && x.Password == strPassword && x.Status == true && x.CustomerId == null);
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
                             
                            res_ResultLoginDto.FullName = !string.IsNullOrEmpty(user.RealName) ? user.RealName : string.Empty;
                            res_ResultLoginDto.UserID = user.UserId;
                            res_ResultLoginDto.CustomerID = null;
                            res_ResultLoginDto.Mobile = user.Mobile;
                          
                            #region تست کد جدید
                            if (string.IsNullOrEmpty(user.Mobile))
                            {

                                return new ResultDto<ResultLoginDto>
                                {
                                    Data = null,
                                    IsSuccess = false,
                                    Message = "برای کاربری شما، شماره موبایلی ثبت نشده است.",
                                };

                            }
                            if (user.IsActive == (byte)Common.Enums.TablesGeneralIsActive.InActive)
                            {
                                return new ResultDto<ResultLoginDto>
                                {
                                    Data = null,
                                    IsSuccess = false,
                                    Message = "اکانت شما توسط مدیران سامانه غیر فعال شده است لطفا برای استفاده مجدد از اکانت به قسمت پشتیبانی سامانه تماس حاصل فرمایید",
                                };
                            }
                              res_ResultLoginDto.UserIDStr = user.UserId.ToString().Encrypt_Advanced_For_Number();
                          //  res_ResultLoginDto.UserIDStr = user.UserId.ToString();
                              string r = RandomDjcode.randnu(5);

                            Ado_NetOperation.SqlUpdate("Users", new Dictionary<string, object>()
                        {
                            {
                                nameof(user.AuthenticateCode),r
                            }
                        }, nameof(user.UserId) + " = " + "'" + user.UserId + "'");

                            await WebService.SMSService.Execute(user.Mobile, string.Format("کاربر گرامی کد احراز شما :{0} می باشد . با تشکر سامانه پارس کیان", r));
                            LoginName = qCheckUserRole.Role.RoleTitle!="Admin" ? "SuperVisor":"Admin";

                            #endregion

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

                    if (request.Mobile.Substring(0, 2) != "09")
                    {

                        return new ResultDto<ResultLoginDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            Message = "شماره تلفن همراه را بدرستی وارد کنید",
                        };

                    }

                    if (string.IsNullOrEmpty(request.NationalCode))
                    {

                        return new ResultDto<ResultLoginDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            Message = "شناسه ملی شرکت را وارد کنید",
                        };

                    }

                    //Customer
                    LoginName = "Customer";
                    bool needSms = false;

                    string r = RandomDjcode.randnu(5);

                    if (request.nkekkfjdkjjkjkdjkdjkjkkj)
                    {
                        var aboutEntity = await _context.AboutUs.FirstOrDefaultAsync();
                        //  MailSender.SendMail("parscrc@outlook.com", "ارسال از سامانه", "درخواست تغییر شماره موبایل کاربری", "ادمین سامانه پارس کیان  لطفا نسبت به تغییر پروفایل اینجانب با کد ملی " + request.NationalCode + " با شماره جدید " + request.Mobile + " اقدام فرمایید با تشکر", "info@parscrc.ir");
                        await WebService.SMSService.Execute(aboutEntity.Mobile1, string.Format("شماره {0} با  شناسه {1} درخواست تغییر شماره موبایل دارد.", request.Mobile,request.NationalCode));
                        await WebService.SMSService.Execute(aboutEntity.Mobile2, string.Format("شماره {0} با  شناسه {1} درخواست تغییر شماره موبایل دارد.", request.Mobile, request.NationalCode));

                        return new ResultDto<ResultLoginDto>
                        {
                            Data = new ResultLoginDto()
                            {
                                iNSt2 = true
                            },
                            IsSuccess = true,
                            Message = "درخواست تغییر شماره موبایل شما جهت دسترسی به پروفایل به ادمین سامانه ارسال گردید لطفا منتظر تماس باشید.",
                        };

                    }

                    var cusUser = await _context.Customers.Where(p => p.AgentMobile == request.Mobile).ToListAsync();
                    if (cusUser != null && cusUser.Count() > 0)
                    {

                        var objSingleCus = cusUser.FirstOrDefault(p => p.NationalCode == request.NationalCode);



                        if (objSingleCus != null)
                        {

                            if (objSingleCus.IsActive == (byte)Common.Enums.TablesGeneralIsActive.InActive)
                            {
                                return new ResultDto<ResultLoginDto>
                                {
                                    Data = null,
                                    IsSuccess = false,
                                    Message = "اکانت شما توسط مدیران سامانه غیر فعال شده است لطفا برای استفاده مجدد از اکانت به قسمت پشتیبانی سامانه تماس حاصل فرمایید",
                                };
                            }

                            var customerUser = await _context.Users.FirstOrDefaultAsync(p => p.CustomerId == objSingleCus.CustomerId);
                          
                            res_ResultLoginDto.FullName = !string.IsNullOrEmpty(objSingleCus.CompanyName) ? objSingleCus.CompanyName : "فاقد نام";
                            res_ResultLoginDto.UserID = customerUser.UserId;
                            res_ResultLoginDto.CustomerID = objSingleCus.CustomerId.ToString().Encrypt_Advanced_For_Number();

                            Ado_NetOperation.SqlUpdate(nameof(Customers), new Dictionary<string, object>()
                          {
                            {
                                nameof(objSingleCus.AuthenticateCode),r
                            }
                           }, nameof(objSingleCus.CustomerId) + " = " + "'" + objSingleCus.CustomerId + "'");

                            needSms = true;

                        }
                        else
                        {
                            if (!request.aslkewkdkmscedkwlssdjcm)
                            {

                                return new ResultDto<ResultLoginDto>
                                {
                                    Data = new ResultLoginDto()
                                    {
                                        aslkewkdkmscedkwlssdjcm = true
                                    },
                                    IsSuccess = false,
                                    Message = "کاربر محترم شما قبلا در سامانه ثبت نام کرده اید آیا مایل هستید برای این شناسه ملی یک کاربری جدید ایجاد شود؟",

                                };

                            }
                            else
                            {

                                var objMessage = await SaveCustomer(request, res_ResultLoginDto, r);
                                if (objMessage != null)
                                    return objMessage;

                                needSms = true;

                            }
                        }


                    }
                    else
                    {

                        var ObjMessage = await SaveCustomer(request, res_ResultLoginDto, r);
                        if (ObjMessage != null) return ObjMessage;

                        needSms = true;
                    }


                    //Send Sms
                    //Use Field needSms In Condition Under
                    if (needSms)
                    {
                       
                        
                        await WebService.SMSService.Execute(request.Mobile, string.Format("کاربر گرامی کد احراز شما :{0} می باشد . با تشکر سامانه پارس کیان", r));
                        
                    }

                }

                // authentication successful so generate jwt token           
               // if (LoginName != "Customer") AuthenticationJwtService(LoginName, res_ResultLoginDto, qCheckUserRole, user);

                return new ResultDto<ResultLoginDto>
                {
                    Data = res_ResultLoginDto,
                    IsSuccess = true,
                    Message = "/" + (LoginName != "Customer" && LoginName != "Admin" ? "SuperVisor" : LoginName) + "/Home/Index",
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

        private async Task<ResultDto<ResultLoginDto>> SaveCustomer(RequestLoginDto request, ResultLoginDto res_ResultLoginDto, string r)
        {

            try
            {

                var qCheckNatinalCode = await _context.Customers.FirstOrDefaultAsync(p => p.NationalCode == request.NationalCode);
                if (qCheckNatinalCode != null)
                    return new ResultDto<ResultLoginDto>
                    {
                        Data = new ResultLoginDto()
                        {
                            nkekkfjdkjjkjkdjkdjkjkkj = true
                        },
                        IsSuccess = false,
                        Message = "شناسه ملی شرکت از قبل موجود می باشد لطفا شناسه ملی شرکت دیگری وارد کنید",
                    };

                var cusUserA = _context.Customers.Add(new Domain.Entities.Customers()
                {
                    AgentMobile = request.Mobile,
                    IsActive = (byte)Common.Enums.TablesGeneralIsActive.Active,
                    SaveDate = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now),
                    AuthenticateCode = r,
                    NationalCode = request.NationalCode,
                    CustomerPersonalityType = request.RadioSelectSha == "0" ? 224 : 223
                });

                await _context.SaveChangesAsync();

                var userC = _context.UserRoles.Add(new UserRoles()
                {
                    User = new Domain.Entities.Users()
                    {
                        CustomerId = cusUserA.Entity.CustomerId,
                        UserName = cusUserA.Entity.AgentMobile,
                        Ip = await Infrastructure.Ipconfig.GetUserHostAddress(),
                        IsActive = (byte)Common.Enums.TablesGeneralIsActive.Active,
                        Status = true,
                        Mobile = cusUserA.Entity.AgentMobile
                    },
                    RoleId = 10
                });
                await _context.SaveChangesAsync();

                res_ResultLoginDto.FullName = "فاقد نام";
                res_ResultLoginDto.UserID = userC.Entity.UserId;
                res_ResultLoginDto.CustomerID = cusUserA.Entity.CustomerId.ToString().Encrypt_Advanced_For_Number();

                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
