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
                string smsmessage = "";

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

                var aboutEntity = await _context.AboutUs.FirstOrDefaultAsync();

                var cus = await _context.Customers.FindAsync(request.Request.CustomerId);

                if (request.Request.RequestId == 0)
                {

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
                        DestLevelStepIndexButton = VaribleForName.DestLevelStepIndexButton,
                        Request = new Domain.Entities.RequestForRating()
                        {

                            RequestNo = int.Parse(MaxAllRequestNo()),
                            DateOfRequest = dt,
                            KindOfRequest = request.Request.KindOfRequest,
                            CustomerId = cus.CustomerId,
                            IsFinished = false,
                            ChangeDate = dt
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
                        SendUser = request.SendUser,
                        SendTime = dt,
                        DestLevelStepIndex = VaribleForName.DestLevelStepIndex1,
                        LevelStepAccessRole = VaribleForName.LevelStepAccessRole1,
                        LevelStepStatus = VaribleForName.LevelStepStatus1,
                        SmsContent = VaribleForName.SmsContent1,
                        SmsType = VaribleForName.SmsType1,
                        DestLevelStepIndexButton = VaribleForName.DestLevelStepIndexButton1,
                    });
                    await _context.SaveChangesAsync();

                      WebService.SMSService.Execute(aboutEntity.Mobile1, VaribleForName.SmsContent1);
                      WebService.SMSService.Execute(aboutEntity.Mobile2, VaribleForName.SmsContent1);

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
                        SmsContent = request.SmsContent,
                        SmsType = request.SmsType,
                        DestLevelStepIndexButton = request.DestLevelStepIndexButton,
                        ReciveUser = request.ReciveUser,
                         LevelStepSettingIndexID=request.LevelStepSettingIndexID
                    });
                    await _context.SaveChangesAsync();

                    if (request.DestLevelStepIndex == "15")
                    {
                        Ado_NetOperation.SqlUpdate(typeof(RequestForRating).Name, new Dictionary<string, object>()
                        {
                            {
                                "IsFinished",true
                            }
                        }, " RequestID = " + request.Request.RequestId);
                    }



                    switch (request.SmsType)
                    {
                        case true:

                            WebService.SMSService.Execute(aboutEntity.Mobile1, request.SmsContent);
                            WebService.SMSService.Execute(aboutEntity.Mobile2, request.SmsContent);

                            break;
                        case false:

                            var requestForRating = await _context.RequestForRating.Include(p => p.Customer).FirstOrDefaultAsync(p => p.RequestId == request.Request.RequestId);
                            WebService.SMSService.Execute(requestForRating.Customer.AgentMobile, request.SmsContent);

                            break;

                        default:

                            break;
                    }

                }

                Ado_NetOperation.SqlUpdate(typeof(RequestForRating).Name, new Dictionary<string, object>()
                {
                    {
                        "ChangeDate",dt
                    }
                }, " RequestID = " + request.Request.RequestId);

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "اطلاعات شما با موفقیت ثبت شد" ,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
