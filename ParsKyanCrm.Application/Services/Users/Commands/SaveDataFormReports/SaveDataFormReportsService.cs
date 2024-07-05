using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure.Consts;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParsKyanCrm.Application.Services.Users.Commands.SaveDataFormDocument;
using ParsKyanCrm.Application.Services.Users.Commands.SaveDataFormReports;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveDataFormDocument
{
    public class SaveDataFormReportsService : ISaveDataFormReportsService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public SaveDataFormReportsService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private bool Check_Remote(DataFormReportDto request)
        {
            try
            {
                string strCondition = string.Empty;

                if (request.RequestId != 0)
                {
                    strCondition = "" + nameof(request.RequestId) + " = " + request.RequestId + " AND " + nameof(request.DataFormAnswerId) + $" = {request.DataFormAnswerId}";
                }
                if (!string.IsNullOrEmpty(strCondition))
                {
                    var q = Ado_NetOperation.GetAll_Table(nameof(DataFormReport), "*", strCondition);
                    return q != null && q.Rows.Count > 0 ? true : false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ResultDto<DataFormReportDto>> Execute(DataFormReportDto request)
        {
            try
            {
                EntityEntry<DataFormReport> q_Entity;
                if (Check_Remote(request) == false)
                {
                    request.IsActive = 15;
                    q_Entity = _context.DataFormReport.Add(_mapper.Map<DataFormReport>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<DataFormReportDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(nameof(DataFormReport), new Dictionary<string, object>()
                        {
                            {
                                nameof(q_Entity.Entity.AnalizeScore),request.AnalizeScore
                            },
                            {
                                nameof(q_Entity.Entity.SystemScore),request.SystemScore
                            },
                            {
                                nameof(q_Entity.Entity.Description),request.Description
                            },
                        }, nameof(q_Entity.Entity.DataReportId) + $" = {request.DataReportId}");
                }

                return new ResultDto<DataFormReportDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت فرم با موفقیت انجام شد",
                    Data = request
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
