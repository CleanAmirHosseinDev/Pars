using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataFormDocument
{

    public class GetDataFormDocumentService : IGetDataFormDocumentService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetDataFormDocumentService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }
        public async Task<DataFormDocumentsDto> Execute(int id)
        {
            try
            {
                var lists = (
                    from s in _context.DataFormDocument
                    where (s.IsActive == 15) && (s.DataFormDocumentId == id)
                    select s
                );

                var res_Lists = await lists.ToListAsync();

                var res = res_Lists.FirstOrDefault();

                if (res != null)
                {
                    return new DataFormDocumentsDto()
                    {
                        DataFormDocumentId = res.DataFormDocumentId,
                        Title = res.Title,
                        CategoryId = res.CategoryId,
                        HelpText = res.HelpText,
                        IsRequierd = res.IsRequierd,
                    };
                }
                return new DataFormDocumentsDto()
                {
                    DataFormDocumentId = id,
                    Title = null,
                    CategoryId = 0,
                    HelpText = null,
                    IsRequierd = false,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
