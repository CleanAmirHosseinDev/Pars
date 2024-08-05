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

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveDataFormDocument
{
    public class SaveDataFormDocumentService : ISaveDataFormDocumentService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public SaveDataFormDocumentService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private bool Check_Remote(DataFormDocumentsDto request)
        {
            try
            {
                string strCondition = string.Empty;

                if (request.DataFormDocumentId == 0)
                {
                    strCondition = "" + nameof(request.DataFormDocumentId) + " = " + request.DataFormDocumentId;
                }


                if (!string.IsNullOrEmpty(strCondition))
                {
                    var q = Ado_NetOperation.GetAll_Table(nameof(DataFormDocument), "*", strCondition);
                    return q != null && q.Rows.Count > 0 ? true : false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ResultDto<DataFormDocumentsDto>> Execute(DataFormDocumentsDto request)
        {
            try
            {
                EntityEntry<DataFormDocument> q_Entity;
                if (Check_Remote(request) == false)
                {
                    request.IsActive = 15;
                    q_Entity = _context.DataFormDocument.Add(_mapper.Map<DataFormDocument>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<DataFormDocumentsDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(nameof(DataFormDocument), new Dictionary<string, object>()
                        {
                            {
                                nameof(q_Entity.Entity.Title),request.Title
                            },
                            {
                                nameof(q_Entity.Entity.CategoryId),request.CategoryId
                            },
                            {
                                nameof(q_Entity.Entity.HelpText),request.HelpText
                            },
                            {
                                nameof(q_Entity.Entity.IsRequierd),request.IsRequierd
                            },
                        }, nameof(q_Entity.Entity.DataFormDocumentId) + $" = {request.DataFormDocumentId}");
                }

                return new ResultDto<DataFormDocumentsDto>()
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
