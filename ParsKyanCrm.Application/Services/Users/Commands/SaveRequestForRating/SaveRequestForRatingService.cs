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
        private readonly IValidator<RequestReferencesDto> _validatorRequestReferencesDto;

        public SaveRequestForRatingService(IDataBaseContext context, IMapper mapper, IValidator<RequestReferencesDto> validatorRequestReferencesDto)
        {
            _context = context;
            _mapper = mapper;
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


                    var qSystemSetting = await _context.SystemSeting.FindAsync(request.Request.KindOfRequest);
                    if (string.IsNullOrEmpty(qSystemSetting.ConfigKindOfRequestInitilizeOne))
                    {
                        return new ResultDto()
                        {
                            IsSuccess = false,
                            Message = "تنظیمات نوع خدمت مورد تقاضا تنظیم نشده است لطفا با پشتیبانی تماس حاصل فرمایید"
                        };
                    }

                    string[] strArrConfigKindOfRequest = qSystemSetting.ConfigKindOfRequestInitilizeOne.Split("-");


                    var rr = _context.RequestReferences.Add(new RequestReferences()
                    {
                        DestLevelStepIndex = strArrConfigKindOfRequest[0],
                        LevelStepAccessRole = strArrConfigKindOfRequest[1],
                        LevelStepStatus = strArrConfigKindOfRequest[2],
                        DestLevelStepIndexButton = strArrConfigKindOfRequest[3],
                        Request = new Domain.Entities.RequestForRating()
                        {

                            RequestNo = int.Parse(MaxAllRequestNo()),
                            DateOfRequest = dt,
                            KindOfRequest = request.Request.KindOfRequest,
                            CustomerId = cus.CustomerId,
                            IsFinished = false,
                            ChangeDate = dt
                        },
                        SmsContent = strArrConfigKindOfRequest[4],
                        SmsType = bool.Parse(strArrConfigKindOfRequest[5]),

                        Comment = null,
                        SendUser = request.SendUser,
                        SendTime = dt,
                        LevelStepSettingIndexID = 1,
                        ReciveUser = strArrConfigKindOfRequest[6],

                    });
                    await _context.SaveChangesAsync();

                    //_context.RequestReferences.Add(new RequestReferences()
                    //{
                    //    Requestid = rr.Entity.Requestid,
                    //    Comment = null,
                    //    SendUser = request.SendUser,
                    //    SendTime = dt,
                    //    DestLevelStepIndex = VaribleForName.DestLevelStepIndex1,
                    //    LevelStepAccessRole = VaribleForName.LevelStepAccessRole1,
                    //    LevelStepStatus = VaribleForName.LevelStepStatus1,
                    //    SmsContent = VaribleForName.SmsContent1,
                    //    SmsType = VaribleForName.SmsType1,
                    //    DestLevelStepIndexButton = VaribleForName.DestLevelStepIndexButton1,
                    //    LevelStepSettingIndexID=2,
                    //    ReciveUser=VaribleForName.ReciveUser1
                    //});
                    //await _context.SaveChangesAsync();

                    await WebService.SMSService.Execute(aboutEntity.Mobile1, VaribleForName.SmsContent1);
                    await WebService.SMSService.Execute(aboutEntity.Mobile2, VaribleForName.SmsContent1);

                }
                else
                {
                    //In Bollow
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
                        LevelStepSettingIndexID = request.LevelStepSettingIndexID
                    });
                    await _context.SaveChangesAsync();

                    if (request.DestLevelStepIndex == "0")
                    {
                        DateTime CodalDate = DateTimeOperation.InsertFieldDataTimeInTables(DateTimeOperation.ConvertStringToDateTime(request.CodalDate));

                        Ado_NetOperation.SqlUpdate(typeof(RequestForRating).Name, new Dictionary<string, object>()
                        {
                            {
                                "IsFinished",true
                            },
                            {
                                 "CodalDate",   CodalDate.ToString()
                            },
                            {
                                "CodalNumber",request.CodalNumber
                            }
                        }, " RequestID = " + request.Request.RequestId);



                    }
                    if (request.DestLevelStepIndex == "110")
                    {

                        Ado_NetOperation.SqlUpdate(typeof(RequestForRating).Name, new Dictionary<string, object>()
                        {
                            {
                                "IsFinished",true
                            }
                        }, " RequestID = " + request.Request.RequestId);



                    }

                    switch (request.SmsType) // تمومه
                    {
                        case true:

                            await WebService.SMSService.Execute(aboutEntity.Mobile1, request.SmsContent);
                            await WebService.SMSService.Execute(aboutEntity.Mobile2, request.SmsContent);

                            break;
                        case false:

                           
                            //var requestForRating = await _context.RequestForRating.FirstOrDefaultAsync(p => p.RequestId == request.Request.RequestId);
                            //var cust = await _context.Customers.FirstOrDefaultAsync(p => p.CustomerId == requestForRating.CustomerId);
                            //if (request.SmsContent.Contains("{0}"))
                            //    request.SmsContent = "مشتری محترم،" + cus.CompanyName + "\n" + string.Format(request.SmsContent, request.Request.RequestId).Replace("\\n", System.Environment.NewLine)
                            //        + "\n" + "شرکت رتبه بندی اعتباری پارس کیان";
                            //else request.SmsContent = "مشتری محترم،" + cust.CompanyName + "\n" + request.SmsContent + "\n" + "شماره درخواست:" + request.Request.RequestNo + "\n" + "شرکت رتبه بندی اعتباری پارس کیان";

                            //await WebService.SMSService.Execute(cust.AgentMobile, request.SmsContent);

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
                    Message = "اطلاعات شما با موفقیت ثبت شد",
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
