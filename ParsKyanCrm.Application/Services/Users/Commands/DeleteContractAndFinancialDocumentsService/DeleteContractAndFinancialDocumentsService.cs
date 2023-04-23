using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Application.Services.Users.Commands.DeleteContractAndFinancialDocumentsService;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.DeleteContract
{

    public class DeleteContractAndFinancialDocumentsService : IDeleteContractAndFinancialDocumentsService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IWebHostEnvironment _env;

        public DeleteContractAndFinancialDocumentsService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            _env = env;
        }

        public ResultDto Execute(int id)
        {

            try
            {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.ContractAndFinancialDocuments).Name, new Dictionary<string, object>()
                    {
                      {
                        "IsActive",(byte)Common.Enums.TablesGeneralIsActive.InActive
                      }
                    }, string.Format("RequestID" + " = '{0}' and ConfirmCommitteeEvaluation=0", id));

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = " قرارداد موردنظر با موفقیت حذف شد"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
