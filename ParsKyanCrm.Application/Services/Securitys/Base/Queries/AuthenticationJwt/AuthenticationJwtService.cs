using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Application.Services.Securitys.Queries.Logins;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Securitys.Base.Queries.AuthenticationJwt
{

    public class AuthenticationJwtService : IAuthenticationJwtService
    {

        private readonly IBasicInfoFacad _basicInfoFacad;
        public AuthenticationJwtService(IBasicInfoFacad basicInfoFacad)
        {
            _basicInfoFacad = basicInfoFacad;
        }

        public void Execute(string LoginName, ResultLoginDto res_ResultLoginDto, UserRolesDto qCheckUserRole, Domain.Entities.Users user)
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
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                if (!string.IsNullOrEmpty(res_ResultLoginDto.CustomerID)) tokenDescriptor.Subject.AddClaim(new Claim("CustomerID", res_ResultLoginDto.CustomerID));
                if (res_ResultLoginDto.UserID != 0) tokenDescriptor.Subject.AddClaim(new Claim("UserID", res_ResultLoginDto.UserID.ToString()));

                List<NormalJsonClassDto> obj_fillUserRoleCustomerRoles = null;

                switch (LoginName)
                {
                    case "Admin":

                        obj_fillUserRoleCustomerRoles =
                            user.UserName != "admin" ?
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
