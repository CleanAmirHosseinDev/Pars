using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFormAnswerTabless
{

    public class GetDataFormAnswerTablessService : IGetDataFormAnswerTablessService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetDataFormAnswerTablessService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ResultDto<IEnumerable<DataFormAnswerTablesDto>>> Execute(RequestDataFormAnswerTablesDto request)
        {
            try
            {

                var q = await Infrastructure.DapperOperation.Run<DataFormAnswerTablesDto>("exec [dbo].[GetDataFormAnswerTables] " + (request.FormId.HasValue ? request.FormId.Value : string.Empty) + (request.CustomerId.HasValue ? "," + request.CustomerId.Value : string.Empty) + (request.AnswerTableId.HasValue ? "," + request.AnswerTableId.Value : string.Empty));

                return new ResultDto<IEnumerable<DataFormAnswerTablesDto>>
                {
                    Data = q,
                    IsSuccess = true,
                    Message = string.Empty,
                    Rows = q.LongCount(),
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
