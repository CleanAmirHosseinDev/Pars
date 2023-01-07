using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.DeleteCustomers
{

    public class DeleteCustomersService : IDeleteCustomersService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IWebHostEnvironment _env;

        public DeleteCustomersService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            _env = env;
        }

        public async Task<ResultDto> Execute(int id)
        {

            try
            {

                var cus = await _context.Customers.FindAsync(id);

                if (cus.IsProfileComplete)
                {

                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "امکان حذف این مشتری وجود ندارد"
                    };

                }

                Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.Customers).Name, new Dictionary<string, object>()
                    {                    
                    {
                        "IsActive",(byte)Common.Enums.TablesGeneralIsActive.InActive
                    }
                    }, string.Format("CustomerID" + " = '{0}' ", id));

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "مشتری موردنظر با موفقیت حذف شد"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
