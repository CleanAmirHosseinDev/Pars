﻿using FluentValidation;

using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Enums;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{
    public class RequestRequestForRatingDto : PageingParamerDto
    {
        public bool ForTimeLine { get; set; } = false;
        public int? CustomerId { get; set; }
        public bool IsExcelReport { get; set; }
        public string LoginName { get; set; }
        public int? RequestId { get; set; }
        public int? DestLevelStepIndex { get; set; }
        public string UserID { get; set; }
        public int? KindOfRequest { get; set; }
        public bool IsMyRequests { get; set; }
        public string FromDateStr { get; set; }
        public DateTime? FromDate
        {
            get
            {
                return (FromDateStr != "" && FromDateStr!=null ? DateTimeOperation.ToMiladiDate(DateTimeOperation.InsertFieldDataTimeInTables(DateTimeOperation.ConvertStringToDateTime(FromDateStr))) : null);
            }
        }

        public int IsCorporate { get; set; }
        public string FromSendTimeDateStr { get; set; }

        public DateTime? FromSendTimeDate
        {
            get
            {

                return (FromSendTimeDateStr != "" && FromSendTimeDateStr != null ? DateTimeOperation.ToMiladiDate(DateTimeOperation.InsertFieldDataTimeInTables(DateTimeOperation.ConvertStringToDateTime(FromSendTimeDateStr))) : null);
            }
        }
        public string ToDateStr { get; set; }
        public DateTime? ToDate { 
            get {
            
                return (ToDateStr!="" && ToDateStr != null ? DateTimeOperation.ToMiladiDate(DateTimeOperation.InsertFieldDataTimeInTables(DateTimeOperation.ConvertStringToDateTime(ToDateStr))):null);
            } 
        }

        public string ToSendTimeDateStr { get; set; }
        public DateTime? ToSendTimeDate
        {
            get
            {

                return (ToSendTimeDateStr != "" && ToSendTimeDateStr != null ? DateTimeOperation.ToMiladiDate(DateTimeOperation.InsertFieldDataTimeInTables(DateTimeOperation.ConvertStringToDateTime(ToSendTimeDateStr))) : null);
            }
        }

        public int? TypeGroupCompanies { get; set; }
        public int? ReciveUser { get; set; }
        public bool IsFinished { get; set; }

        public bool IsExcel { get; set; }

    }

    public class RequestForRatingDto
    {

        public string Assessment { get; set; }

        public string ReasonAssessment1 { get; set; }

        public string EvaluationExpert { get; set; }
        public string ReciveUserName { get; set; }

        public string CompanyName { get; set; }
        public string DestLevelStepIndex { get; set; }
        public int RequestId { get; set; }
        public string RequestNoStr { get; set; }
        public int? RequestNo { get; set; }
        public int? CustomerId { get; set; }


        public DateTime? DateOfRequest { get; set; }
        public string DateOfRequestStr
        {
            get
            {
                if (DateOfRequest.HasValue) return DateTimeOperation.ToPersianDate(DateOfRequest.Value);
                return string.Empty;
            }
        }

        public DateTime? SendTime { get; set; }
        public string SendTimeStr
        {
            get
            {
                if (SendTime.HasValue) return DateTimeOperation.ToPersianDate(SendTime.Value);
                return string.Empty;
            }
        }

        public DateTime? DateOfConfirmed { get; set; }
        public string DateOfConfirmedStr
        {
            get
            {
                if (DateOfConfirmed.HasValue) return DateTimeOperation.ToPersianDate(DateOfConfirmed.Value);
                return string.Empty;
            }
        }

        public DateTime? ChangeDate { get; set; }

        public int? KindOfRequest { get; set; }

        public string KindOfRequestName { get; set; }

        public bool IsFinished { get; set; }

        public string LevelStepStatus { get; set; }

        public string LevelStepAccessRole { get; set; }

        public string AgentName { get; set; }

        public string AgentMobile { get; set; }
        public string DestLevelStepAccessRole { get; set; }
        public string Comment { get; set; }
        public string DestLevelStepIndexButton { get; set; }

        public string ReciveUser { get; set; }

        public string ContractDocument { get; set; }

        public string SendUser { get; set; }

        public string LevelStepSettingIndexID { get; set; }

        public string NationalCode { get; set; }
        public string CodalNumber { get; set; }

        public DateTime? CodalDate { get; set; }
        public int? CustomerRequestInformationId { get; set; }

        public DateTime? LastStatusChangeDate { get; set; }
        public string LastStatusChangeDateStr
        {
            get
            {
                if (LastStatusChangeDate.HasValue) return DateTimeOperation.ToPersianDate(LastStatusChangeDate.Value);
                return string.Empty;
            }
        }
        public string SortColumn { get; set; }

    }
}
