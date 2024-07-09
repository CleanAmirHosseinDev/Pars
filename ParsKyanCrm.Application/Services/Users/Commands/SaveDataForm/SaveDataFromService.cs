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

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveDataForm
{
    public class SaveDataFormService : ISaveDataFormService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public SaveDataFormService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private bool Check_Remote(DataFormsDto request)
        {
            try
            {
                string strCondition = string.Empty;

                if (request.FormId == 0)
                {
                    strCondition = "" + nameof(request.FormId) + " = " + request.FormId;
                }


                if (!string.IsNullOrEmpty(strCondition))
                {
                    var q = Ado_NetOperation.GetAll_Table(nameof(DataForms), "*", strCondition);
                    return q != null && q.Rows.Count > 0 ? true : false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResultDto<DataFormsDto>> Execute(DataFormsDto request)
        {
            try
            {
                EntityEntry<DataForms> q_Entity;
                if (Check_Remote(request) == false)
                {
                    request.IsActive = 15;
                    q_Entity = _context.DataForms.Add(_mapper.Map<DataForms>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<DataFormsDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(nameof(DataForms), new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.FormCode),request.FormCode
                        },
                        {
                            nameof(q_Entity.Entity.FormTitle),request.FormTitle
                        },
                        {
                            nameof(q_Entity.Entity.CategoryId),request.CategoryId
                        },
                    }, nameof(q_Entity.Entity.FormId) + $" = {request.FormId}");
                }

                return new ResultDto<DataFormsDto>()
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
