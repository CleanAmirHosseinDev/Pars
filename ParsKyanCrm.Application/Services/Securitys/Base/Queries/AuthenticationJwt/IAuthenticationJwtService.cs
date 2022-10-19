using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Services.Securitys.Queries.Logins;

namespace ParsKyanCrm.Application.Services.Securitys.Base.Queries.AuthenticationJwt
{
    public interface IAuthenticationJwtService
    {
        void Execute(string LoginName, ResultLoginDto res_ResultLoginDto, UserRolesDto qCheckUserRole, Domain.Entities.Users.Users user);
    }
}
