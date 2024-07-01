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
using ParsKyanCrm.Domain.Entities;
using Org.BouncyCastle.Asn1.Ocsp;
using static Dapper.SqlMapper;

namespace ParsKyanCrm.Application.Services.Users.Commands.DeleteDataFormDocument
{

    public class DeleteDataFormDocumentService : IDeleteDataFormDocumentService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        private readonly IWebHostEnvironment _env;

        public DeleteDataFormDocumentService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;            
            _env = env;
        }

        public ResultDto Execute(int id)
        {
            try
            {
                Ado_NetOperation.SqlUpdate(nameof(DataFormDocument), new Dictionary<string, object>()
                    {
                    {
                        "IsActive",(byte)Common.Enums.TablesGeneralIsActive.InActive
                    }
                }, "DataFormDocumentId" + $" = {id}");

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "فرم با موفقیت حذف شد"
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
