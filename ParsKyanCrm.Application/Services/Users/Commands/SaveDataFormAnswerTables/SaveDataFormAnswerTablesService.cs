using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveDataFormAnswerTables
{

    public class SaveDataFormAnswerTablesService : ISaveDataFormAnswerTablesService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IWebHostEnvironment _env;

        public SaveDataFormAnswerTablesService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            _env = env;
        }

        public async Task<ResultDto<DataFormAnswerTablesDto>> Execute(DataFormAnswerTablesDto request)
        {
            try
            {
                #region Validation



                #endregion

                EntityEntry<DataFormAnswerTables> q_Entity;
                if (request.AnswerTableId == 0)
                {                    
                    q_Entity = _context.DataFormAnswerTables.Add(_mapper.Map<DataFormAnswerTables>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<DataFormAnswerTablesDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.DataFormAnswerTables).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.FormId),request.FormId
                        },
                        {
                            nameof(q_Entity.Entity.CustomerId),request.CustomerId
                        },
                        {
                            nameof(q_Entity.Entity.Answer1),request.Answer1
                        },
                        {
                            nameof(q_Entity.Entity.Answer2),request.Answer2
                        },
                        {
                            nameof(q_Entity.Entity.Answer3),request.Answer3
                        },
                        {
                            nameof(q_Entity.Entity.Answer4),request.Answer4
                        },
                        {
                            nameof(q_Entity.Entity.Answer5),request.Answer5
                        },
                        {
                            nameof(q_Entity.Entity.Answer6),request.Answer6
                        },
                        {
                            nameof(q_Entity.Entity.Answer7),request.Answer7
                        },
                        {
                            nameof(q_Entity.Entity.Answer8),request.Answer8
                        },
                        {
                            nameof(q_Entity.Entity.Answer9),request.Answer9
                        },
                        {
                            nameof(q_Entity.Entity.Answer10),request.Answer10
                        },
                    }, string.Format(nameof(q_Entity.Entity.AnswerTableId) + " = {0} ", request.AnswerTableId));
                }

                return new ResultDto<DataFormAnswerTablesDto>()
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
