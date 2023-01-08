using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.DeleteManagerOfParsKyan
{

    public class DeleteManagerOfParsKyanService : IDeleteManagerOfParsKyanService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public DeleteManagerOfParsKyanService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public ResultDto Execute(int id)
        {

            try
            {

                Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.ManagerOfParsKyan).Name, new Dictionary<string, object>()
                    {
                    {
                        "IsActive",(byte)Common.Enums.TablesGeneralIsActive.InActive
                    }
                    }, string.Format("ManagersID" + " = '{0}' ", id));

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "ساختار مدیریت موردنظر با موفقیت حذف شد"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
