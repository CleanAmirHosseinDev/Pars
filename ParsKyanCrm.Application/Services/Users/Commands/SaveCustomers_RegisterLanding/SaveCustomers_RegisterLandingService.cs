using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Hosting;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveCustomers_RegisterLanding
{

    public class SaveCustomers_RegisterLandingService : ISaveCustomers_RegisterLandingService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        private readonly IWebHostEnvironment _env;

        public SaveCustomers_RegisterLandingService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;            
            _env = env;
        }

        private async Task<string> Validation_Execute(Customers_RegisterLandingDto request)
        {
            try
            {

                ValidationResult result = await new ValidatorCustomers_RegisterLandingDto().ValidateAsync(request);
                if (!result.IsValid) return result.Errors.GetErrorsF();

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<ResultDto> Execute(Customers_RegisterLandingDto request)
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

                              

                DateTime dt = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);


                request.IsActive = (byte)Common.Enums.TablesGeneralIsActive.Active;
                request.SaveDate = dt;
                _context.Customers_RegisterLanding.Add(_mapper.Map<Customers_RegisterLanding>(request));
                await _context.SaveChangesAsync();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "درخواست شما با موفقیت ثبت شد"
                };
            }
            catch (Exception ex)
            {

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };

            }
        }
    }
}
