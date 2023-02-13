using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SavePublicActivities
{

    public class SavePublicActivitiesService : ISavePublicActivitiesService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IWebHostEnvironment _env;
        public SavePublicActivitiesService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            
        }

        public async Task<ResultDto<PublicActivitiesDto>> Execute(PublicActivitiesDto request)
        {
            
            try
            {
               
                #region Validation



                #endregion

              

                EntityEntry<PublicActivities> q_Entity;
                if (request.PublicActivitiesID == 0)
                {
                   // request.SaveDate = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);
                    q_Entity = _context.PublicActivities.Add(_mapper.Map<PublicActivities>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<PublicActivitiesDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.PublicActivities).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.RequestId),request.RequestId
                        },
                        {
                            nameof(q_Entity.Entity.CustomerID),request.CustomerID
                        },
                        {
                            nameof(q_Entity.Entity.EmploymentDisabled),request.EmploymentDisabled
                        },
                        {
                            nameof(q_Entity.Entity.Investment),request.Investment
                        }
                        ,
                        {
                            nameof(q_Entity.Entity.IsPublicActivityFile),request.IsPublicActivityFile
                        }
                    }, string.Format(nameof(q_Entity.Entity.PublicActivitiesID) + " = {0} ", request.PublicActivitiesID));
                    
                }

                return new ResultDto<PublicActivitiesDto>()
                {
                    IsSuccess = true,
                    Message = " با موفقیت انجام شد",
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
