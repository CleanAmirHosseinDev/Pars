using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
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

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveRequestForRating
{

    public class SaveRequestForRatingService : ISaveRequestForRatingService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IValidator<RequestReferencesDto> _validatorRequestReferencesDto;

        public SaveRequestForRatingService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IValidator<RequestReferencesDto> validatorRequestReferencesDto)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            _validatorRequestReferencesDto = validatorRequestReferencesDto;
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

        private async Task<string> Validation_Execute(RequestReferencesDto request)
        {
            try
            {

                ValidationResult result = await _validatorRequestReferencesDto.ValidateAsync(request);
                if (!result.IsValid) return result.Errors.GetErrorsF();

                return string.Empty;
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

                #region Validation

                string strValidation = await Validation_Execute(request);
                if (!string.IsNullOrEmpty(strValidation))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = strValidation
                    };
                }

                #endregion                                

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

                    var rr = _context.RequestReferences.Add(new RequestReferences()
                    {
                        DestLevelStepIndex = VaribleForName.DestLevelStepIndex,
                        LevelStepAccessRole = VaribleForName.LevelStepAccessRole,
                        LevelStepStatus = VaribleForName.LevelStepStatus,
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
                    });
                    await _context.SaveChangesAsync();

                    _context.RequestReferences.Add(new RequestReferences()
                    {
                        Requestid = rr.Entity.Requestid,
                        Comment = null,
                        SendUser = null,
                        SendTime = dt,
                        DestLevelStepIndex = VaribleForName.DestLevelStepIndex1,
                        LevelStepAccessRole = VaribleForName.LevelStepAccessRole1,
                        LevelStepStatus = VaribleForName.LevelStepStatus1
                    });
                    await _context.SaveChangesAsync();


                }
                else
                {

                    _context.RequestReferences.Add(new RequestReferences()
                    {
                        DestLevelStepIndex = request.DestLevelStepIndex,
                        Comment = request.Comment,
                        SendUser = request.SendUser,
                        Requestid = request.Request.RequestId,
                        SendTime = dt,
                        LevelStepAccessRole = request.LevelStepAccessRole,
                        LevelStepStatus = request.LevelStepStatus,
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
