using AutoMapper;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.UpdatePassUsers
{

    public class UpdatePassUsersService : IUpdatePassUsersService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public UpdatePassUsersService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        private async Task<string> Validation_Execute(RequestUpdatePassUsersDto request)
        {
            try
            {

                ValidationResult result = await new ValidatorRequestUpdatePassUsersDto().ValidateAsync(request);
                if (!result.IsValid) return result.Errors.GetErrorsF();

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResultDto> Execute(RequestUpdatePassUsersDto request)
        {
            try
            {
                #region Validation

                string strValidation = await Validation_Execute(request);
                if (!string.IsNullOrEmpty(strValidation))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = strValidation
                    };
                }

                #endregion

                var userObject = await _context.Users.FirstOrDefaultAsync(p => p.UserId == request.UserID);

                if (userObject.Password != EncryptDecrypt.Encrypt(request.OldPassword))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "کلمه عبور جاری اشتباه می باشد"
                    };
                }

                Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.Users).Name, new Dictionary<string, object>()
                    {
                        {
                            "Password",EncryptDecrypt.Encrypt(request.NewPassword)
                        }
                    }, string.Format("UserID" + " = {0} ", request.UserID));

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "تغییر کلمه عبور با موفقیت انجام شد"
                };


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
