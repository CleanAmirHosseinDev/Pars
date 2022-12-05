using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveRequestForRating
{

    public class SaveRequestForRatingService : ISaveRequestForRatingService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;

        public SaveRequestForRatingService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        private string MaxAllRequestNo()
        {
            try
            {
                List<RequestForRatingDto> q = Ado_NetOperation.ConvertDataTableToList<RequestForRatingDto>(Ado_NetOperation.GetAll_Table(typeof(RequestForRating).Name, "cast(isnull((max(cast((isnull(RequestNo,'1')) as bigint))+1),1) as nvarchar(max)) as RequestNoStr"));
                if (q != null) return q.FirstOrDefault().RequestNoStr.ToString();
                return "1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResultDto> Execute(RequestReferencesDto request)
        {
            try
            {
                DateTime dt = DateTimeOperation.InsertFieldDataTimeInTables(DateTime.Now);

                if (request.Request.RequestId == 0)
                {

                    var cus = await _context.Customers.FindAsync(request.Request.CustomerId);
                    if (!cus.IsProfileComplete)
                    {

                        return new ResultDto()
                        {
                            IsSuccess = false,
                            Message = "لطفا از قسمت پروفایل کاربری اطلاعات خود را تکمیل کنید"
                        };

                    }

                    var qRequest = await _context.RequestForRating.FirstOrDefaultAsync(p => p.CustomerId == cus.CustomerId && p.IsFinished == false && p.KindOfRequest == request.Request.KindOfRequest);
                    if (qRequest != null)
                    {

                        return new ResultDto()
                        {
                            IsSuccess = false,
                            Message = "شما یک درخواست باز دارید و امکان ثبت مجدد وجود ندارد"
                        };

                    }

                    _context.RequestReferences.Add(new RequestReferences()
                    {
                        DestLevelStepIndex = 1,
                        Request = new Domain.Entities.RequestForRating()
                        {

                            RequestNo = int.Parse(MaxAllRequestNo()),
                            DateOfRequest = dt,
                            KindOfRequest = request.Request.KindOfRequest,
                            CustomerId = cus.CustomerId,
                            IsFinished = false,
                        },
                        Comment = null,
                        SendUser = request.SendUser,
                        SendTime = dt,
                        LevelStepAccessRole = "10",
                    });
                    await _context.SaveChangesAsync();



                }
                else
                {

                    _context.RequestReferences.Add(new RequestReferences()
                    {
                        DestLevelStepIndex = request.DestLevelStepIndex,
                        Comment = request.SendUser == null ? null : request.Comment,
                        SendUser = request.SendUser,
                        Requestid = request.Request.RequestId,
                        SendTime = dt,
                        LevelStepAccessRole = request.LevelStepAccessRole
                    });
                    await _context.SaveChangesAsync();

                }


                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "اطلاعات شما با موفقیت ثبت شد"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
