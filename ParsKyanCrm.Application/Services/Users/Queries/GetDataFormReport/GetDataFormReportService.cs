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
            try
            {
                var res_Lists = (
                    from s in _context.DataFormReport
                    where s.DataFormAnswerId == request.DataFormAnswerId && s.RequestId == request.RequestId
                    select s);

                var res = await res_Lists.FirstOrDefaultAsync();

                if (res != null)
                {
                    return new DataFormReportDto()
                    {
                        DataReportId = res.DataReportId,
                        RequestId = res.RequestId,
                        DataFormAnswerId = res.DataFormAnswerId,
                        SystemScore = res.SystemScore,
                        AnalizeScore = res.AnalizeScore,
                        Description = res.Description,
                        IsActive = 15,
                    };
                }
                return new DataFormReportDto()
                {
                    DataReportId = 0,
                    RequestId = 0,
                    DataFormAnswerId = 0,
                    SystemScore = 0,
                    AnalizeScore = 0,
                    Description = null,
                    IsActive = 14,
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
