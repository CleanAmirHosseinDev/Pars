using AutoMapper;
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

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveCompanies
{

    public class SaveCompaniesService : ISaveCompaniesService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        public SaveCompaniesService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        public async Task<ResultDto<CompaniesDto>> Execute(CompaniesDto request)
        {
            try
            {
                #region Validation



                #endregion

                EntityEntry<Companies> q_Entity;
                if (request.CompaniesId == 0)
                {
                    request.IsActive = (byte)Common.Enums.TablesGeneralIsActive.Active;
                    q_Entity = _context.Companies.Add(_mapper.Map<Companies>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<CompaniesDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.Companies).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.CompanyName),request.CompanyName
                        },
                        {
                            nameof(q_Entity.Entity.CompanyGroupId),request.CompanyGroupId
                        },
                        {
                            nameof(q_Entity.Entity.KindOfCompany),request.KindOfCompany
                        },                        
                    }, string.Format(nameof(q_Entity.Entity.CompaniesId) + " = {0} ", request.CompaniesId));
                }

                return new ResultDto<CompaniesDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت شرکتها با موفقیت انجام شد",
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
