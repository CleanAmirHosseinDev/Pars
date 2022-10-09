using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
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
        public LoginsService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        //هنوز کامل نشده فقط مبین روی این قسمت کار کند
        public async Task<ResultDto<ResultLoginDto>> Execute(RequestLoginDto request)
        {
            try
            {

                var res_ResultLoginDto = new ResultLoginDto();

                string strPassword = Infrastructure.EncryptDecrypt.Encrypt(request.Password);
                UserRolesDto qCheckUserRole = null;
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.Username && x.Password == strPassword && x.Status == true);
                if (user == null)
                    return new ResultDto<ResultLoginDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "نام کاربری یا کلمه عبور یافت نشد",
                    };
                else
                {

                    qCheckUserRole = await CheckUserRole(request.LoginName, user.UserID);
                    if (qCheckUserRole != null)
                    {

                        if (request.LoginName != "Customer")
                        {
                            res_ResultLoginDto.FullName = !string.IsNullOrEmpty(user.UserName) ? user.UserName : string.Empty;
                            res_ResultLoginDto.UserID = user.UserID;
                            res_ResultLoginDto.CustomerID = null;
                        }
                        else
                        {
                            res_ResultLoginDto.UserID = 0;
                            res_ResultLoginDto.CustomerID = "Diane";
                            res_ResultLoginDto.FullName = "Diane";
                            //FullName Customers Get In Table
                        }

                    }
                    else
                        return new ResultDto<ResultLoginDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            Message = "شما به پنل دسترسی ندارید",
                        };
                }

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(VaribleForName.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {                        
                        new Claim(ClaimTypes.Role,request.LoginName)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                if (!string.IsNullOrEmpty(res_ResultLoginDto.CustomerID)) tokenDescriptor.Subject.AddClaim(new Claim("CustomerID", res_ResultLoginDto.CustomerID));
                if(res_ResultLoginDto.UserID != 0) tokenDescriptor.Subject.AddClaim(new Claim("UserID", res_ResultLoginDto.UserID.ToString()));

                List<NormalJsonClassDto> obj_fillUserRoleCustomerRoles = null;

                switch (request.LoginName)
                {
                    case "Admin":

                        obj_fillUserRoleCustomerRoles = user.UserName != "admin" ?
                            _basicInfoFacad.FillUserRoleAdminRolesService.Execute(qCheckUserRole.Roles).Where(p => p.Selected).ToList() :
                            _basicInfoFacad.FillUserRoleAdminRolesService.Execute();

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

                return new ResultDto<ResultLoginDto>
                {
                    Data = res_ResultLoginDto,
                    IsSuccess = true,
                    Message = string.Empty,
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<UserRolesDto> CheckUserRole(string Role, int UserID)
        {
            try
            {
                var qRole = await _context.Roles.FirstOrDefaultAsync(p => p.RoleTitle == Role);
                var qUserRole = await _context.UserRoles.FirstOrDefaultAsync(p => p.RoleID == qRole.RoleID && p.UserID == UserID);
                return _mapper.Map<UserRolesDto>(qUserRole);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
