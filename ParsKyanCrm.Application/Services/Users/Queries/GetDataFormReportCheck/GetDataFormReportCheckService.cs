using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFormReportCheck
{
    public class GetDataFormReportCheckService : IGetDataFormReportCheckService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetDataFormReportCheckService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }
        public async Task<DataFormReportCheckDto> Execute(RequestDataFormReportCheckDto request)
        {
            try
            {
                var lists = (
                    from s in _context.DataFormReportCheck
                     where (
                         s.IsActive == 15 &&
                         s.CheckId == request.CheckId ||
                         s.QuestionId == request.QuestionId && s.FormId == request.FormId ||
                         s.RequestId == request.RequestId && s.DocumentId == request.DocumentId ||
                         s.AnswerId == request.AnswerId ||
                         s.CategoryId == request.CategoryId && s.QuestionId == request.QuestionId
                     )
                     select s
                );

                var res_Lists = await lists.ToListAsync();

                var res = res_Lists.FirstOrDefault();

                if (res != null)
                {
                    return new DataFormReportCheckDto()
                    {
                        CheckId = res.CheckId,
                        QuestionId = res.QuestionId,
                        AnswerId = res.AnswerId,
                        FormId = res.FormId,
                        RequestId = res.RequestId,
                        CategoryId = res.CategoryId,
                        DocumentId = res.DocumentId,
                        FormCode = res.FormCode,
                        AnswerBeforEdit = res.AnswerBeforEdit,
                        AnswerAfterEdit = res.AnswerAfterEdit,
                        Document = res.Document,
                        SystemScore = res.SystemScore,
                        SuperVisorDescription = res.SuperVisorDescription,
                        CostumerDescriptionBeforEdit = res.CostumerDescriptionBeforEdit,
                        CostumerDescriptionAfterEdit = res.CostumerDescriptionAfterEdit,
                        IsActive = res.IsActive,
                    };
                }
                return new DataFormReportCheckDto()
                {
                    CheckId = 0,
                    QuestionId = 0,
                    AnswerId = 0,
                    FormId = 0,
                    RequestId = 0,
                    CategoryId = 0,
                    DocumentId = 0,
                    FormCode = null,
                    AnswerBeforEdit = null,
                    AnswerAfterEdit = null,
                    Document = res.Document,
                    SystemScore = null,
                    SuperVisorDescription = null,
                    CostumerDescriptionBeforEdit = null,
                    CostumerDescriptionAfterEdit = null,
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
