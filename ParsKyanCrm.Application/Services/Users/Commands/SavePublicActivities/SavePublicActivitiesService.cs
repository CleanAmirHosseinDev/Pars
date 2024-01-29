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
        private readonly IWebHostEnvironment _env;
        public SavePublicActivitiesService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;


        }

        public async Task<ResultDto<PublicActivitiesDto>> Execute(PublicActivitiesDto request)
        {

            try
            {

                #region Validation



                #endregion

                if (request.IsPublicActivityFileStr != null)
                {
                    if (request.IsPublicActivityFileStr == "on")
                    {
                        request.IsPublicActivityFile = true;
                    }
                    else
                    {
                        request.IsPublicActivityFile = false;
                    }
                }

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

        public async Task<ResultDto<PublicActivitiesDto>> ExecuteCopy(string Request)
        {

            try
            {
                string[] values = Request.Split('-');
                int newReq = Convert.ToInt32(values[0]);
                int OldReq = Convert.ToInt32(values[1]);

                #region Validation



                #endregion
                var con = await Infrastructure.DapperOperation.Run<PublicActivitiesDto>("select * from PublicActivities where RequestId=" + OldReq);

                foreach (var item in con)
                {
                    PublicActivitiesDto request = new PublicActivitiesDto();

                    if (item.IsPublicActivityFileStr != null)
                    {
                        if (item.IsPublicActivityFileStr == "on")
                        {
                            request.IsPublicActivityFile = true;
                        }
                        else
                        {
                            request.IsPublicActivityFile = false;
                        }
                    }

                    request.CustomerID = item.CustomerID;
                    request.EmploymentDisabled = item.EmploymentDisabled;
                    request.Investment = item.Investment;
                    request.IsActive = item.IsActive;
                    request.IsPublicActivityFile = item.IsPublicActivityFile;
                    request.RequestId = newReq;

                    EntityEntry<PublicActivities> q_Entity;
                    if (request.PublicActivitiesID == 0)
                    {
                        // request.SaveDate = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);
                        q_Entity = _context.PublicActivities.Add(_mapper.Map<PublicActivities>(request));
                        await _context.SaveChangesAsync();
                        request = _mapper.Map<PublicActivitiesDto>(q_Entity.Entity);
                    }

                }


                return new ResultDto<PublicActivitiesDto>()
                {
                    IsSuccess = true,
                    Message = " با موفقیت انجام شد",
                    Data = null
                };


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
