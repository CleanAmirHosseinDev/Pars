﻿using AutoMapper;
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

namespace ParsKyanCrm.Application.Services.Users.Commands.DeleteServiceFee
{

    public class DeleteServiceFeeService : IDeleteServiceFeeService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        private readonly IWebHostEnvironment _env;

        public DeleteServiceFeeService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;            
            _env = env;
        }

        public ResultDto Execute(int id)
        {

            try
            {

                Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.ServiceFee).Name, new Dictionary<string, object>()
                    {
                    {
                        "IsActive",(byte)Common.Enums.TablesGeneralIsActive.InActive
                    }
                    }, string.Format("ServiceFeeId" + " = '{0}' ", id));

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "نرخ نامه قرارداد موردنظر با موفقیت حذف شد"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
