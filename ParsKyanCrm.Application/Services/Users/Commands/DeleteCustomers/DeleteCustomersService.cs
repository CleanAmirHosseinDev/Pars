﻿using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
        private readonly IWebHostEnvironment _env;

        public DeleteCustomersService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;            
            _env = env;
        }

        public async Task<ResultDto> Execute(int id)
        {

            try
            {

                var cus = await _context.Customers.FindAsync(id);
                var q_req = await _context.RequestForRating.FirstOrDefaultAsync(p => p.CustomerId == id && p.IsFinished==true);

                if (q_req!=null)
                {

                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "امکان حذف این مشتری وجود ندارد"
                    };

                }
              
                var q_user = await _context.Users.FirstOrDefaultAsync(p => p.CustomerId == id);
                var q_reqNoComplet = await _context.RequestForRating.FirstOrDefaultAsync(p => p.CustomerId == id && p.IsFinished == false);
                if (q_reqNoComplet!=null)
                {
                    Ado_NetOperation.SqlDelete(typeof(Domain.Entities.RequestReferences).Name, string.Format("Requestid" + " = '{0}'", q_reqNoComplet.RequestId));
                    Ado_NetOperation.SqlDelete(typeof(Domain.Entities.ContractAndFinancialDocuments).Name, string.Format("Requestid" + " = '{0}'", q_reqNoComplet.RequestId));
                    Ado_NetOperation.SqlDelete(typeof(Domain.Entities.RequestForRating).Name, string.Format("Requestid" + " = '{0}'", q_reqNoComplet.RequestId));

                }
                Ado_NetOperation.SqlDelete(typeof(Domain.Entities.UserRoles).Name, string.Format("UserID" + " = '{0}'", q_user.UserId));
                Ado_NetOperation.SqlDelete(typeof(Domain.Entities.Users).Name, string.Format("CustomerID" + " = '{0}'", id));
                Ado_NetOperation.SqlDelete(typeof(Domain.Entities.Customers).Name, string.Format("CustomerID" + " = '{0}'", id));


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
