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
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveQuestionLevel
{
    public class SaveQuestionLevelService : ISaveQuestionLevelService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public SaveQuestionLevelService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private bool Check_Remote(QuestionLevelDto request)
        {
            try
            {
                string strCondition = string.Empty;

                if (request.QuestionLevelId == 0)
                {
                    strCondition = "" + nameof(request.QuestionLevelId) + " = " + request.QuestionLevelId;
                }


                if (!string.IsNullOrEmpty(strCondition))
                {
                    var q = Ado_NetOperation.GetAll_Table(nameof(QuestionLevel), "*", strCondition);
                    return q != null && q.Rows.Count > 0 ? true : false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResultDto<QuestionLevelDto>> Execute(QuestionLevelDto request)
        {
            var req_id = 0;
            try
            {
                EntityEntry<QuestionLevel> q_Entity;
                if (Check_Remote(request) == false)
                {
                    request.IsActive = 15;
                    q_Entity = _context.QuestionLevel.Add(_mapper.Map<QuestionLevel>(request));
                    await _context.SaveChangesAsync();
                    req_id = q_Entity.Entity.QuestionLevelId;
                    request = _mapper.Map<QuestionLevelDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(nameof(QuestionLevel), new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.LevelTitle),request.LevelTitle
                        },
                        {
                            nameof(q_Entity.Entity.LevelDescription),request.LevelDescription
                        },
                    }, nameof(q_Entity.Entity.QuestionLevelId) + $" = {request.QuestionLevelId}");
                    req_id = request.QuestionLevelId;
                }

                return new ResultDto<QuestionLevelDto>()
                {
                    IsSuccess = true,
                    DataId = req_id,
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
