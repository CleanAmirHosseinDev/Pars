using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Commands.SaveAboutUs
{

    public class SaveAboutUsService : ISaveAboutUsService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public SaveAboutUsService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<AboutUsDto>> Execute(AboutUsDto request)
        {
            try
            {
                EntityEntry<AboutUs> q_Entity;

                var qAboutUs = await _context.AboutUs.FirstOrDefaultAsync();

                if (qAboutUs == null)
                {
                    request.DateOfSaveorEdit = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);
                    q_Entity = _context.AboutUs.Add(_mapper.Map<AboutUs>(request));
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<AboutUsDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(AboutUs).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.CompanyName),request.CompanyName
                        },
                        {
                            nameof(q_Entity.Entity.Subject),request.Subject
                        },
                        {
                            nameof(q_Entity.Entity.AboutUscontent),request.AboutUscontent
                        },
                        {
                            nameof(q_Entity.Entity.Tel1),request.Tel1
                        },
                        {
                            nameof(q_Entity.Entity.Tel2),request.Tel2
                        },
                        {
                            nameof(q_Entity.Entity.Tel3),request.Tel3
                        },
                        {
                            nameof(q_Entity.Entity.Tel4),request.Tel4
                        },
                        {
                            nameof(q_Entity.Entity.Tel5),request.Tel5
                        },
                        {
                            nameof(q_Entity.Entity.Mobile1),request.Mobile1
                        },
                        {
                            nameof(q_Entity.Entity.Mobile2),request.Mobile2
                        },
                        {
                            nameof(q_Entity.Entity.FaxNumber),request.FaxNumber
                        },
                        {
                            nameof(q_Entity.Entity.Address),request.Address
                        },
                        {
                            nameof(q_Entity.Entity.Email),request.Email
                        },
                        {
                            nameof(q_Entity.Entity.Moto1),request.Moto1
                        },
                        {
                            nameof(q_Entity.Entity.Moto2),request.Moto2
                        },
                        {
                            nameof(q_Entity.Entity.Moto3),request.Moto3
                        },
                        {
                            nameof(q_Entity.Entity.Moto4),request.Moto4
                        },
                        {
                            nameof(q_Entity.Entity.Moto5),request.Moto5
                        },
                        {
                            nameof(q_Entity.Entity.Instagram),request.Instagram
                        },
                        {
                            nameof(q_Entity.Entity.Whatsapp),request.Whatsapp
                        },                        
                        {
                            nameof(q_Entity.Entity.Telegram),request.Telegram
                        },
                        {
                            nameof(q_Entity.Entity.VisionAndMission),request.VisionAndMission
                        },
                        {
                            nameof(q_Entity.Entity.OrganazationChart),request.OrganazationChart
                        },
                        {
                            nameof(q_Entity.Entity.Userid),request.Userid
                        },
                        {
                            nameof(q_Entity.Entity.DateOfSaveorEdit),DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now)
                        },
                    }, string.Format(nameof(q_Entity.Entity.AboutUsId) + " = {0} ", qAboutUs.AboutUsId));
                }

                return new ResultDto<AboutUsDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت درباره با موفقیت انجام شد",
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
