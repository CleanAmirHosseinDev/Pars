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

namespace ParsKyanCrm.Application.Services.Users.Queries.GetDataForm
{
    public class GetDataFormService : IGetDataFormService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public GetDataFormService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        public async Task<DataFormsDto> Execute(int id)
        {
            try
            {
                var lists = (
                    from s in _context.DataForms
                    where (s.IsActive == 15 ) && (s.FormId == id)
                    select s
                );

                var res_Lists = await lists.ToListAsync();

                var res = res_Lists.FirstOrDefault();

                if (res != null)
                {
                    return new DataFormsDto()
                    {
                        CategoryId = res.CategoryId,
                        FormId = res.FormId,
                        FormCode = res.FormCode,
                        FormTitle = res.FormTitle,
                        IsTable = res.IsTable,
                    };
                }
                return new DataFormsDto()
                {
                    CategoryId = null,
                    FormId = id,
                    FormCode = null,
                    FormTitle = null,
                    IsTable = null,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
