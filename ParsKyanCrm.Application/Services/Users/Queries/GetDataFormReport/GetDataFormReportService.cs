using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Application.Services.Users.Queries.GetDataFromReport;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFromReport
{
    public class GetDataFormReportService : IGetDataFormReportService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetDataFormReportService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        public async Task<DataFormReportDto> Execute(int? id = null)
        {
            try
            {
                DataFormReportDto res = new DataFormReportDto();

                if (id != null && id != 0)
                {
                    var q_Find = await _context.DataFormReport.FirstOrDefaultAsync(p => p.DataReportId == id.Value);
                    res = _mapper.Map<DataFormReportDto>(q_Find);
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DataFormReportDto> ExecuteWhithParam(RequestDataFormReportDto request)
        {
            // https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-data-type-mappings
            try
            {
                DataFormReportDto res = new DataFormReportDto();

                if (request.DataFormAnswerId != null && request.DataFormAnswerId != 0)
                {
                    if (request.RequestId != null && request.RequestId != 0)
                    {
                        var q_Find = await _context.DataFormReport.FirstOrDefaultAsync(
                            p => p.DataFormAnswerId == request.DataFormAnswerId.Value && p.RequestId == request.RequestId.Value
                        );
                        res = _mapper.Map<DataFormReportDto>(q_Find);
                        if (res != null)
                            return res;
                    }
                    var q_Find1 = await _context.DataFormReport.FirstOrDefaultAsync(p => p.DataFormAnswerId == request.DataFormAnswerId.Value);
                    res = _mapper.Map<DataFormReportDto>(q_Find1);

                }
                if (res != null)
                    return res;

                return new DataFormReportDto()
                {
                    RequestId = request.RequestId.Value,
                    AnalizeScore = 0,
                    DataFormAnswerId = request.DataFormAnswerId.Value,
                    DataReportId = 0,
                    Description = "",
                    IsActive = 14,
                    SystemScore = 0,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
